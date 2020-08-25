using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRunnerApi.Models
{
    public class TestRun
    {
        public string TestName { get; set; }

        public bool HasPassed { get; set; }

        public string Message { get; set; }
    }
}
