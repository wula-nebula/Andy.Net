using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.TestApp
{
    public class TestService : ITestService
    {
        public string Get(string id)
        {
            return $"你好，我是{id}";
        }
    }
}
