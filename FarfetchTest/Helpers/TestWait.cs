using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.WaitHelpers;
using Xunit.Abstractions;
using System.Diagnostics;

namespace FarfetchSeleniumTest.Helpers
{
    public class TestWait
    {

        private readonly IWebDriver driver;
        private WebDriverWait wait;
        private readonly Stopwatch timeout;

        public TestWait(IWebDriver driver)
        {
            this.driver = driver;
            timeout = new Stopwatch();
        }

        private WebDriverWait InitializeWait(int seconds)
        {
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return this.wait;
        }

        public void WaitElementInteractable(int seconds, By locator)
        {
            wait = InitializeWait(seconds);            
            WaitElementExists(seconds, locator);
            WaitElementVisible(seconds, locator);
        }

        public void WaitElementExists(int seconds, By locator)
        {
            wait = InitializeWait(seconds);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }

        public void WaitElementVisible(int seconds, By locator)
        {
            wait = InitializeWait(seconds);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public void WaitElementClickable(int seconds, By locator)
        {
            wait = InitializeWait(seconds);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public void WaitTextInElement(int seconds, IWebElement element, string text)
        {
            wait = InitializeWait(seconds);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, text));
        }

        public void WaitTextDifferentFrom(int seconds, IWebElement element, string text)
        {          
            timeout.Start();
            bool comparsionResult = (element.Text == text);
            while (comparsionResult)
            {
                comparsionResult = (element.Text == text);
                if (timeout.ElapsedMilliseconds > (seconds * 1000))
                {
                    //throw new TimeoutException();
                    // Using the Selenium Class to keep the same Exception, but the above can be used;
                    wait = InitializeWait(0);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, "@-@-@NOTFOUND@-@-@"));
                }
            }
        }

        public void WaitGenericElement(int seconds, By locator)
        {
            wait = InitializeWait(seconds);
            wait.Until((driver) => driver.FindElement(locator));
        }

    }
}
