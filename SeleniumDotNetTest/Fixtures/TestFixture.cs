using SeleniumDotNetTest.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SeleniumTests.Drivers;
using System;

namespace SeleniumDotNetTest.Fixtures
{
    
    public class TestFixture : IDisposable
    {
        
        public IWebDriver Driver { get; private set; }
        private DriverSetUp driverSetUp;
        private WindowManagerHelper window;
        private EnvironmentHelper env;
        private readonly Paths paths;

        public TestFixture()
        {
            paths = new Paths();

            CreateWebDriver();
        }

        public void CreateWebDriver()
        {
            driverSetUp = new DriverSetUp();
            driverSetUp.SetUpDrivers();

            ChromeOptions chromeOpt = new ChromeOptions();
            //opchromeOpttions.AddArguments("--whitelisted-ips=''");
            string downloadFolder = paths.solutionRuntimeFilesPath;
            chromeOpt.AddUserProfilePreference("download.default_directory", downloadFolder); // Set another directory when downloading a file;
            //chromeOpt.AddUserProfilePreference("download.prompt_for_download", true); // Ask directory when downloading a file;

            // The environment variable it's created through the Jenkinsfile
            env = new EnvironmentHelper();
            string executingCI = env.GetEnvVariableValue("CI_EXECUTION");
            if (executingCI == "" || executingCI == null)
            {
                Driver = new ChromeDriver(driverSetUp.ChromeDriverFolder, chromeOpt);
            }
            else
            {
                Driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), chromeOpt.ToCapabilities(), TimeSpan.FromSeconds(120));
            }

            window = new WindowManagerHelper(Driver);
            window.SetWindowMaximized();
        }

        public void Dispose()
        {
            Driver.Quit();
        }

    }
}
