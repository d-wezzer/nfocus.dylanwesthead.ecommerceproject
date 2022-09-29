/*
 * Author: Dylan Westhead
 * Last Edited: 29/09/2022
 *
 *   - The Page Object Model for the shop of the Edgewords eCommerce demo site. 
 */
using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class ShopPOM
    {
        private readonly IWebDriver _driver;
        internal ShopPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators for items in shop. The => means each time the variable is used, find element is called.
        private IWebElement HoodieWithLogo => _driver.FindElement(By.XPath("//main[@id='main']/ul//a[@href='?add-to-cart=31']"));
        private IWebElement BaseballCap => _driver.FindElement(By.XPath("//main[@id='main']/ul//a[@href='?add-to-cart=29']"));

        /*
         * Add items to the Cart
         *   - The AddItemsToCart() function simply adds two items to the cart.
         *   - HoodieWithLogo and BaseballCap are added to the cart.
         */
        internal void AddItemsToCart()
        {
            HoodieWithLogo.Click();
            BaseballCap.Click();
        }
    }
}
