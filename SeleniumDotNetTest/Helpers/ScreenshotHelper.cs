using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumDotNetTest.Helpers
{
    public class ScreenshotHelper
    {
        private readonly IWebDriver driver;

        public ScreenshotHelper(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void TakeScreenshot(string screenshotName)
        {

            ITakesScreenshot screenShotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenShotDriver.GetScreenshot();

            screenshot.SaveAsFile($"{screenshotName}.png", ScreenshotImageFormat.Png);
        }
    }
}
