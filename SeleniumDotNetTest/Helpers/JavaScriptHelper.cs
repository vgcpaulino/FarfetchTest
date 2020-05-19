using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumDotNetTest.Helpers
{
    public class JavaScriptHelper
    {

        private readonly IWebDriver driver;
        private readonly IJavaScriptExecutor js;

        public JavaScriptHelper (IWebDriver driver)
        {
            this.driver = driver;
            js = (IJavaScriptExecutor) this.driver;
        }

        public void ScrollToElement(IWebElement element)
        {
            js.ExecuteScript("arguments[0].scrollIntoView();", element);
        }

        public void ScrollWindowCoordinates(int XAxis, int YAxis)
        {
            js.ExecuteScript("window.scrollBy(" + XAxis + "," + YAxis + ")");
        }

        public void ClickHiddenElement(string elementId)
        {
            js.ExecuteScript("document.getElementById('" + elementId + "').click();");
        }

        public string GetElementInnerHTML(string elementId)
        {
            return (string)js.ExecuteScript("document.getElementById('" + elementId + "').innerHTML;");
        }
    }
}
