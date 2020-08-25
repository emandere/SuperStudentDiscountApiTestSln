using System.Collections.Generic;
using System.Linq;
using System;

namespace TestCaseGenerator
{
    public class TestComboGen
    {
        private int _nOrder { get; set; }
        private TestingTechniques _testingTechniques { get; set; }
        private IList<InputDefinition> _inputDefinitions { get; set; }
        
        public TestComboGen(IList<InputDefinition> inputDefinitions)
        {
            if(inputDefinitions == null || inputDefinitions.Count < 1)
            {
                throw new ApplicationException("Test Case Generation requires at least 1 input.");
            }
            _inputDefinitions = inputDefinitions;
        }

        public IEnumerable<T> GenerateAllTestCases<T>() where T : ITestCase, new()
        {
            _testingTechniques = TestingTechniques.All;
            return GetTestCases<T>();
        }

        public IEnumerable<T> GenerateNfatTestCases<T>(int order) where T : ITestCase, new()
        {
            if(order > _inputDefinitions.Count)
            {
                throw new ApplicationException($"{order}-FAT was selected which exceeds the number of inputs provided: {_inputDefinitions.Count}.  Please select {_inputDefinitions.Count}-FAT or lower.");
            }
            _testingTechniques = TestingTechniques.NFat;
            _nOrder = order;
            return GetTestCases<T>();
        }

        private IEnumerable<T> GetTestCases<T>() where T : ITestCase, new()
        {
            var testCases = new List<T>();

            if(_testingTechniques == TestingTechniques.NFat)
            {
                NFatGen nfatGen = new NFatGen(_inputDefinitions, _nOrder);
                testCases = nfatGen.Execute<T>();
            }
            else if(_testingTechniques == TestingTechniques.All)
            {
                NFatGen allComboGen = new NFatGen(_inputDefinitions, _inputDefinitions.Count);
                testCases = allComboGen.Execute<T>();
            }

            var distinctTestCases = testCases.Distinct();

            foreach (T tc in distinctTestCases)
            {
                if(tc.ConstrainTestCase<T>(tc) != null)
                {
                    yield return tc;
                }
            }
        }
    }

    enum TestingTechniques
    {
        NFat,
        All
    }
}
