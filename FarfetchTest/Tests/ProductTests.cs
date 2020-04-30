using FarfetchSeleniumTest.Fixtures;
using FarfetchSeleniumTest.Helpers;
using FarfetchSeleniumTest.PageObjects;
using OpenQA.Selenium;
using Xunit;

namespace FarfetchSeleniumTest.Tests
{
    //[Collection("Chrome Driver")]
    public class ProductTests : IClassFixture<TestFixture>
    {

        private readonly IWebDriver driver;
        private readonly ProductPO product;
        private readonly ActionsHelper actions;
       
        public ProductTests(TestFixture fixture)
        {
            this.driver = fixture.Driver;
            
            product = new ProductPO(driver);
            actions = new ActionsHelper(driver);
        }

        [Fact]
        public void MustConverModelAndProductMeasurement()
        {
            // ARRANGE: When the user is at the product page;
            product.OpenProductModelTest();

            // ACT: When the select the Size and Measurements;
            product.SizeAndMeasBtn.Click();
            actions.MoveToElement(product.SizeAndFitCollapser);
           
            // ASSERT: Must be possible to convert the units from  m/cm to ft/em;
            Assert.True(product.CheckMeasurementInchesConversion("model"), "There was some problem to convert the model measurments.");
            Assert.True(product.CheckMeasurementInchesConversion("product"), "There was some problem to convert the product measurments.");
        }
  
   
    }
}

