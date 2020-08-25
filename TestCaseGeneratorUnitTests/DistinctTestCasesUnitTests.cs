using TestCaseGenerator;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using Xunit;

namespace TestCaseGeneratorUnitTests
{
    public class DistinctTestCasesUnitTests
    {
        private InputDefinition _intDef;
        private InputDefinition _doubleDef;
        private InputDefinition _stringDef;
        private TestComboGen _testGen;

        public DistinctTestCasesUnitTests()
        {
            _intDef = new InputDefinition("Int", 1, new object[] { 1, 2, 3 });
            _doubleDef = new InputDefinition("Double", 1.0, new object[] { 1.0, 2.0, 3.0 });
            _stringDef = new InputDefinition("String", "1", new object[] { "1", "2", "3" });
            _testGen = new TestComboGen(new List<InputDefinition>() { _intDef, _doubleDef, _stringDef });
        }

        [Fact]
        public void NonDistinctTestCaseTest()
        {
            var testCases = _testGen.GenerateNfatTestCases<NonDistinctTestCase>(1);
            Assert.Equal(9, testCases.Count());
        }

        [Fact]
        public void DistinctTestCasesTest()
        {
            var testCases = _testGen.GenerateNfatTestCases<DistinctTestCase>(1);
            Assert.Equal(7, testCases.Count());
        }

        [Fact]
        public void AllCombosDistinctTestCasesTest()
        {
            var testCases = _testGen.GenerateAllTestCases<DistinctTestCase>();
            Assert.Equal(27, testCases.Count()); //All combos should generate the same amount of test cases regardless if the test case is setup for distinct vs non distinct
        }

        [Fact]
        public void AllCombosNonDistinctTestCasesTest()
        {
            var testCases = _testGen.GenerateAllTestCases<NonDistinctTestCase>();
            Assert.Equal(27, testCases.Count()); //All combos should generate the same amount of test cases regardless if the test case is setup for distinct vs non distinct
        }

        internal class NonDistinctTestCase : ITestCase
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
                PropertyInfo[] properties = typeof(NonDistinctTestCase).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

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

        internal class DistinctTestCase : ITestCase
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
                PropertyInfo[] properties = typeof(DistinctTestCase).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

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

                return IsEqual((DistinctTestCase)obj);
            }

            public bool Equal(DistinctTestCase testCase)
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

            private bool IsEqual(DistinctTestCase testCase)
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
