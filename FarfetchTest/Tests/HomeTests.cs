using FarfetchSeleniumTest.Fixtures;
using FarfetchSeleniumTest.Helpers;
using FarfetchSeleniumTest.PageObjects;
using OpenQA.Selenium;
using Xunit;

namespace FarfetchSeleniumTest.Tests
{
    
    //[Collection("Chrome Driver")]
    public class HomeTests : IClassFixture<TestFixture>
    {

        private readonly IWebDriver driver;
        private readonly UrlLinks links;
        private readonly HomePO home;
        private readonly LoginPO login;

        public HomeTests(TestFixture fixture)
        {
            this.driver = fixture.Driver;
            links = new UrlLinks();
            home = new HomePO(driver);
            login = new LoginPO(driver);
        }

        [Fact]
        public void MustHaveLinkToLoginPage()
        {
            // ARRANGE: When the user is at Home Page;
            home.OpenHomePage();
 
            // ASSERT: The Login icon must be displayed;
            Assert.True(home.LoginIcon.Displayed);
        }

        [Fact]
        public void LoginIconMustRedirectToLoginPage()
        {
            // ARRANGE: When the user is at Home page;
            home.OpenHomePage();

            // ACT: And clicks on the Login icon;
            home.LoginIcon.Click();

            // ASSERT: The Login page must be opened;
            Assert.True(login.PageIsOpened());
        }
    }
}
