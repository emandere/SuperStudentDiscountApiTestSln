using System.Collections.Generic;

namespace TestCaseGenerator
{
    public class InputDefinition
    {
        public string InputName { get; private set; }
        public object DefaultValue { get; private set; }
        public IEnumerable<object> ExerciseValues { get; private set; }

        public InputDefinition(string inputName, object defaultValue, IEnumerable<object> exerciseValues)
        {
            InputName = inputName;
            DefaultValue = defaultValue;
            ExerciseValues = exerciseValues;
        }
    }
}
