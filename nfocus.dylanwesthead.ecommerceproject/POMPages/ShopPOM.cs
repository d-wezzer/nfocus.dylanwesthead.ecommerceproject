using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class ShopPOM
    {
        private IWebDriver _driver;
        public ShopPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators for items in shop
        IWebElement hoodieWithLogo => _driver.FindElement(By.XPath("//main[@id='main']/ul//a[@href='?add-to-cart=31']"));
        IWebElement baseballCap => _driver.FindElement(By.XPath("//main[@id='main']/ul//a[@href='?add-to-cart=29']"));

        public void addItemsToCart()
        {
            hoodieWithLogo.Click();
            baseballCap.Click();
        }
    }
}
