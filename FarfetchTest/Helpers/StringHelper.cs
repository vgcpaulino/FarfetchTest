using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarfetchSeleniumTest.Helpers
{
    public class StringHelper
    {

        public string RemoveCurrencySymbols(string text)
        {
            List<string> knowSymbols = new List<string>(){
                "R$", "U$", "£", "€"
            };

            foreach (string symbol in knowSymbols)
            {
                text = text.Replace(symbol, "").Trim();
            }

            return text;
        }

        public string RemoveDigits(string text)
        {
            return new string(text.Where(char.IsLetter).ToArray());
        }
    }
}
