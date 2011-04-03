// <auto-generated />
namespace IronJS.Tests.UnitTests.Sputnik.Conformance.NativeECMAScriptObjects.NumberObjects.PropertiesOfNumberConstructor
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class NumberMINVALUETests : SputnikTestFixture
    {
        public NumberMINVALUETests()
            : base(@"Conformance\15_Native_ECMA_Script_Objects\15.7_Number_Objects\15.7.3_Properties_of_Number_Constructor\15.7.3.3_Number.MIN_VALUE")
        {
        }

        [Test]
        [Category("Sputnik Conformance")]
        [Category("ECMA 15.7.3.3")]
        [TestCase("S15.7.3.3_A1.js", Description = "Number.MIN_VALUE is approximately 5e-324")]
        public void NumberMIN_VALUEIsApproximately5e324(string file)
        {
            RunFile(file);
        }

        [Test]
        [Category("Sputnik Conformance")]
        [Category("ECMA 15.7.3.3")]
        [TestCase("S15.7.3.3_A2.js", Description = "Number.MIN_VALUE is ReadOnly")]
        public void NumberMIN_VALUEIsReadOnly(string file)
        {
            RunFile(file);
        }

        [Test]
        [Category("Sputnik Conformance")]
        [Category("ECMA 15.7.3.3")]
        [TestCase("S15.7.3.3_A3.js", Description = "Number.MIN_VALUE is DontDelete")]
        public void NumberMIN_VALUEIsDontDelete(string file)
        {
            RunFile(file);
        }

        [Test]
        [Category("Sputnik Conformance")]
        [Category("ECMA 15.7.3.3")]
        [TestCase("S15.7.3.3_A4.js", Description = "Number.MIN_VALUE has the attribute DontEnum")]
        public void NumberMIN_VALUEHasTheAttributeDontEnum(string file)
        {
            RunFile(file);
        }
    }
}