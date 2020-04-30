using FarfetchSeleniumTest.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarfetchSeleniumTest.PageObjects
{
    public class NotifyAvailablePO
    {

        private readonly IWebDriver driver;
        private readonly TestWait wait;

        private readonly By byForm;
        private readonly By byEmailInput;
        private readonly By byInvalidEmail;
        private readonly By bySizeDiv;
        private readonly By bySubmitBtn;

        public NotifyAvailablePO (IWebDriver driver)
        {
            this.driver = driver;
            wait = new TestWait(driver);

            byForm = By.CssSelector("form[data-tstid=formToNotify]");
            byEmailInput = By.CssSelector("input[data-tstid=email]");
            byInvalidEmail = By.CssSelector("div[data-tstid=emailError]");
            bySizeDiv = By.CssSelector("div[data-tstid=sizeDropDown");
            bySubmitBtn = By.CssSelector("button[data-tstid=submit]");
        }

        public IWebElement NotifyAvailableForm => driver.FindElement(byForm);
        public IWebElement EmailInput => driver.FindElement(byEmailInput);
        public IWebElement InvalidEmail => driver.FindElement(byInvalidEmail);
        public IWebElement SizeDiv => driver.FindElement(bySizeDiv);
        public IWebElement SubmitBtn => driver.FindElement(bySubmitBtn);

        public bool CheckNotifyAvailableFormVisivble()
        {
            wait.WaitElementExists(5, byForm);
            wait.WaitElementInteractable(1, byForm);
            return NotifyAvailableForm.Displayed;
        }

        public bool OutOfStockNotificationIsOpened()
        {
            return CheckNotifyAvailableFormVisivble()
                && EmailInput.Displayed
                && SizeDiv.Displayed
                && SubmitBtn.Displayed;
        }

        public bool InvalidEmailIsShown()
        {
            return InvalidEmail.Displayed
                && ("Por favor, digite um email válido" == InvalidEmail.Text);
        }
    }
}
