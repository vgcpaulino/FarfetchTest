using FarfetchSeleniumTest.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SeleniumTests.Drivers;
using System;

namespace FarfetchSeleniumTest.Fixtures
{
    
    public class TestFixture : IDisposable
    {
        
        public IWebDriver Driver { get; private set; }
        private DriverSetUp driverSetUp;
        private WindowManagerHelper window;
        private EnvironmentHelper env;

        public TestFixture()
        {
            CreateWebDriver();
        }

        public void CreateWebDriver()
        {
            driverSetUp = new DriverSetUp();
            driverSetUp.SetUpDrivers();

            //ChromeOptions options = new ChromeOptions();
            //options.AddArguments("--whitelisted-ips=''");
            //Driver = new ChromeDriver(driverSetUp.ChromeDriverFolder, options);

            // The environment variable it's created through the Jenkinsfile
            env = new EnvironmentHelper();
            string executingCI = env.GetEnvVariableValue("CI_EXECUTION");
            if (executingCI == "" || executingCI == null)
            {
                Driver = new ChromeDriver(driverSetUp.ChromeDriverFolder);
            }
            else
            {
                ChromeOptions chromeOpt = new ChromeOptions();
                Driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), chromeOpt);
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
