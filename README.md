# SeleniumDotNetTest

## Description:
Project created for the purpose of testing Selenium using C# and .Net Core. The project is prepared to run in Docker containers using the Selenium images (Selenium Grid) and Jenkins (manual configuration);

**A production website it's used in most of the tests, some tests may fail due to data changes.**

## Jenkins Pipeline:
The Jenkins job must be manually configured, the pipeline script will execute the following steps:
- Check out the repository;
- Download and install .Net Core;
- Download and install NuGet;
- Build and restore the project;
- Build the Docker Compose file;
- Execute the tests and publish the testing report in Jenkins;

## Packages:
- [Xunit](https://github.com/xunit/xunit)
- [Xunit Test Logger](https://github.com/spekt/xunit.testlogger)
- [Selenium WebDriver](https://github.com/SeleniumHQ/selenium)
- [DotNet Selenium Extras](https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras)
- [Selenium WebDriver Manager](https://github.com/rosolko/WebDriverManager.Net)

