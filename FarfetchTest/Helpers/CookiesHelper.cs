using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FarfetchSeleniumTest.Helpers
{
    public class CookiesHelper
    {
        private readonly IWebDriver driver;

        public CookiesHelper(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void AddCookies(dynamic cookieOrListOfCookies)
        {
            Type type = cookieOrListOfCookies.GetType(); 
            switch (type.Namespace)
            {
                case "System.Collections.Generic":
                    foreach (Cookie ck in cookieOrListOfCookies)
                    {
                        driver.Manage().Cookies.AddCookie(ck);
                    }
                    break;
                case "OpenQA.Selenium":
                    driver.Manage().Cookies.AddCookie(cookieOrListOfCookies);
                    break;
            }
        }

        public void DeleteCookieByName(string cookieName)
        {
            driver.Manage().Cookies.DeleteCookieNamed(cookieName);
        }

        public void DeleteAllCookies()
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }

        public List<string> GetListOfCookies()
        {
            ReadOnlyCollection<Cookie> allCookies = driver.Manage().Cookies.AllCookies;
            List<string> allCookiesList = new List<string>(){ };

            foreach (Cookie ck in allCookies)
            {
                allCookiesList.Add(ck.Name);
            }

            return allCookiesList;
        }

        

    }
}
