using OpenQA.Selenium;
using SeleniumDotNetTest.Fixtures;
using SeleniumDotNetTest.Helpers;
using Xunit;

namespace SeleniumDotNetTest.Tests.TheInternet
{
    public class AlertTests : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly AlertPO alertPO;

        public AlertTests(TestFixture fixture)
        {
            this.driver = fixture.Driver;
            alertPO = new AlertPO(this.driver);

            alertPO.OpenSite();
        }

        [Fact]
        public void AlertText()
        {
            alertPO.JsAlertBtn.Click();

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
            alertPO.JsAlertBtn.Click();

            IAlert jsAlertPopup = driver.SwitchTo().Alert();
            jsAlertPopup.Accept();

            Assert.Throws<NoAlertPresentException>(() => jsAlertPopup = driver.SwitchTo().Alert());
            Assert.Equal("You successfuly clicked an alert", alertPO.ResultText.Text);
        }

        [Fact]
        public void ConfirmationAlertCancel()
        {
            alertPO.JsConfirmBtn.Click();

            IAlert jsAlertPopup = driver.SwitchTo().Alert();
            jsAlertPopup.Dismiss();

            Assert.Throws<NoAlertPresentException>(() => jsAlertPopup = driver.SwitchTo().Alert());
            Assert.Equal("You clicked: Cancel", alertPO.ResultText.Text);
        }

        [Fact]
        public void ConfirmationAlertConfirm()
        {
            alertPO.JsConfirmBtn.Click();

            IAlert jsAlertPopup = driver.SwitchTo().Alert();
            jsAlertPopup.Accept();

            Assert.Throws<NoAlertPresentException>(() => jsAlertPopup = driver.SwitchTo().Alert());
            Assert.Equal("You clicked: Ok", alertPO.ResultText.Text);
        }

        [Fact]
        public void PromptInsertValue()
        {
            alertPO.JsPromptBtn.Click();

            IAlert jsAlertPopup = driver.SwitchTo().Alert();
            jsAlertPopup.SendKeys("Testing Alerts!");

            jsAlertPopup.Accept();

            Assert.Equal("You entered: Testing Alerts!", alertPO.ResultText.Text);
        }

    }

    public class AlertPO
    {
        private readonly IWebDriver driver;
        private readonly ElementHelper element;

        private readonly By byJsAlertBtn;
        private readonly By byJsConfirmBtn;
        private readonly By byJsPromptBtn;
        private readonly By byResultText;

        public AlertPO(IWebDriver driver)
        {
            this.driver = driver;
            element = new ElementHelper(this.driver);

            byJsAlertBtn = By.CssSelector("button[onclick='jsAlert()']");
            byJsConfirmBtn = By.CssSelector("button[onclick='jsConfirm()']");
            byJsPromptBtn = By.CssSelector("button[onclick='jsPrompt()']");
            byResultText = By.Id("result");
        }

        public IWebElement JsAlertBtn => element.TryFindElement(byJsAlertBtn);
        public IWebElement JsConfirmBtn => element.TryFindElement(byJsConfirmBtn);
        public IWebElement JsPromptBtn => element.TryFindElement(byJsPromptBtn);
        public IWebElement ResultText => element.TryFindElement(byResultText);

        public void OpenSite()
        {
            driver.Navigate().GoToUrl(TestingUrls.AlertUrl);
        }
    }
}
