using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumDotNetTest.Helpers
{
    public class TabHelper
    {
        private IWebDriver driver;

        public TabHelper(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void CreateNewTab()
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)driver;
            scriptExecutor.ExecuteScript("window.open();");
            //IWebElement boby = driver.FindElement(By.CssSelector("body"));
            //Actions action = new Actions(driver);
            ///action.KeyDown(Keys.Control).MoveToElement(boby).Click().KeyUp(Keys.Control).Perform();
            //action.KeyDown(Keys.Control).SendKeys("t").KeyUp(Keys.Control).Build().Perform();
            //action.SendKeys(Keys.Control + "T").Perform();
            //action.SendKeys(OpenQA.Selenium.Keys.Control + "t").Build().Perform();
        }

        public void SelectLastTab()
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public void SelectTabByPageTitle(string pageTitle)
        {
            foreach (string currentWindowHandle in driver.WindowHandles)
            {
                string currentPageTitle = driver.SwitchTo().Window(currentWindowHandle).Title;
                if (currentPageTitle.Contains(pageTitle))
                {
                    driver.SwitchTo().Window(currentWindowHandle);
                }
            }
        }

        public void CloseCurrentTab()
        {
            driver.Close();
        }
    }
}
