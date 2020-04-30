using System.IO;
using System.Reflection;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumTests.Drivers
{
    public class DriverSetUp
    {

        private static string PathExe => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); 
        private readonly string chromeVersion = "80.0.3987.106";
        public string ChromeDriverFolder => PathExe + $"\\Chrome\\{chromeVersion}\\X64";
        private string ChromeBinaryPath => $"{ChromeDriverFolder}\\chromedriver.exe";

        // This method uses the WebDriverManager package to download the browser drivers;
        // DOC: https://github.com/rosolko/WebDriverManager.Net#usage
        public void SetUpDrivers()
        {
            // To avoid download the driver every execution;
            if (!Directory.Exists(ChromeDriverFolder) || !File.Exists(ChromeBinaryPath))
            {
                new DriverManager().SetUpDriver(new ChromeConfig(), chromeVersion, Architecture.X64);
            }  
        }

        

    }
}
