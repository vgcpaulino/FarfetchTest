using FarfetchSeleniumTest.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace FarfetchSeleniumTest.PageObjects
{
    public class SizeGuidePO
    {

        private readonly IWebDriver driver;
        private readonly TestWait wait;
        private readonly SelectHelper selectHelper;
        private readonly ListHelper listHelper;
        private readonly TableHelper tableHelper;
        private readonly ActionsHelper actions;
        private readonly StringHelper strHelper;
        private readonly ConvertHelper convertHelper;
        private readonly JavaScriptHelper executeJScript;
       
        private readonly By bySizeGuideContent;
        private readonly By byChartTable;
        private readonly By bySizeGuideDropdown;
        private readonly By bySizeGuideDropdownSelect;
        private readonly By byBrandName;
        private readonly By byProductDescription;
        private readonly By bySizeTable;
        private readonly By byOutOfStockDiv;
        private readonly By byOutOfStockText;
        private readonly By byOutOfStockNotifyAvailable;
        private readonly By byAddToBagRow;
        private readonly By byAddToBagPrice;
        private readonly By byAddToBagButton;
        private readonly By byDivDiffPrices;
        private readonly By byDiffPriceSpan;
        private readonly By byDiffPriceWhy;
        private readonly By byDiffPriceWhyInfo;

        public SizeGuidePO (IWebDriver driver)
        {
            this.driver = driver;
            wait = new TestWait(driver);
            actions = new ActionsHelper(driver);
            selectHelper = new SelectHelper();
            listHelper = new ListHelper();
            tableHelper = new TableHelper();
            strHelper = new StringHelper();
            convertHelper = new ConvertHelper();
            executeJScript = new JavaScriptHelper(driver);

            bySizeGuideContent = By.Id("sizeGuideContent");
            byChartTable = By.CssSelector("table[data-tstid=charttable");
            bySizeGuideDropdown = By.CssSelector("div[data-tstid=sizeguide-scalesdropdown]");
            bySizeGuideDropdownSelect = By.XPath("//div[@data-tstid='sizeguide-scalesdropdown']/preceding-sibling::select");
            byBrandName = By.Id("productInfo-brand");
            byProductDescription = By.Id("productInfo-description");
            bySizeTable = By.CssSelector("table[data-tstid=charttable");
            byOutOfStockDiv = By.CssSelector("td[data-tstid=outOfStockRow]");
            byOutOfStockText = By.XPath("//div[contains(text(), 'Esgotado')]");
            byOutOfStockNotifyAvailable = By.CssSelector("button[data-tstid=nibis]");
            byAddToBagRow = By.CssSelector("td[data-tstid=addToBagRow]");
            byAddToBagPrice = By.XPath("//td[@data-tstid='addToBagRow']//span");
            byAddToBagButton = By.CssSelector("button[data-tstid=addToBag]");
            byDivDiffPrices = By.XPath("//td[@data-tstid='addToBagRow']/div/div");
            byDiffPriceSpan = By.XPath("//td[@data-tstid='addToBagRow']//span[contains(text(), 'Preço diferente')]");
            byDiffPriceWhy = By.CssSelector("button[data-tstid=findOutWhy]");
            byDiffPriceWhyInfo = By.CssSelector("button[data-tstid=findOutWhy]+div>p");
        }

        public IWebElement SizeGuideContent => driver.FindElement(bySizeGuideContent);
        public IWebElement ChartTable => driver.FindElement(byChartTable);
        public IWebElement SizeGuideDropdown => driver.FindElement(bySizeGuideDropdown);
        public IWebElement SizeGuideDropdownSelect => driver.FindElement(bySizeGuideDropdownSelect);
        public IWebElement BrandName => driver.FindElement(byBrandName);
        public IWebElement ProductDescription => driver.FindElement(byProductDescription);
        public IWebElement SizeTable => driver.FindElement(bySizeTable);
        public IWebElement OutOfStockDiv => driver.FindElement(byOutOfStockDiv);
        public IWebElement OutOfStockText => driver.FindElement(byOutOfStockText);
        public IWebElement OutOfStockNotifyAvailable => driver.FindElement(byOutOfStockNotifyAvailable);
        public IWebElement AddToBagRow => driver.FindElement(byAddToBagRow);
        public IWebElement AddToBagPrice => driver.FindElement(byAddToBagPrice);
        public IWebElement AddToBagButton => driver.FindElement(byAddToBagButton);
        public IWebElement DivDifferentPrices => driver.FindElement(byDivDiffPrices);
        public IWebElement DifferentPriceText => driver.FindElement(byDiffPriceSpan);
        public IWebElement DifferentPriceWhyButton => driver.FindElement(byDiffPriceWhy);
        public IWebElement DifferentPriceWhyInfo => driver.FindElement(byDiffPriceWhyInfo);

        public void WaitSizeGuideDiv()
        {
            wait.WaitElementInteractable(10, bySizeGuideContent);
            wait.WaitElementInteractable(10, byChartTable);

            actions.MoveToElement(SizeGuideDropdown);
            
            wait.WaitElementInteractable (5, bySizeGuideDropdown);
            wait.WaitElementExists(5, bySizeGuideDropdownSelect);
        }

        public void SelectCountry(string textName)
        {
            selectHelper.SelectByText(SizeGuideDropdownSelect, textName);
        }

        public int ReturnNumberOfCountryOptions()
        {
            return selectHelper.SelectReturnQtyOptions(SizeGuideDropdownSelect);
        }

        public int ReturnMatchesCountryList(List<string> expectedList)
        {
            List<string> actualList = selectHelper.GetListOfOptions(SizeGuideDropdownSelect);
            return listHelper.ListCompareMatches(expectedList, actualList);
        }

        public void SelectTableRow(List<string> tableValues)
        {            
            IWebElement tableRow = tableHelper.ReturnTableRowElement(SizeTable, tableValues);
            executeJScript.ScrollToElement(tableRow);
            tableRow.Click();
        }

        public void WaitOutOfStockText(string text)
        {
            wait.WaitTextInElement(1, OutOfStockText, text);
        }

        public void WaitNormalPriceText()
        { 
            wait.WaitTextDifferentFrom(10, AddToBagPrice, AddToBagPrice.Text);
        }

        private IList<IWebElement> GetDifferentPricesElements()
        {
            IList<IWebElement> listOfSpan = DivDifferentPrices.FindElements(By.TagName("span"));
            return listOfSpan;
        }

        public bool CheckTwoPricesDisplayed()
        {
            IList<IWebElement> listOfSpan = GetDifferentPricesElements();

            if (listOfSpan.Count != 2)
            {
                return false;
            }

            foreach (IWebElement element in listOfSpan)
            {
                if (!element.Displayed)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckDifferentPricesSideBySide()
        {
            IList<IWebElement> listOfSpan = GetDifferentPricesElements();
            bool result = false;
            if (listOfSpan.Count == 2)
            {
                result = ((listOfSpan[0].Location.X < listOfSpan[1].Location.X) && (listOfSpan[0].Location.Y == listOfSpan[1].Location.Y));
            }
            return result;
        }

        public bool CheckDifferentPricesValues(double standardProductPrice)
        {
            string strCurrentPrice = strHelper.RemoveCurrencySymbols(AddToBagPrice.Text);
            return standardProductPrice != convertHelper.ConvertStrToDouble(strCurrentPrice);
        }

        public bool CheckProductWithDiscount()
        {
            IList<IWebElement> listOfSpan = GetDifferentPricesElements();
            List<double> listOfSpanValues = new List<double>();
            bool result = false;

            int indexHigherPrice = 0;
            double higherPrice = -999999;
            if (listOfSpan.Count == 2)
            {
                for (int index = 0; index < listOfSpan.Count; index++)
                {
                    double value = convertHelper.ConvertStrToDouble(strHelper.RemoveCurrencySymbols(listOfSpan[index].Text));
                    listOfSpanValues.Add(value);

                    if (value > higherPrice)
                    {
                        higherPrice = value;
                        indexHigherPrice = index;
                    }
                }
                result = (listOfSpanValues[0] > listOfSpanValues[1]);
            }

            string textDecorationValue = listOfSpan[indexHigherPrice].GetCssValue("text-decoration");
            result = textDecorationValue.Contains("line-through");

            return result;
        }

        public bool CheckDifferentPriceWarning()
        {
            return (DifferentPriceText.Text == "Preço diferente");
        }

        public void ClickDifferentPriceButton()
        {
            executeJScript.ScrollToElement(DifferentPriceWhyButton);
            DifferentPriceWhyButton.Click();
            wait.WaitElementVisible(1, byDiffPriceWhyInfo);
        }

        public bool CheckDifferentPriceWarningNotVisible()
        {
            return false;
        }

        public double GetProductPrice()
        {
            string strPrice = strHelper.RemoveCurrencySymbols(AddToBagPrice.Text);
            return convertHelper.ConvertStrToDouble(strPrice);
        }
    }
    
}
