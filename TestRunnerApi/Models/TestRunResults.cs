using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRunnerApi.Models
{
    public class TestRunResults
    {
        public IEnumerable<TestRun> TestRuns { get; set; }
    }
}
