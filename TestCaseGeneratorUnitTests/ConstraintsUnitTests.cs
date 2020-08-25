using TestCaseGenerator;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Xunit;

namespace TestCaseGeneratorUnitTests
{
    public class ConstraintsUnitTests
    {
        private InputDefinition _intDef;
        private InputDefinition _doubleDef;
        private InputDefinition _stringDef;
        private TestComboGen _testGen;

        public ConstraintsUnitTests()
        {
            _intDef = new InputDefinition("Int", 1, new object[] { 1, 2, 3 });
            _doubleDef = new InputDefinition("Double", 1.0, new object[] { 1.0, 2.0, 3.0 });
            _stringDef = new InputDefinition("String", "1", new object[] { "1", "2", "3" });
            _testGen = new TestComboGen(new List<InputDefinition>() { _intDef, _doubleDef, _stringDef });
        }

        [Fact]
        public void ThrowOutConstrainedTestCaseTest()
        {
            var testCases = _testGen.GenerateNfatTestCases<ConstraintsThrowOutTestCase>(1);
            var constraintTestCase = testCases.FirstOrDefault(x => x.Int == 1 && x.Double == 1.0 && x.String == "1");
            Assert.Null(constraintTestCase);
        }

        [Fact]
        public void ModifyConstrainedTestCaseTest()
        {
            var testCases = _testGen.GenerateNfatTestCases<ConstraintsModifyTestCase>(1);
            var constraintTestCase = testCases.FirstOrDefault(x => x.Int == 1 && x.Double == 1.0 && x.String == "MODIFIED");
            Assert.NotNull(constraintTestCase);
        }

        internal class ConstraintsThrowOutTestCase : ITestCase
        {
            public int Int { get; set; }
            public double Double { get; set; }
            public string String { get; set; }

            public object Clone()
            {
                return this.MemberwiseClone();
            }

            public T ConstrainTestCase<T>(T testCase) where T : ITestCase, new()
            {
                ConstraintsThrowOutTestCase tc = testCase as ConstraintsThrowOutTestCase;

                if(tc.Int == 1 && tc.Double == 1.0 && tc.String == "1")
                {
                    return default(T);
                }

                return testCase;
            }

            public void SetPropertyOnTestCase(string propertyName, object value)
            {
                PropertyInfo[] properties = typeof(ConstraintsThrowOutTestCase).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

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

        internal class ConstraintsModifyTestCase : ITestCase
        {
            public int Int { get; set; }
            public double Double { get; set; }
            public string String { get; set; }

            public object Clone()
            {
                return this.MemberwiseClone();
            }

            public T ConstrainTestCase<T>(T testCase) where T : ITestCase, new()
            {
                ConstraintsModifyTestCase tc = testCase as ConstraintsModifyTestCase;

                if (tc.Int == 1 && tc.Double == 1.0 && tc.String == "1")
                {
                    tc.String = "MODIFIED";
                }

                return testCase;
            }

            public void SetPropertyOnTestCase(string propertyName, object value)
            {
                PropertyInfo[] properties = typeof(ConstraintsModifyTestCase).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

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
