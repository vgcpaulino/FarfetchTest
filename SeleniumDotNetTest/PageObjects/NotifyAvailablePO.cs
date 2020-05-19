using SeleniumDotNetTest.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumDotNetTest.PageObjects
{
    public class NotifyAvailablePO
    {

        private readonly IWebDriver driver;
        private readonly TestWait wait;
        private readonly ElementHelper element;

        private readonly By byForm;
        private readonly By byEmailInput;
        private readonly By byInvalidEmail;
        private readonly By bySizeDiv;
        private readonly By bySubmitBtn;

        public NotifyAvailablePO (IWebDriver driver)
        {
            this.driver = driver;
            wait = new TestWait(driver);
            element = new ElementHelper(this.driver);

            byForm = By.CssSelector("form[data-tstid=formToNotify]");
            byEmailInput = By.CssSelector("input[data-tstid=email]");
            byInvalidEmail = By.CssSelector("div[data-tstid=emailError]");
            bySizeDiv = By.CssSelector("div[data-tstid=sizeDropDown");
            bySubmitBtn = By.CssSelector("button[data-tstid=submit]");
        }

        public IWebElement NotifyAvailableForm => element.TryFindElement(byForm);
        public IWebElement EmailInput => element.TryFindElement(byEmailInput);
        public IWebElement InvalidEmail => element.TryFindElement(byInvalidEmail);
        public IWebElement SizeDiv => element.TryFindElement(bySizeDiv);
        public IWebElement SubmitBtn => element.TryFindElement(bySubmitBtn);

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
