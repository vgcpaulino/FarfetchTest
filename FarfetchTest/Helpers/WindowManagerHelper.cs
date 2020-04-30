using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarfetchSeleniumTest.Helpers
{
    public class WindowManagerHelper
    {

        private readonly IWebDriver Driver;

        public WindowManagerHelper(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public void SetWindowSize(int width, int height, int positionX, int positionY)
        {
            Driver.Manage().Window.Size = new System.Drawing.Size(width, height);

            if (positionX > 0 && positionX > 0)
            {
                SetWindowPosition(positionX, positionY);
            }
        }

        public void SetWindowMaximized()
        {
            Driver.Manage().Window.Maximize();
        }

        private void SetWindowPosition(int positionX, int positionY)
        {
            Driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
        }

    }
}
