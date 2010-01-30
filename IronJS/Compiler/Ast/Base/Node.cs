﻿using System;
using System.Collections.Generic;
using System.Text;
using Antlr.Runtime.Tree;
using IronJS.Extensions;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using Et = System.Linq.Expressions.Expression;

namespace IronJS.Compiler.Ast
{
    public enum NodeType
    {
        Assign, Identifier, Double, Null,
        MemberAccess, Call, If, Eq, Block,
        String, Lambda, While, BinaryOp,
        Object, New, AutoProperty, Return,
        UnaryOp, Logical, PostfixOperator,
        TypeOf, Boolean, Void, StrictCompare,
        UnsignedRightShift, ForStep, ForIn,
        Break, Continue, With, Try, Catch,
        Throw, IndexAccess, Delete, In,
        Switch, InstanceOf, Regex, Array,
        Integer
    }

    abstract public class Node : INode
    {
        public NodeType NodeType { get; protected set; }
        public int Line { get; protected set; }
        public int Column { get; protected set; }
        public virtual JsType ExprType { get { return JsType.Dynamic; } }

        public Node(NodeType type, ITree node)
        {
            NodeType = type;

            if (node != null)
            {
                Line = node.Line;
                Column = node.CharPositionInLine;
            }
            else
            {
                Line = -1;
                Column = -1;
            }
        }

        public bool IdenticalTypes(params INode[] nodes)
        {
            if (nodes.Length > 0)
            {
                var type = nodes[0].ExprType;

                for (int i = 0; i < nodes.Length; ++i)
                    if (nodes[i].ExprType != type)
                        return false;

                return true;
            }

            return false;
        }
        
        public JsType EvalTypes(params INode[] nodes)
        {
            var set = new HashSet<JsType>();

            foreach (var node in nodes)
                set.Add(node.ExprType);

            return set.EvalType();
        }

        public string Print()
        {
            var writer = new StringBuilder();

            Print(writer);

            return writer.ToString();
        }

        public virtual void Print(StringBuilder writer, int indent = 0)
        {
            var indentStr = new String(' ', indent * 2);
            writer.AppendLine(indentStr + "(" + NodeType + ")");
        }

        public virtual INode Optimize(AstOptimizer astopt)
        {
            return this;
        }

        public virtual Et Generate2(EtGenerator etgen)
        {
            return AstUtils.Empty();
        }

        #region Abstract

        public abstract Et Generate(EtGenerator etgen);

        #endregion
    }
}