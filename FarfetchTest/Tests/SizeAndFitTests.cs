using FarfetchSeleniumTest.Fixtures;
using FarfetchSeleniumTest.Helpers;
using FarfetchSeleniumTest.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace FarfetchSeleniumTest.Tests
{

    //[Collection("Chrome Driver")]
    public class SizeAndFitTests : IClassFixture<TestFixture>
    {

        private readonly IWebDriver driver;
        private readonly SizeAndFitTestsData tstData;

        private readonly ProductPO product;
        private readonly SizeGuidePO size;
        private readonly NotifyAvailablePO notify;
        
        private double productPrice;

        public SizeAndFitTests(TestFixture fixture)
        {
            this.driver = fixture.Driver;
            tstData = new SizeAndFitTestsData();

            product = new ProductPO(driver);
            size = new SizeGuidePO(driver);
            notify = new NotifyAvailablePO(driver);
        }

        private void OpenSizeGuidePage(bool getPrice = false)
        {
            product.OpenProductTShirt();

            if (getPrice)
            {
                productPrice = product.GetProductPrice();
            }

            product.btnSizeGuideButton.Click();
            size.WaitSizeGuideDiv();
        }

        private void OpenSizeGuidePageDiscount(bool getPrice = false)
        {
            product.OpenProductTShirtDiscount();

            if (getPrice)
            {
                productPrice = product.GetProductPrice();
            }

            product.btnSizeGuideButton.Click();
            size.WaitSizeGuideDiv();
        }

        private void SelectOutOfStockProductSize()
        {
            size.SelectTableRow(tstData.ProductOutOfStockTableSelect);
            size.WaitOutOfStockText("Esgotado");
        }

        private void SelectDifferentPriceProductSize()
        {
            size.SelectTableRow(tstData.ProductDifferentPriceTableSelect);
            size.WaitNormalPriceText();
        }

        private void SelectDiscountProductSize()
        {
            size.SelectTableRow(tstData.ProductDiscountTableSelect);
            size.WaitNormalPriceText();
        }

        private void OpenNotifyAvailablePage()
        {
            size.OutOfStockNotifyAvailable.Click();
            notify.CheckNotifyAvailableFormVisivble();
        }

        [Fact]
        public void ProductMustDisplaySizeGuideButton()
        {
            // ARRANGE: When the user is at the Product page;
            product.OpenProductTShirt();

            // ASSERT: The button to open the Size Guide will be displayed;
            Assert.True(product.btnSizeGuideButton.Displayed);
            Assert.Contains("Guia de tamanhos", product.btnSizeGuideButton.Text);
        }

        [Fact]
        public void SizeGuideMustDisplayProductInfo()
        {
            // ARRANGE: When the user is at the Product page;
            product.OpenProductTShirt();

            // ACT: After clicking on the Size Guide button;
            product.btnSizeGuideButton.Click();
            string productPageBrandName = product.BrandName.Text;
            string productPageProductDescription = product.ProductDescription.Text;
            size.WaitSizeGuideDiv();

            // ASSERT: The Size Guide page will be opened with the product information;
            Assert.Contains(productPageBrandName, size.BrandName.Text);
            Assert.Contains(productPageProductDescription, size.ProductDescription.Text);
        }

        [Fact]
        public void SizeGuideMustDisplayCountriesList()
        {
            // ARRANGE: When the user is at the Product page;
            product.OpenProductTShirt();

            // ACT: After clicking on the Size Guide button;
            product.btnSizeGuideButton.Click();
            size.WaitSizeGuideDiv();

            // ASSERT: The Size Guide page will display the right country list;
            Assert.Equal(tstData.CountryList.Count, size.ReturnNumberOfCountryOptions());
        }

        [Fact]
        public void SizeGuideMustDisplayOutOfStockSize()
        {
            // ARRANGE: When the Product Size and Measurement page is opened;
            OpenSizeGuidePage();

            // ARRANGE: When the Product Size and Measurement page is opened;
            SelectOutOfStockProductSize();

            // ASSERT: The Out Of Stock text will be displayed within an option to be Notified When Available;
            Assert.Equal("Esgotado", size.OutOfStockText.Text);
            Assert.Equal("Avise-me quando estiver disponível", size.OutOfStockNotifyAvailable.Text);
        }

        [Fact]
        public void SizeGuideMustOpenToBeNotified()
        {
            // ARRANGE: When the Product Size and Measurement page is opened;
            OpenSizeGuidePage();

            // ARRANGE: When the Product Size and Measurement page is opened and clicking to be Notified;
            SelectOutOfStockProductSize();
            OpenNotifyAvailablePage();

            // ASSERT: The Notification page will be oepend.
            Assert.True(notify.OutOfStockNotificationIsOpened(), "The Notification page was not opened.");
        }

        [Fact]
        public void SizeGuideNotificationMustConsistInvalidEmail()
        {
            // ARRANGE: When the Product Size and Measurement page is opened;
            OpenSizeGuidePage();
            SelectOutOfStockProductSize();

            // ACT: After selecting a size out of stock, and to be notified. When a invalid email is inserted;
            OpenNotifyAvailablePage();
            notify.EmailInput.SendKeys("test" + Keys.Tab);

            // ASSERT: The hint to inform invalid email will be displayed;
            Assert.True(notify.InvalidEmailIsShown(), "The hint warning invalid email was not displayed.");
        }

        [Fact]
        public void SizeGuideMustDisplayWhenStandardPrice()
        {
            // ARRANGE: When the Product Size and Measurement page is opened;
            OpenSizeGuidePage(true);

            // ACT: And the user select a size with standard price.
            size.SelectTableRow(tstData.ProductStandardPriceTableSelect);
            size.WaitNormalPriceText();

            // ASSERT: The Size Guide page will display the same price of Product page;
            Assert.True(size.AddToBagButton.Displayed, "The Size Guide was not displaying the button to add to the bag.");
            Assert.False(size.CheckDifferentPricesValues(productPrice), "The Size Guide was not displaying two different prices.");
        }

        [Fact]
        public void SizeGuideMustDisplayWhenDifferentPrice()
        {
            // ARRANGE: When the Product Size and Measurement page is opened;
            OpenSizeGuidePage(true);

            // ACT: And the user select a size with a different price;
            SelectDifferentPriceProductSize();

            // ASSERT: An different price will be displayed withing a notification;
            Assert.True(size.CheckDifferentPricesValues(productPrice), "The price different from the standard was not displayed.");
            Assert.True(size.CheckDifferentPriceWarning(), "The warning about the different price was not displayed.");
        }

        [Fact]
        public void SizeGuideWhenDifferentPriceMoreInfoWillBeDisplayed()
        {
            // ARRANGE: When the Product Size and Measurement page is opened;
            OpenSizeGuidePage();

            // ACT: And the user select a size with a different price and clicks to have more info;
            SelectDifferentPriceProductSize();
            size.ClickDifferentPriceButton();

            // ASSERT: The More Info button will be displayed;
            Assert.True(size.DifferentPriceWhyButton.Displayed, "The \"Why\" button was not displayed.");
            Assert.True(size.DifferentPriceWhyInfo.Displayed, "The detailed information (hint) of the \"Why\" button was not displayed.");
        }

        [Fact]
        public void SizeGuideMustShowWhenDicount()
        {
            // ARRANGE: When the Product Size and Measurement page is opened;
            OpenSizeGuidePageDiscount();

            // ACT: And the user selects a size with discount;
            SelectDiscountProductSize();

            // ASSERT: 
            //  The standard price will be displayed together with the price with the discount applied;
            //  Both prices will be displayed side by side;
            //  The price with the discount will be displayed within the "line-through / strike" effect;
            Assert.True(size.CheckTwoPricesDisplayed(), "Both prices where not displayed.");
            Assert.True(size.CheckDifferentPricesSideBySide(), "The prices where not displayed side by side.");
            Assert.True(size.CheckProductWithDiscount(), "The price with \"discount\" was displaying equal or higher value.");
        }

    }
}
