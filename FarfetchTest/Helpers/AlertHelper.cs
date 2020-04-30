using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarfetchSeleniumTest.Helpers
{
    public class AlertHelper
    {
        private readonly IWebDriver driver;
        private IAlert alertPopup;

        public AlertHelper(IWebDriver driver)
        {
            this.driver = driver;
            GetAlert();
        }

        internal void GetAlert()
        {
            alertPopup = driver.SwitchTo().Alert();
        }

        public void ConfirmationAlertAccept()
        {
            alertPopup.Accept();
        }

        public void ConfirmationAlertDismiss()
        {
            alertPopup.Dismiss();
        }

        public void PromptAlertInput(string text)
        {
            alertPopup.SendKeys(text);
        }

    }
}
