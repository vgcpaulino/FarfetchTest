using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using SeleniumDotNetTest.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Drivers;
using System;
using Xunit;

namespace SeleniumDotNetTest.Tests.Google
{
    public class MobileBrowserTests : IDisposable
    {

        private readonly IWebDriver driver;
        private readonly DriverSetUp setup;
        private readonly ElementHelper element;

        public MobileBrowserTests()
        {
            ChromeMobileEmulationDeviceSettings deviceSettings = new ChromeMobileEmulationDeviceSettings();
            deviceSettings.Width = 1024;
            deviceSettings.Height = 1366;
            deviceSettings.UserAgent = "CustomDevice";

            ChromeOptions chromeOpt = new ChromeOptions();
            chromeOpt.EnableMobileEmulation(deviceSettings);

            setup = new DriverSetUp();
            driver = new ChromeDriver(setup.ChromeDriverFolder, chromeOpt);
            element = new ElementHelper(driver);
        }

        [Fact]
        public void TestMobileBrowser()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");

            IWebElement gAppsBar = element.TryFindElement(By.Id("gbar"));

            Assert.True(gAppsBar.Displayed);
        }

        [Fact]
        public void ExtentReport()
        {
            var htmlReporter = new ExtentHtmlReporter(@"C:\report\xpto2.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            //htmlReporter.Start();
            var extent = new AventStack.ExtentReports.ExtentReports();

            extent.AttachReporter(htmlReporter);

            var testLog = extent.CreateTest("My First Test", "Sample Description");
            testLog.Log(Status.Info, "This step shows usage of log(status, details)");
            testLog.Info("This step shows usage of info(details)");
            testLog.Fail("details", MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());
            testLog.AddScreenCaptureFromPath("screenshot.png");
            extent.Flush();

        }

        public void Dispose()
        {
            driver.Quit();
        }

    }
}
