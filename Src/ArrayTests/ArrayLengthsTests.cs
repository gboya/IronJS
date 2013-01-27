using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using IronJS.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArrayTests
{
    /// <summary>
    /// Summary description for ArrayLengthsTests
    /// </summary>
    [TestClass]
    public class ArrayLengthsTests
    {
        public ArrayLengthsTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void TestWithZeroSizedArray()
        {
            string jsCode = @"myArray.length;";
            var javascriptEngine = new IronJS.Hosting.CSharp.Context();
            
            var array = new ArrayObject(javascriptEngine.Environment, 0); // Creates a 0-sized Array
            array.Put(0, 12.0);
            array.Put(1, 45.1);

            javascriptEngine.SetGlobal<ArrayObject>("myArray", array);

            var result = javascriptEngine.Execute(jsCode);
            Assert.AreEqual((double) 2, (double) result);
        }

        [TestMethod]
        public void HandlingArrayLength()
        {
            var jsCode = @"myArray.length;";
            var javascriptEngine = new IronJS.Hosting.CSharp.Context();

            var array = new ArrayObject(javascriptEngine.Environment, 2);//array of size 2
            array.Put(0, 12.0);//mock values
            array.Put(1, 45.1);

            javascriptEngine.SetGlobal<ArrayObject>("myArray", array);

            var result = javascriptEngine.Execute(jsCode);
            Assert.AreEqual((double) 2, (double) result);

        }
    }
}
