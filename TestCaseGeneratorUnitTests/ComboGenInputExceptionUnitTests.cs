using TestCaseGenerator;
using System.Collections.Generic;
using System.Reflection;
using System;
using Xunit;

namespace TestCaseGeneratorUnitTests
{
    public class ComboGenInputExceptionUnitTests
    {

        [Fact]
        public void TwoFatOneInputTest()
        {
            InputDefinition intDef = new InputDefinition("Int", 1, new object[] { 1, 2, 3 });
            TestComboGen gen = new TestComboGen(new List<InputDefinition>() { intDef });
            string exceptionMessage = string.Empty;

            try
            {
                var testCases = gen.GenerateNfatTestCases<ComboGenOneInputTestCase>(2);
            }
            catch(ApplicationException e)
            {
                exceptionMessage = e.Message;
            }

            Assert.Equal("2-FAT was selected which exceeds the number of inputs provided: 1.  Please select 1-FAT or lower.", exceptionMessage);
        }

        [Fact]
        public void ThreeFatTwoInputsTest()
        {
            InputDefinition intDef = new InputDefinition("Int", 1, new object[] { 1, 2, 3 });
            InputDefinition doubleDef = new InputDefinition("Double", 1.0, new object[] { 1.0, 2.0, 3.0 });
            TestComboGen gen = new TestComboGen(new List<InputDefinition>() { intDef, doubleDef });
            string exceptionMessage = string.Empty;

            try
            {
                var testCases = gen.GenerateNfatTestCases<ComboGenTwoInputTestCase>(3);
            }
            catch (ApplicationException e)
            {
                exceptionMessage = e.Message;
            }

            Assert.Equal("3-FAT was selected which exceeds the number of inputs provided: 2.  Please select 2-FAT or lower.", exceptionMessage);
        }

        [Fact]
        public void InputCount0Test()
        {
            string exceptionMessage = string.Empty;

            try
            {
                TestComboGen gen = new TestComboGen(new List<InputDefinition>());
            }
            catch (ApplicationException e)
            {
                exceptionMessage = e.Message;
            }

            Assert.Equal("Test Case Generation requires at least 1 input.", exceptionMessage);
        }

        [Fact]
        public void InputNullTest()
        {
            string exceptionMessage = string.Empty;

            try
            {
                TestComboGen gen = new TestComboGen(null);
            }
            catch (ApplicationException e)
            {
                exceptionMessage = e.Message;
            }

            Assert.Equal("Test Case Generation requires at least 1 input.", exceptionMessage);
        }

        internal class ComboGenOneInputTestCase : ITestCase
        {
            public int Int { get; set; }

            public object Clone()
            {
                return this.MemberwiseClone();
            }

            public T ConstrainTestCase<T>(T testCase) where T : ITestCase, new()
            {
                return testCase;
            }

            public void SetPropertyOnTestCase(string propertyName, object value)
            {
                PropertyInfo[] properties = typeof(ComboGenOneInputTestCase).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name == propertyName)
                    {
                        property.SetValue(this, value);
                        return;
                    }
                }
            }
        }

        internal class ComboGenTwoInputTestCase : ITestCase
        {
            public int Int { get; set; }
            public double Double { get; set; }

            public object Clone()
            {
                return this.MemberwiseClone();
            }

            public T ConstrainTestCase<T>(T testCase) where T : ITestCase, new()
            {
                return testCase;
            }

            public void SetPropertyOnTestCase(string propertyName, object value)
            {
                PropertyInfo[] properties = typeof(ComboGenOneInputTestCase).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name == propertyName)
                    {
                        property.SetValue(this, value);
                        return;
                    }
                }
            }
        }
    }
}
