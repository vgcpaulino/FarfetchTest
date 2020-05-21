using OpenQA.Selenium;
using SeleniumDotNetTest.Fixtures;
using SeleniumDotNetTest.Helpers;
using Xunit;

namespace SeleniumDotNetTest.Tests.TheInternet
{
    public class BrowserTabsTests : IClassFixture<TestFixture>
    {
        
        private readonly IWebDriver driver;
        private readonly BrowserTabsPO browserTabsPO;

        public BrowserTabsTests(TestFixture fixture)
        {
            this.driver = fixture.Driver;
            browserTabsPO = new BrowserTabsPO(this.driver);

            browserTabsPO.OpenSite();
        }
        
        [Fact]
        public void MustBePossibleSwithBackFirstTab()
        {
            browserTabsPO.ClickHereLink.Click(); 
            browserTabsPO.SelectHomeBrowserTab();

            Assert.Contains("The Internet", driver.Title);
        }
    }

    public class BrowserTabsPO
    {
        private readonly IWebDriver driver;
        private readonly ElementHelper element;
        private readonly TabHelper tab;

        private readonly By byClickHereLink;

        public BrowserTabsPO(IWebDriver driver)
        {
            this.driver = driver;
            element = new ElementHelper(this.driver);
            tab = new TabHelper(this.driver);

            byClickHereLink = By.PartialLinkText("Click Here");
        }

        public IWebElement ClickHereLink => element.TryFindElement(byClickHereLink);

        public void OpenSite()
        {
            driver.Navigate().GoToUrl(TestingUrls.BrowserTabs);
        }

        public void SelectHomeBrowserTab()
        {
            tab.SelectTabByPageTitle("The Internet");
        }
    }
}
