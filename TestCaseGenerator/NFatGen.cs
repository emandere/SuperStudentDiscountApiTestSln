using System.Collections.Generic;

namespace TestCaseGenerator
{
    public class NFatGen
    {
        private IList<InputDefinition> _inputDefinitions { get; set; }
        private int _nFATOrder { get; set; }

        public NFatGen(IList<InputDefinition> inputDefinitions, int nFATOrder)
        {
            _inputDefinitions = inputDefinitions;
            _nFATOrder = nFATOrder;
        }

        public List<T> Execute<T>() where T : ITestCase, new() 
        {
            T defaultTestCase = GetDefaultTestCase<T>();
            List<T> testCases = new List<T>();
            AddToTestCases(testCases, defaultTestCase, _nFATOrder, 0);
            return testCases;
        }

        private void AddToTestCases<T>(List<T> testCases, T combination, int nLeft, int nFATLevel) where T : ITestCase, new()
        {
            if(nLeft > 0)
            {
                while(nFATLevel + nLeft <= _inputDefinitions.Count)
                {
                    InputDefinition currentInputDefinition = _inputDefinitions[nFATLevel];
                    foreach(object value in currentInputDefinition.ExerciseValues)
                    {
                        var tempTestCase = (T)combination.Clone();
                        tempTestCase.SetPropertyOnTestCase(currentInputDefinition.InputName, value);
                        AddToTestCases(testCases, tempTestCase, nLeft - 1, nFATLevel + 1);
                    }
                    nFATLevel++;
                }
            }
            else
            {
                testCases.Add(combination);
            }
        }

        private T GetDefaultTestCase<T>() where T : ITestCase, new()
        {
            T testCase = new T();
            foreach(InputDefinition inputDefinition in _inputDefinitions)
            {
                testCase.SetPropertyOnTestCase(inputDefinition.InputName, inputDefinition.DefaultValue);
            }
            return testCase;
        }
    }
}
