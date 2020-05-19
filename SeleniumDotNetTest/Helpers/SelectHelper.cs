using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumDotNetTest.Helpers
{
    public class SelectHelper
    {
        private SelectElement select;

        public void SelectByText(IWebElement element, string text)
        {
            select = new SelectElement(element);
            select.SelectByText(text);
        }

        public int SelectReturnQtyOptions(IWebElement element)
        {
            select = new SelectElement(element);
            return select.Options.Count;
        }

        public List<string> GetListOfOptions(IWebElement element)
        {
            select = new SelectElement(element);
            IList<IWebElement> optionElements = select.Options;
            List<string> selectOptions = new List<string>();

            foreach (IWebElement optElement in optionElements)
            {
                selectOptions.Add(optElement.Text);
            }

            return selectOptions;
        }

    }
}
