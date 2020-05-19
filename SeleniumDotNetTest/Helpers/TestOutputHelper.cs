using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SeleniumDotNetTest.Helpers
{
    public class TestOutputHelper
    {

        private ITestOutputHelper output;

        public TestOutputHelper(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void WriteCustomLine(string message)
        {
            output.WriteLine("@@@ " + message + " @@@");
        }
    }
}
