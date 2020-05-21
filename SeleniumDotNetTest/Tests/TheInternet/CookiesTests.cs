using OpenQA.Selenium;
using SeleniumDotNetTest.Fixtures;
using SeleniumDotNetTest.Helpers;
using System.Collections.Generic;
using Xunit;

namespace SeleniumDotNetTest.Tests.TheInternet
{
    public class CookiesTests : IClassFixture<TestFixture>
    {
        
        private readonly IWebDriver driver;

        private readonly CookiesPO cookiesPO;

        public CookiesTests(TestFixture fixture)
        {
            driver = fixture.Driver;          
            cookiesPO = new CookiesPO(driver);

            cookiesPO.DeleteCookie();
            cookiesPO.OpenSite();
        }

        [Fact]
        public void HeaderMustDisplayExpectedTitle()
        {
            List<string> expectedStrings = new List<string>() { "A/B Test Variation 1", "A/B Test Control" };

            string actualSystemString = cookiesPO.HeaderTitle.Text;

#pragma warning disable xUnit2017 // Do not use Contains() to check if a value exists in a collection
            Assert.True(expectedStrings.Contains(actualSystemString));
#pragma warning restore xUnit2017 // Do not use Contains() to check if a value exists in a collection
        }

        [Fact]
        public void HeaderMustDisplayCookieTitle()
        {
            cookiesPO.AddCookieAndRefreshPage();

            string actualSystemString = cookiesPO.HeaderTitle.Text;

            Assert.Contains("No A/B Test", actualSystemString);
        }
    }


    public class CookiesPO
    {

        private readonly IWebDriver driver;
        private readonly CookiesHelper cookies;
        private readonly ElementHelper element;

        private readonly By byHeaderTitle;

        public CookiesPO(IWebDriver driver)
        {
            this.driver = driver;
            cookies = new CookiesHelper(driver);
            element = new ElementHelper(this.driver);

            byHeaderTitle = By.TagName("h3");
        }

        public IWebElement HeaderTitle => element.TryFindElement(byHeaderTitle);


        public void OpenSite()
        {
            driver.Navigate().GoToUrl(TestingUrls.CookiesUrl);
        }

        public void DeleteCookie()
        {
            cookies.DeleteCookieByName("optimizelyOptOut");
        }

        public void AddCookieAndRefreshPage()
        {
            cookies.CreateCookie("optimizelyOptOut", "true");
            driver.Navigate().Refresh();
        }
    }

}
