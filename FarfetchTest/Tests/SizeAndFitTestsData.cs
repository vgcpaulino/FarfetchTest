using System;
using System.Collections.Generic;
using System.Text;

namespace FarfetchSeleniumTest.Tests
{
    public class SizeAndFitTestsData
    {

        public List<string> ProductStandardPriceTableSelect => new List<string>() { "PADRÃO\r\n(Saint Laurent)", "S", "SIZE", "50" }; 
        public List<string> ProductOutOfStockTableSelect => new List<string>() { "PADRÃO\r\n(Saint Laurent)", "XXXXS", "SIZE", "42" };
        public List<string> ProductDifferentPriceTableSelect => new List<string>() { "PADRÃO\r\n(Saint Laurent)", "L", "SIZE", "54" };
        public List<string> ProductDiscountTableSelect => new List<string>() { "PADRÃO\r\n(Saint Laurent)", "L", "SIZE", "54" };
        public List<string> CountryList => new List<string>(){ "FRANCE", 
            "ITALY",
            "UK",
            "US",
            "GERMANY",
            "MEN BRAZIL",
            "JAPAN",
            "JAPAN STANDARD",
            "KOREA STANDARD",
            "KOREA",
            "SPAIN",
            "CHINA"
        };


    }
}
