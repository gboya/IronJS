using System;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CultureAndNumericsTests
{
    [TestClass]
    public class CultureAndDecimalTests
    {
        /// <summary>
        /// This Fails. See discussion of issue #97 on fholm/IronJS
        /// </summary>
        [TestMethod]
        public void TestDecimalWithCultureInfo()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("fi-FI");
            var ctx = new IronJS.Hosting.CSharp.Context();
            const decimal decimalValue = 1.5M;
            ctx.SetGlobal("decimalVal", decimalValue);
            var result = ctx.Execute("decimalVal < 3");
            Assert.IsTrue(result, "1.5 should be smaller");
            var returnedDecimal = ctx.GetGlobalAs<decimal>("decimalVal");
            Assert.AreEqual(decimalValue, returnedDecimal);
        }

        /// <summary>
        /// This Fails. See discussion of issue #97 on fholm/IronJS
        /// </summary>
        [TestMethod]
        public void TestFloatWithCultureInfo()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("fi-FI");
            var ctx = new IronJS.Hosting.CSharp.Context();
            ctx.SetGlobal("floatVal", 1.5F);
            var result = ctx.Execute("floatVal < 3");
            Assert.IsTrue(result, "1.5 should be smaller");
        }

        /// <summary>
        /// This passes.
        /// </summary>
        [TestMethod]
        public void TestDecimalWithCultureInfoEnUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var ctx = new IronJS.Hosting.CSharp.Context();
            ctx.SetGlobal("decimalVal", 1.5m);
            var result = ctx.Execute("decimalVal < 3");
            Assert.IsTrue(result, "1.5 should be smaller");
        }

        /// <summary>
        /// This passes, since decimal is boxed and unboxed as a plain CLR Object.
        /// </summary>
        [TestMethod]
        public void TestDecimalEqualityAfterSetAndGet()
        {
            var ctx = new IronJS.Hosting.CSharp.Context();
            const decimal d = 1.5m;
            const string globalVariableName = "decimalVal";
            
            ctx.SetGlobal(globalVariableName, d);
            var result = ctx.GetGlobalAs<decimal>(globalVariableName);

            Assert.AreEqual(d, result);
        }

        /// <summary>
        /// This passes as single is implicitly casted to double.
        /// </summary>
        [TestMethod]
        public void IsCLRSingleBoxedIntoNumber()
        {
            var boxedSingle = IronJS.Runtime.BoxedValue.Box(1.5F);
            Assert.IsTrue(boxedSingle.IsNumber);
        }

        /// <summary>
        /// This fails.
        /// </summary>
        [TestMethod]
        public void IsCLRSingleAnIronJSNumber()
        {
            var ctx = new IronJS.Hosting.CSharp.Context();
            const float f = 1.5F;

            ctx.SetGlobal("myFloat", f);
            var result = ctx.GetGlobal("myFloat");

            Assert.IsTrue(result.IsNumber);
        }

        [TestMethod]
        public void IsCLRDoubleAnIronJSNumber()
        {
            var ctx = new IronJS.Hosting.CSharp.Context();
            const double d = 1.5;

            ctx.SetGlobal("myDouble", d);
            var result = ctx.GetGlobal("myDouble");

            Assert.IsTrue(result.IsNumber);
        }
    }
}
