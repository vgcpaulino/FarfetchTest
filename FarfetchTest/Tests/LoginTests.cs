using OpenQA.Selenium;
using FarfetchSeleniumTest.PageObjects;
using Xunit;
using FarfetchSeleniumTest.Fixtures;
using FarfetchSeleniumTest.Helpers;

namespace FarfetchSeleniumTest.Tests
{
    
    //[Collection("Chrome Driver")]
    public class LoginTests : IClassFixture<TestFixture>
    {
        private readonly IWebDriver driver;
        private readonly LoginPO login;
        private readonly HomePO home;

        public LoginTests(TestFixture fixture)
        {
            this.driver = fixture.Driver;
            login = new LoginPO(driver);
            home = new HomePO(driver);
        }

        [Fact]
        public void MustAlertLackOfInformation()
        {
            // ARRANGE: When the user is at Login page;
            login.GoToLoginUrl();

            // ACT: When the user click at Login button without information;
            login.LoginTypeInfo("", "", false);
            login.BtnLogin.Click();

            // ASSERT: Hints must be displayed asking to fill email and password;
            login.WaitLoginValidation();
            Assert.True(login.HintLoginNoInputIsDisplayed());
        }

        [Fact]
        public void MustAlertWrongCredentials()
        {
            // ARRANGE: When the user is at Login page;
            login.GoToLoginUrl();

            // ACT: When the user type invalid credentials and click at Login button;
            login.LoginTypeInfo("test@test.com.br", "NoPassword", false);
            login.BtnLogin.Click();

            // ASSERT: Hint must be displayed alerting wrong credentials;
            login.WaitLoginValidation();    
            Assert.True(login.HintLoginInvalidInputIsDisplayed());
        }

        [Fact (Skip = "No public test credentials.")]
        public void MustAcceptValidCredentials()
        {
            // ARRANGE: When the user is at Login page;
            login.GoToLoginUrl();

            // ACT: When the user type valid credentials and click at Login button;
            login.LoginTypeInfo("test@gmail.com", "123", false);
            login.BtnLogin.Click();
            login.WaitLoginBeingDone();

            // ASSERT: The Home page must be displayed with the user logged;
            Assert.True(home.UserIsLogged());
        }

        [Fact(Skip = "No public test credentials.")]
        public void MustKeepUserLogged()
        {
            // ARRANGE: When the user is at Login page;
            login.GoToLoginUrl();

            // ACT: When the user type valid credentials and click at Login button with the "remember me" option selected;
            login.LoginTypeInfo("test@gmail.com", "123", true);
            login.BtnLogin.Click();
            login.WaitLoginBeingDone();

            // ASSERT: If the user open and closed and open the Home page again, the user will still logged;
            driver.Navigate().GoToUrl("http://localhost:80");
            home.OpenHomePage();

            // ASSERT: The Home page must be displayed with the user logged;
            Assert.True(home.UserIsLogged());
        }



    }


}
