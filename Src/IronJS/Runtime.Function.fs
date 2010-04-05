﻿module IronJS.Runtime.Function

open IronJS
open IronJS.Utils
open IronJS.Runtime
open System.Dynamic
open System.Collections.Generic

(*Closure base class, representing a closure environment*)
type Closure =
  val mutable Globals : Core.Object
  val mutable Environment : Core.IEnvironment

  new(globals:Core.Object, env:Core.IEnvironment) = {
    Globals = globals
    Environment = env
  }

(*Typedef*)
let closureTypeDef = typedefof<Closure>

let makeParamTypeList (args:MetaObj array) =
  let len = args.Length

  closureTypeDef 
  :: Runtime.Core.objectTypeDef 
  :: Runtime.Core.objectTypeDef 
  :: Array.fold (fun state (arg:MetaObj) -> arg.LimitType :: state) [Constants.clrDynamic] args

(*Javascript object that also is a function*)
type Function<'a> when 'a :> Closure =
  inherit Core.Object

  val mutable Closure : 'a
  val mutable Ast : Ast.Types.Node

  new(ast, closure, env) = { 
    inherit Core.Object(env)
    Closure = closure
    Ast = ast
  }

  interface System.Dynamic.IDynamicMetaObjectProvider with
    member self.GetMetaObject expr = new FunctionMeta<'a>(expr, self) :> MetaObj

(*DLR meta object for the above Function class*)
and FunctionMeta<'a> when 'a :> Closure (expr, jsFunc:Function<'a>) =
  inherit Core.ObjectMeta(expr, jsFunc)

  member self.FuncExpr with get() = Tools.Expr.castT<Function<'a>> self.Expression
  member self.ClosureExpr with get() = Tools.Expr.field self.FuncExpr "Closure"
  member self.AstExpr with get() = Tools.Expr.field self.FuncExpr "Ast"

  override self.BindInvoke (binder, args) =
    let types = List.tail [for arg in args -> arg.LimitType]
    let func, paramTypes = jsFunc.Environment.GetDelegate jsFunc.Ast typeof<'a> types
    let paramTypes = Runtime.Core.objectTypeDef :: paramTypes

    let expr = 
      Tools.Expr.invoke (Tools.Expr.constant func) (
        self.ClosureExpr 
        :: Tools.Expr.typeDefault<Runtime.Core.Object> 
        :: List.rev (Array.fold (fun lst (arg:MetaObj) -> Tools.Expr.cast arg.Expression paramTypes.[lst.Length] :: lst) [] args)
      )

    let restrict = (Tools.Expr.restrictType self.Expression typeof<Function<'a>>).
                      Merge(Tools.Expr.restrictInst self.AstExpr jsFunc.Ast).
                      Merge(Tools.Expr.restrictArgs (List.tail (Array.toList args)))

    new MetaObj(expr, restrict)

let functionTypeDef = typedefof<Function<_>>
let functionTypeDefHashCode = functionTypeDef.GetHashCode()