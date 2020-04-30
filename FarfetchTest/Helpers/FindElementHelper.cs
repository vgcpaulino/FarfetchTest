using AventStack.ExtentReports.Model;
using FarfetchSeleniumTest.Fixtures;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarfetchSeleniumTest.Helpers
{
    public class FindElementHelper
    {

        private readonly IWebDriver driver;
        private readonly TestFixture fixture;

        public FindElementHelper(IWebDriver driver)
        {
            this.driver = driver;
            fixture = new TestFixture();
        }

        public dynamic TryFindElementRefreshDriver(By locator)
        {
            try
            {
                return driver.FindElement(locator);
            }
            catch (NoSuchElementException e)
            {
                fixture.Dispose();
                fixture.CreateWebDriver();
                throw new NoSuchElementException(e.Message);
            }
        }


    }
}
