using OpenQA.Selenium;
using System.Collections.Generic;

namespace FarfetchSeleniumTest.Helpers
{
    public class ElementHelper
    {

        private readonly IWebDriver driver;
        private readonly CookiesHelper cookies;

        public ElementHelper(IWebDriver driver)
        {
            this.driver = driver;
            cookies = new CookiesHelper(this.driver);
        }

        public IWebElement TryFindElement(By locator)
        {
            return TryCatchFind(false, locator);
        }

        public IList<IWebElement> TryFindElements(By locator)
        {
            return TryCatchFind(true, locator);
        }


        private dynamic TryCatchFind(bool multipleElements, By locator)
        {
            try
            {
                if (multipleElements)
                {
                    return driver.FindElements(locator);
                } else
                {
                    return driver.FindElement(locator);
                }
            }
            catch (NoSuchElementException e)
            {
                RefreshBrowser();
                throw new NoSuchElementException(e.Message);
            }
        }

        private void RefreshBrowser()
        {
            driver.Navigate().GoToUrl("http://localhost:12345");
            cookies.DeleteAllCookies();
        }


    }
}
