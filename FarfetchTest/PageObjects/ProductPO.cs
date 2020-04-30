using FarfetchSeleniumTest.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarfetchSeleniumTest.PageObjects
{
    public class ProductPO
    {

        private readonly IWebDriver driver;
        private readonly UrlLinks links;
        private readonly StringHelper strHelper;
        private readonly ConvertHelper convert;

        private readonly By byProductGallery;
        private readonly By byBtnSizeGuide;
        private readonly By byBrandName;
        private readonly By byProductDescription;
        private readonly By byPriceInfo;
        private readonly By byDivProductCollapser;
        private readonly By bySizeAndMeas;
        private readonly By bySizeAndFitCollapser;
        private readonly By byCategoryBreadCrumb;
        private readonly By byMeasToogle;
        private readonly By byModelMeas;
        private readonly By byModelMeasTable;
        private readonly By bySpanMeasToogleProduct;


        public ProductPO(IWebDriver driver)
        {
            this.driver = driver;
            links = new UrlLinks();
            strHelper = new StringHelper();
            convert = new ConvertHelper();

            byProductGallery = By.CssSelector("div[data-tstid=gallery-and-productoffer");
            byBtnSizeGuide = By.CssSelector("button[data-tstid=sizeGuideButton");
            byBrandName = By.CssSelector("a[data-tstid=cardInfo-title");
            byProductDescription = By.CssSelector("span[data-tstid=cardInfo-description");
            byPriceInfo = By.CssSelector("span[data-tstid=priceInfo-original]");
            byDivProductCollapser = By.CssSelector("div[data-tstid=collapser]");
            bySizeAndMeas = By.Id("tamanhos-&-medidas");
            bySizeAndFitCollapser = By.CssSelector("div[data-tstid=sizeAndFitCollapserArea]");
            byCategoryBreadCrumb = By.CssSelector("ol[data-tstid=breadcrumb]");
            byMeasToogle = By.CssSelector("div[data-tstid=measurementsToggle]");
            byModelMeas = By.CssSelector("div[data-tstid=modelMeasurements]");
            byModelMeasTable = By.CssSelector("div[data-tstid=modelMeasurements]  >table");
            bySpanMeasToogleProduct = By.CssSelector("div[data-tstid=measurementToggle] > span");
        }

        public IWebElement productGallery => driver.FindElement(byProductGallery);
        public IWebElement btnSizeGuideButton => driver.FindElement(byBtnSizeGuide);
        public IWebElement BrandName => driver.FindElement(byBrandName);
        public IWebElement ProductDescription => driver.FindElement(byProductDescription);
        public IWebElement ProductPrice => driver.FindElement(byPriceInfo);
        public IWebElement Collapser => driver.FindElement(byDivProductCollapser);
        public IWebElement SizeAndMeasBtn => driver.FindElement(bySizeAndMeas);
        public IWebElement SizeAndFitCollapser => driver.FindElement(bySizeAndFitCollapser);
        public IWebElement ProductCategoryBreadCrumb => driver.FindElement(byCategoryBreadCrumb);
        public IWebElement MeasurementToogle => driver.FindElement(byMeasToogle);
        public IWebElement ModelMeasurementDiv => driver.FindElement(byModelMeas);
        public IWebElement ModelMeasurementTable => driver.FindElement(byModelMeasTable);

        public void OpenProductTShirt()
        {
            driver.Navigate().GoToUrl(links.ProductTShirtUrl);
        }

        public void OpenProductTShirtDiscount()
        {
            driver.Navigate().GoToUrl(links.ProductTShirtDiscountUrl);
        }

        public void OpenProductModelTest()
        {
            driver.Navigate().GoToUrl(links.ProductModelUrl);
        }

        public double GetProductPrice()
        {
            string strPrice = strHelper.RemoveCurrencySymbols(ProductPrice.Text);
            return convert.ConvertStrToDouble(strPrice);
        }

        private void SelectProductMeasOption(string optionText)
        {
            IList<IWebElement> listOfSpans = driver.FindElements(bySpanMeasToogleProduct);
            foreach (IWebElement ele in listOfSpans)
            {
                string eleText = ele.GetAttribute("innerText");
                if (eleText == optionText)
                {
                    if (ele.Displayed == false)
                    {
                        ele.Click();
                    }
                }
            }
        }

        private List<string> GenerateExpectedMeasurementValues(By byElementsWithValues)
        {
            IList<IWebElement> tableDataValueCM = driver.FindElements(byElementsWithValues);
            List<string> expectedValuesInches = new List<string>();

            foreach (IWebElement element in tableDataValueCM)
            {
                string unitOfMeas = strHelper.RemoveDigits(element.Text);
                string valueString = (element.Text).Replace(unitOfMeas, "").Trim();
                double valueDouble = convert.ConvertStrToDouble(valueString);
                string convertedValueStr;
                switch (unitOfMeas)
                {
                    case ("m"):
                        List<double> converted = convert.ConvertMetersToFeetInches(valueDouble);
                        convertedValueStr = converted[0] + " ft " + converted[1] + " em";
                        expectedValuesInches.Add(convertedValueStr);
                        break;
                    case ("cm"):
                        convertedValueStr = convert.ConvertCmToInches(valueDouble) + " em";
                        expectedValuesInches.Add(convertedValueStr);
                        break;
                }
            }

            return expectedValuesInches;
        }

        public bool CheckMeasurementInchesConversion(string divToCheck)
        {

            By placeToChek;
            List<string> expectedValuesInches = new List<string>();
            
            switch (divToCheck)
            {
                case "model":
                    placeToChek = By.CssSelector("td[data-tstid=standardMeasurement]");
                    expectedValuesInches = GenerateExpectedMeasurementValues(placeToChek);
                    MeasurementToogle.Click();
                    break;
                case "product":
                    placeToChek = By.CssSelector("td[data-tstid=productStandardMeasurement]");
                    expectedValuesInches = GenerateExpectedMeasurementValues(placeToChek);
                    SelectProductMeasOption("in");
                    break;
                default:
                    return false;
            }
            

            IList<IWebElement> tableDataValueInches = driver.FindElements(placeToChek);
            bool result = true;
            for (int indexEle = 0; indexEle < tableDataValueInches.Count; indexEle++)
            {
                if (tableDataValueInches[indexEle].Text != expectedValuesInches[indexEle])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

    }
}
