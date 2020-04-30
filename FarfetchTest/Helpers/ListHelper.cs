using System;
using System.Collections.Generic;
using System.Text;

namespace FarfetchSeleniumTest.Helpers
{
    public class ListHelper
    {
        public int ListCompareMatches(List<string> expectedList, List<string> actualList)
        {
            int qtyFound = 0;
            int qtyFound2 = 0;

            foreach (string expectedItem in expectedList)
            {
                foreach (string actualItem in actualList)
                {
                    if (expectedItem == actualItem)
                    {
                        qtyFound2++;
                    }
                }
            }

            for (int counter = 0; counter < expectedList.Count; counter++)
            {
                for (int actualListCounter = 0; actualListCounter < actualList.Count; actualListCounter++)
                {
                    if (expectedList[counter] == actualList[actualListCounter])
                    {
                        qtyFound++;
                    }
                }
            }
            return qtyFound;
        }
    }
}
