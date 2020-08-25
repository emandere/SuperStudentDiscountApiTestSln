using TestCaseGenerator;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using Xunit;

namespace TestCaseGeneratorUnitTests
{
    public class ComboCountUnitTests
    {
        private InputDefinition _intDef;
        private InputDefinition _doubleDef;
        private InputDefinition _stringDef;
        private TestComboGen _testGen;

        public ComboCountUnitTests()
        {
            _intDef = new InputDefinition("Int", 1, new object[] { 1, 2, 3 });
            _doubleDef = new InputDefinition("Double", 1.0, new object[] { 1.0, 2.0, 3.0 });
            _stringDef = new InputDefinition("String", "1", new object[] { "1", "2", "3" });
            _testGen = new TestComboGen(new List<InputDefinition>() { _intDef, _doubleDef, _stringDef });
        }

        [Fact]
        public void OneFatCountTest()
        {
            var testCases = _testGen.GenerateNfatTestCases<ComboCountUnitTestCase>(1);
            Assert.Equal(7, testCases.Count());
        }

        [Fact]
        public void TwoFatCountTest()
        {
            var testCases = _testGen.GenerateNfatTestCases<ComboCountUnitTestCase>(2);
            Assert.Equal(19, testCases.Count());
        }

        [Fact]
        public void AllCountTest()
        {
            var testCases = _testGen.GenerateAllTestCases<ComboCountUnitTestCase>();
            Assert.Equal(27, testCases.Count());
        }

        internal class ComboCountUnitTestCase : ITestCase
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
                return testCase;
            }

            public void SetPropertyOnTestCase(string propertyName, object value)
            {
                PropertyInfo[] properties = typeof(ComboCountUnitTestCase).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name == propertyName)
                    {
                        property.SetValue(this, value);
                        return;
                    }
                }
            }

            #region Equality overloads for distinct test cases
            public override bool Equals(object obj)
            {
                if (object.ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (object.ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != this.GetType())
                {
                    return false;
                }

                return IsEqual((ComboCountUnitTestCase)obj);
            }

            public bool Equal(ComboCountUnitTestCase testCase)
            {
                if (object.ReferenceEquals(null, testCase))
                {
                    return false;
                }

                if (object.ReferenceEquals(this, testCase))
                {
                    return true;
                }

                return IsEqual(testCase);
            }

            private bool IsEqual(ComboCountUnitTestCase testCase)
            {
                return testCase.Int.Equals(this.Int) &&
                    testCase.Double.Equals(this.Double) &&
                    testCase.String.Equals(this.String);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    // Choose large primes to avoid hashing collisions
                    const int HashingBase = (int)2166136261;
                    const int HashingMultiplier = 16777619;

                    int hash = HashingBase;
                    hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Int) ? Int.GetHashCode() : 0);
                    hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Double) ? Double.GetHashCode() : 0);
                    hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, String) ? String.GetHashCode() : 0);
                    return hash;
                }
            }
            #endregion
        }
    }
}
