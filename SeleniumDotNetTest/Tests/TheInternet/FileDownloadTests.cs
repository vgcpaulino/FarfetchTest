using OpenQA.Selenium;
using SeleniumDotNetTest.Fixtures;
using SeleniumDotNetTest.Helpers;
using Xunit;

namespace SeleniumDotNetTest.Tests.TheInternet
{
    public class FileDownloadTests : IClassFixture<TestFixture>
    {

        private readonly IWebDriver driver;
        private readonly FileDownloadPO fileDownloadPO;

        public FileDownloadTests(TestFixture fixture)
        {
            driver = fixture.Driver;
            fileDownloadPO = new FileDownloadPO(driver);

            fileDownloadPO.OpenSite();
        }

        [Fact]
        public void MustPresentFileAfterDownload()
        {            
            fileDownloadPO.SomeFileLink.Click();

            Assert.True(fileDownloadPO.CheckFileDownloaded("some-file.txt"));
        }
    }

    public class FileDownloadPO
    {

        private readonly IWebDriver driver;
        private readonly ElementHelper element;
        private readonly FileHelper file;
        private readonly Paths paths;

        private readonly By bySomeFile;
        
        public FileDownloadPO(IWebDriver driver)
        {
            this.driver = driver;
            element = new ElementHelper(this.driver);
            file = new FileHelper();
            paths = new Paths();

            bySomeFile = By.PartialLinkText("some-file.txt");
        }

        public void OpenSite()
        {
            driver.Navigate().GoToUrl(TestingUrls.FileDownload);
        }

        public bool CheckFileDownloaded(string fileName)
        {
            string fullPath = paths.solutionRuntimeFilesPath + $"\\{fileName}";
            return file.FileExists(fullPath);
        }

        public IWebElement SomeFileLink => element.TryFindElement(bySomeFile);

    }
}
