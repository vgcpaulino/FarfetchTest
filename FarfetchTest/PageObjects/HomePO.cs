using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using FarfetchSeleniumTest.Helpers;
using Xunit.Abstractions;

namespace FarfetchSeleniumTest.PageObjects
{
    public class HomePO
    {
        private readonly IWebDriver driver;
        private readonly TestWait wait;
        private readonly UrlLinks links;
        private readonly CookiesHelper cookie;
        private readonly ElementHelper element;

        private readonly By byIconLogin;
        public readonly By byUserDetailName;
        private readonly By byUserName;
        private readonly By byGreetingMessage;
        private readonly By byBtnExit;
        private readonly By byBtnLogin;
        private readonly By byDetailsDrawer;
        private readonly By byGenderMaleFilter;
        private readonly By byGenderMaleClothingFilter;
        private readonly By byNewsLetterCloseBtn;
        private readonly By bySideMenu;

        public HomePO(IWebDriver driver)
        {
            this.driver = driver;
            wait = new TestWait(driver);
            links = new UrlLinks();
            cookie = new CookiesHelper(driver);
            element = new ElementHelper(driver);

            byIconLogin = By.ClassName("icon-user");
            byUserDetailName = By.Id("ff-details-account");
            byUserName = By.ClassName("js-details-account-name");
            byGreetingMessage = By.XPath("//a[@href='/useraccount.aspx?ffref=nb_name']");
            byBtnExit = By.XPath("//a[@href='/br/account/logout?ffref=hd_lidd_so']");
            byBtnLogin = By.XPath("//a[@href='/br/login.aspx']");
            byDetailsDrawer = By.Id("ff-details-drawer");
            byGenderMaleFilter = By.XPath("//a[@href='/br/shopping/men/items.aspx?ffref=hd_mnav']");
            byGenderMaleClothingFilter = By.XPath("//a[@href='/br/shopping/men/clothing-2/items.aspx']");
            byNewsLetterCloseBtn = By.CssSelector("button[data-test=Go_NewsletterModalCloseButton]");
            bySideMenu = By.CssSelector("a[data-test='ff-sidenav']");
        }

        
        public IWebElement LoginIcon => element.TryFindElement(byIconLogin);
        public IWebElement UserDetailName => element.TryFindElement(byUserDetailName);
        public IWebElement UserName => element.TryFindElement(byUserName);
        public IWebElement GreetingMessage => element.TryFindElement(byGreetingMessage);
        public IWebElement BtnExit => element.TryFindElement(byBtnExit);
        public IWebElement BtnLogin => element.TryFindElement(byBtnLogin);
        public IWebElement DetailsDrawer => element.TryFindElement(byDetailsDrawer);
        public IWebElement GenderMaleFilter => element.TryFindElement(byGenderMaleFilter);
        public IWebElement GenderMaleClothingFilter => element.TryFindElement(byGenderMaleClothingFilter);
        public IWebElement NewsLetterCloseBtn => element.TryFindElement(byNewsLetterCloseBtn);
        public IWebElement SideMenu => element.TryFindElement(bySideMenu);

        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl(links.HomeUrl);

            DateTime time = new DateTime(2050, 01, 01, 23, 59, 00);
            List<Cookie> listOfCookies = new List<Cookie>() {
                new Cookie("ff_newsletter", "1", "farfetch.com", "/", time),
                new Cookie("smClosePushOptin", "true", "www.farfetch.com", "/", time),
                new Cookie("smCloseBounce", "true", "www.farfetch.com", "/", time)
            };
            cookie.AddCookies(listOfCookies);
        }
        
        private void WaitDetailDrawer()
        {
            wait.WaitElementInteractable(10, byDetailsDrawer);
        }

        private void WaitDetailDrawerOptions()
        {
            wait.WaitElementClickable(10, byBtnExit);
        }

        public void LogoutUser()
        {
            UserName.Click();
            WaitDetailDrawer();
            WaitDetailDrawerOptions();
            BtnExit.Click();
        }

        public bool UserIsLogged()
        {
            string fullNameText = UserName.GetAttribute("innerHTML");
            string greetingMessageText = GreetingMessage.GetAttribute("text");

            return UserDetailName.Displayed
                && (fullNameText == "Vinicius Gabriel Cabral Paulino")
                && GreetingMessage.Displayed
                && (greetingMessageText == "Bem-vindo(a) de volta, Vinicius");
        }

        public void CloseNewsLetterAd()
        {
            wait.WaitElementExists(10, byNewsLetterCloseBtn);
            NewsLetterCloseBtn.Click();
        }
    }
}
