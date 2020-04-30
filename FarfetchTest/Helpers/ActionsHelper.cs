using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;


namespace FarfetchSeleniumTest.Helpers
{
    public class ActionsHelper
    {

        private readonly Actions action;
        
        public ActionsHelper(IWebDriver driver)
        {
            action = new Actions(driver);
        }

        private void PerformAction()
        {
            action.Perform();
        }

        public void MoveToElement(IWebElement element)
        {
            action.MoveToElement(element);
            PerformAction();
        }

        public void MoveToElementEndY(IWebElement element)
        {
            action.MoveToElement(element).MoveByOffset(0, element.Size.Width);
            PerformAction();
        }

    }
}
