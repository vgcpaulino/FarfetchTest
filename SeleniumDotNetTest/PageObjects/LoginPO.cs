using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using SeleniumDotNetTest.Helpers;
using Xunit.Abstractions;

namespace SeleniumDotNetTest.PageObjects
{
    public class LoginPO
    {

        private readonly IWebDriver driver;
        private readonly TestWait wait;
        private readonly InputHelper helper;
        private readonly UrlLinks links;
        private readonly ElementHelper element;

        private readonly By byDivLoginTitle;
        private readonly By byInputEmail;
        private readonly By byInputPassword;
        private readonly By byBtnLogin;
        private readonly By bySpanRequiredLogin;
        private readonly By bySpanRequiredPassword;
        private readonly By bySpanWrongEmailPassword;
        private readonly By byRememberMeChkBox;
        
        public LoginPO(IWebDriver driver)
        {
            this.driver = driver;
            wait = new TestWait(driver);
            helper = new InputHelper();
            links = new UrlLinks();
            element = new ElementHelper(this.driver);

            byDivLoginTitle = By.Id("login");
            byInputEmail = By.Id("email-input-login");
            byInputPassword = By.CssSelector("input[data-tstid='TextBox_Pass_Login']");
            byBtnLogin = By.CssSelector("div.submit-button");
            bySpanRequiredLogin = By.CssSelector("span[for=email-input-login]"); 
            bySpanRequiredPassword = By.CssSelector("span[for=password-input-login]");
            bySpanWrongEmailPassword = By.CssSelector("span[id=js-passwordValidationMessage]");
            byRememberMeChkBox = By.CssSelector("label[for=RememberMe]");
        }

        public IWebElement LoginTitle => element.TryFindElement(byDivLoginTitle);
        public IWebElement InputEmail => element.TryFindElement(byInputEmail);
        public IWebElement InputPassword => element.TryFindElement(byInputPassword);
        public IWebElement BtnLogin => element.TryFindElement(byBtnLogin);
        public IWebElement HintRequiredLogin => element.TryFindElement(bySpanRequiredLogin);
        public IWebElement HintRequiredPassword => element.TryFindElement(bySpanRequiredPassword);
        public IWebElement HintWrongEmailPassword => element.TryFindElement(bySpanWrongEmailPassword);
        private IWebElement RememberMeChkBox => element.TryFindElement(byRememberMeChkBox);

        public void GoToLoginUrl()
        {
            driver.Navigate().GoToUrl(links.LoginUrl);
        }

        public bool PageIsOpened()
        {
            wait.WaitElementExists(10, byDivLoginTitle);
            return (links.LoginUrl == driver.Url) && LoginTitle.Displayed;
        }

        public bool HintLoginNoInputIsDisplayed()
        {
            return HintRequiredLogin.Displayed && (HintRequiredLogin.Text == "Por favor, digite o seu e-mail") 
                && HintRequiredPassword.Displayed && (HintRequiredPassword.Text == "Por favor, digite a sua senha");
        }

        public bool HintLoginInvalidInputIsDisplayed()
        {
            return HintWrongEmailPassword.Displayed && (HintWrongEmailPassword.Text == "Email ou senha incorreto.");
        }

        public void LoginTypeInfo(string email, string password, bool keepConnected)
        {
            helper.ClearInputAndType(InputEmail, email);
            helper.ClearInputAndType(InputPassword, password);

            if (!keepConnected && RememberMeChkBox.Selected)
            {
                RememberMeChkBox.Click();
            }
        }

        public void WaitLoginPage()
        {
            wait.WaitElementInteractable(5, byDivLoginTitle);
        }

        public void WaitLoginValidation() 
        {
            wait.WaitElementInteractable(5, bySpanWrongEmailPassword);
        }

        public void WaitLoginBeingDone()
        {
            HomePO home = new HomePO(driver);
            wait.WaitElementInteractable(50, home.byUserDetailName);
        }
    }
}
