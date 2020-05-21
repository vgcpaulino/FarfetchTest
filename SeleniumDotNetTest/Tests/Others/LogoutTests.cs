using SeleniumDotNetTest.Fixtures;
using SeleniumDotNetTest.PageObjects;
using OpenQA.Selenium;
using Xunit;

namespace SeleniumDotNetTest.Tests.Others
{
    //[Collection("Chrome Driver")]
    public class LogoutTests : IClassFixture<TestFixture>
    {

        private readonly IWebDriver driver;
        private readonly LoginPO login;
        private readonly HomePO home;

        public LogoutTests(TestFixture fixture)
        {
            this.driver = fixture.Driver;
            login = new LoginPO(driver);
            home = new HomePO(driver);
        }

        [Fact(Skip = "No public test credential.")]
        public void MustAllowLogout()
        {
            // ARRANGE: When the user is at the Home page with the user logged in;
            login.GoToLoginUrl();
            login.LoginTypeInfo("testgmail.com", "123", false);
            login.BtnLogin.Click();
            login.WaitLoginBeingDone();

            // ACT: When the user clicks at the Logout button;
            home.LogoutUser();

            // ASSERT: The the Home page will be displayed with the Login icon;
            Assert.True(home.BtnLogin.Displayed);
        }

    }
}
