using System;

namespace TestCaseGenerator
{
    public interface ITestCase : ICloneable
    {
        void SetPropertyOnTestCase(string propertyName, object value);

        T ConstrainTestCase<T>(T testCase) where T : ITestCase, new ();
    }
}
