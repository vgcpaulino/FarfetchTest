using AngleSharp.Dom;
using SeleniumDotNetTest.Fixtures;
using SeleniumDotNetTest.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace SeleniumDotNetTest.Tests
{
    public class AlertTests: IClassFixture<TestFixture>
    {

        private readonly IWebDriver driver;
        private ScreenshotHelper screenShot;
        private readonly ElementHelper element;

        private readonly IWebElement jsAlertBtn;
        private readonly IWebElement jsConfirmBtn;
        private readonly IWebElement jsPromptBtn;
        private readonly IWebElement resultText;


        public AlertTests(TestFixture fixture)
        {
            driver = fixture.Driver;
            screenShot = new ScreenshotHelper(driver);
            element = new ElementHelper(this.driver);

            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/javascript_alerts");

            jsAlertBtn = element.TryFindElement(By.CssSelector("button[onclick='jsAlert()']"));
            jsConfirmBtn = element.TryFindElement(By.CssSelector("button[onclick='jsConfirm()']"));
            jsPromptBtn = element.TryFindElement(By.CssSelector("button[onclick='jsPrompt()']"));
            resultText = element.TryFindElement(By.Id("result"));
        }

        [Fact]
        public void AlertText()
        {
            jsAlertBtn.Click();

            IAlert jsAlertPopup = driver.SwitchTo().Alert();

            Assert.Equal("I am a JS Alert", jsAlertPopup.Text);
            AlertText_Dispose();
        }

        internal void AlertText_Dispose()
        {
            IAlert jsAlertPopup = driver.SwitchTo().Alert();
            jsAlertPopup.Accept();
        }

        [Fact]
        public void AlertConfirmation()
        {
            jsAlertBtn.Click();

            IAlert jsAlertPopup = driver.SwitchTo().Alert();
            jsAlertPopup.Accept();

            Assert.Throws<NoAlertPresentException>(() => jsAlertPopup = driver.SwitchTo().Alert());
            Assert.Equal("You successfuly clicked an alert", resultText.Text);
        }

        [Fact]
        public void ConfirmationAlertCancel()
        {
            jsConfirmBtn.Click();

            IAlert jsAlertPopup = driver.SwitchTo().Alert();
            jsAlertPopup.Dismiss();

            Assert.Throws<NoAlertPresentException>(() => jsAlertPopup = driver.SwitchTo().Alert());
            Assert.Equal("You clicked: Cancel", resultText.Text);
        }

        [Fact]
        public void ConfirmationAlertConfirm()
        {
            jsConfirmBtn.Click();

            IAlert jsAlertPopup = driver.SwitchTo().Alert();
            jsAlertPopup.Accept();

            Assert.Throws<NoAlertPresentException>(() => jsAlertPopup = driver.SwitchTo().Alert());
            Assert.Equal("You clicked: Ok", resultText.Text);
        }

        [Fact]
        public void PromptInsertValue()
        {
            jsPromptBtn.Click();

            IAlert jsAlertPopup = driver.SwitchTo().Alert();
            jsAlertPopup.SendKeys("Testing Alerts!");

            jsAlertPopup.Accept();

            Assert.Equal("You entered: Testing Alerts!", resultText.Text);
        }

    }
}
