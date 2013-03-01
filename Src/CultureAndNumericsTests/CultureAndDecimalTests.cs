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

        [TestMethod]
        public void TestFloatWithCultureInfo()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("fi-FI");
            var ctx = new IronJS.Hosting.CSharp.Context();
            ctx.SetGlobal("floatVal", 1.5F);
            var result = ctx.Execute("floatVal < 3");
            Assert.IsTrue(result, "1.5 should be smaller");
        }

        [TestMethod]
        public void TestDecimalWithCultureInfoEnUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var ctx = new IronJS.Hosting.CSharp.Context();
            ctx.SetGlobal("decimalVal", 1.5m);
            var result = ctx.Execute("decimalVal < 3");
            Assert.IsTrue(result, "1.5 should be smaller");
        }
    }
}
