using FarfetchSeleniumTest.Fixtures;
using FarfetchSeleniumTest.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace FarfetchSeleniumTest.Tests
{
    //[Collection("Chrome Driver")]
    public class CategoryFilterTests : IClassFixture<TestFixture>
    {

        private readonly IWebDriver driver;
        private readonly HomePO home;
        private readonly ProductAreaPO products;

        public CategoryFilterTests (TestFixture fixture)
        {
            this.driver = fixture.Driver;
            home = new HomePO(driver);
            products = new ProductAreaPO(driver);
        }

        [Fact]
        public void MustDisplayRightResults()
        {
            // ARRANGE: When the user is at Home Page;           
            home.OpenHomePage();
    
            // ACT: When the categories "Male > Clothing" is selected;
            home.GenderMaleFilter.Click();
            home.GenderMaleClothingFilter.Click();

            // ASSERT: Then the Men Cloathing page will be displayed;
            Assert.True(products.MenClothingIsOpened());
        }

    }
}
