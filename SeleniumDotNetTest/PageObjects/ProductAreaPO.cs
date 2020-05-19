using SeleniumDotNetTest.Helpers;
using MongoDB.Bson.Serialization.Serializers;
using OpenQA.Selenium;

namespace SeleniumDotNetTest.PageObjects
{
    public class ProductAreaPO
    {

        private readonly IWebDriver driver;
        private readonly UrlLinks links;
        private readonly ElementHelper element;

        private readonly By byProductCardList;
        private readonly By byHeaderTitle;


        public ProductAreaPO(IWebDriver driver)
        {
            this.driver = driver;
            links = new UrlLinks();
            element = new ElementHelper(this.driver);

            byProductCardList = By.CssSelector("ul[data-test=product-card-list");
            byHeaderTitle = By.CssSelector("h1[data-test=header-title");
        }

        public IWebElement ProductCardList => element.TryFindElement(byProductCardList);
        public IWebElement HeaderTitle => element.TryFindElement(byHeaderTitle);

        public bool MenClothingIsOpened()
        {
            return (links.MenCloathingUrl == driver.Url) && ("ROUPAS MASCULINAS" == HeaderTitle.Text);
        }


    }
}
