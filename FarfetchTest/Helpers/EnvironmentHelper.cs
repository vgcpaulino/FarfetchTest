using System;
using System.Collections.Generic;
using System.Text;

namespace FarfetchSeleniumTest.Helpers
{
    public class EnvironmentHelper
    {

        public string GetEnvVariableValue(string variableName)
        {
            string value = Environment.GetEnvironmentVariable(variableName);

            return value;
        }

    }
}
