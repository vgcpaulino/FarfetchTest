using OpenQA.Selenium;
using SeleniumDotNetTest.Fixtures;
using SeleniumDotNetTest.Helpers;
using Xunit;

namespace SeleniumDotNetTest.Tests.TheInternet
{
    public class FileUploadTests : IClassFixture<TestFixture>
    {

        private readonly IWebDriver driver;
        private readonly FileUploadPO fileUploadPO;
        public FileUploadTests(TestFixture fixture)
        {
            driver = fixture.Driver;
            fileUploadPO = new FileUploadPO(driver);

            fileUploadPO.OpenSite();
        }

        [Fact]
        public void MustPresentFileUploadConfirmation()
        {
            fileUploadPO.UploadFiles();

            Assert.True(fileUploadPO.VerifyUploadedFile());
        }

    }

    public class FileUploadPO
    {

        private readonly IWebDriver driver;
        private readonly ElementHelper element;
        private readonly Paths paths;
        private const string FileName = "TextFile001.txt";

        private readonly By byChooseFileBtn;
        private readonly By byUploadBtn;
        private readonly By byHeaderTitle;
        private readonly By byDivFileNames;

        public FileUploadPO(IWebDriver driver)
        {
            this.driver = driver;
            element = new ElementHelper(driver);
            paths = new Paths();

            byChooseFileBtn = By.Id("file-upload");
            byUploadBtn = By.Id("file-submit");
            byHeaderTitle = By.TagName("h3");
            byDivFileNames = By.Id("uploaded-files");
        }

        public IWebElement ChooseFileBtn => element.TryFindElement(byChooseFileBtn);
        public IWebElement UploadBtn => element.TryFindElement(byUploadBtn);
        public IWebElement HeaderTitle => element.TryFindElement(byHeaderTitle);
        public IWebElement FileNameDiv => element.TryFindElement(byDivFileNames);

        public void OpenSite()
        {
            driver.Navigate().GoToUrl(TestingUrls.FileUpload);
        }

        public void UploadFiles()
        {
            string stringProjectPath = paths.projectResourcesFilesPath;
            string filePath = $"{stringProjectPath}\\{FileName}";

            ChooseFileBtn.SendKeys(filePath);
            UploadBtn.Click();
        }

        public bool VerifyUploadedFile()
        {

            if (HeaderTitle.Text != "File Uploaded!")
            {
                return false;
            }

            if (FileNameDiv.Text != $"{FileName}")
            {
                return false;
            }

            return true;
        }
    }

}
