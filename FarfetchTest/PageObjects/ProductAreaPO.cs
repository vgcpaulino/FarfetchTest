using FarfetchSeleniumTest.Helpers;
using OpenQA.Selenium;

namespace FarfetchSeleniumTest.PageObjects
{
    public class ProductAreaPO
    {

        private readonly IWebDriver driver;
        private readonly UrlLinks links;

        private readonly By byProductCardList;
        private readonly By byHeaderTitle;


        public ProductAreaPO(IWebDriver driver)
        {
            this.driver = driver;
            links = new UrlLinks();

            byProductCardList = By.CssSelector("ul[data-test=product-card-list");
            byHeaderTitle = By.CssSelector("h1[data-test=header-title");
        }

        public IWebElement ProductCardList => driver.FindElement(byProductCardList);
        public IWebElement HeaderTitle => driver.FindElement(byHeaderTitle);

        public bool MenClothingIsOpened()
        {
            return (links.MenCloathingUrl == driver.Url) && ("ROUPAS MASCULINAS" == HeaderTitle.Text);
        }


    }
}
