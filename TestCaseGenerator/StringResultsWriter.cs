using System.Text;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Collections;

namespace TestCaseGenerator
{
    public static class StringResultsWriter
    {
        public static void AddHeaders<T>(StringBuilder sb) where T : ITestCase, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            sb.Append("| ");
            foreach (PropertyInfo property in properties)
            {
                sb.Append(property.Name + " | ");
            }
        }

        public static void AddRow<T>(T testCase, StringBuilder sb) where T : ITestCase, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            sb.AppendLine();
            sb.Append("| ");
            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(testCase);
                if(value is IList)
                {
                    foreach(object subVal in value as IList)
                    {
                        sb.Append(string.Concat(subVal, ","));
                    }
                    sb.Append(" | ");
                }
                else
                {
                    sb.Append(value + " | ");
                }
            }
        }
    }
}
