using System;
using System.Collections.Generic;
using System.Text;

namespace FarfetchSeleniumTest.Helpers
{
    public class ConvertHelper
    {

        public double ConvertStrToDouble(string text)
        {
            return Convert.ToDouble(text);
        }

        public int ConvertDoubleToInt(double value)
        {
            return Convert.ToInt32(value);
        }

        public List<double> ConvertMetersToFeetInches(double value)
        {
            double newDoubleValue = value * 3.28084;
            double decimalPart = newDoubleValue - Math.Truncate(newDoubleValue);
            double feetValue = ConvertDoubleToInt(newDoubleValue);
            double inchesValue = Math.Round(decimalPart * 12);
            List<double> returnValues = new List<double>() { feetValue, inchesValue };
            return returnValues;
        }
        
        public double ConvertCmToInches(double value)
        {
            return Math.Round((value * 0.393701), 1);
        }
    }
}
