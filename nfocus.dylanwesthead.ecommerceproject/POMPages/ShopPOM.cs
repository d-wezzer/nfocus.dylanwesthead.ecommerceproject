/*
 * Author: Dylan Westhead
 * Last Edited: 01/10/2022
 *
 *   - The Page Object Model for the shop of the Edgewords eCommerce demo site. 
 */
using nfocus.dylanwesthead.ecommerceproject.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V102.Network;

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
        private readonly By _AddToCartLocator = By.ClassName("single_add_to_cart_button");
        private readonly By _ViewCartLocator = By.LinkText("View cart");
        private IWebElement AddToCartButton => _driver.FindElement(_AddToCartLocator);
        private IWebElement ViewCartLink => _driver.FindElement(_ViewCartLocator);
        private IWebElement SearchBar => _driver.FindElement(By.Id("woocommerce-product-search-field-0"));

        /*
         * Search for Product and Add to Cart
         *   - The SearchForAndAddItem() function searches for the provided product names.
         *   - Makes use of the SearchForProduct() AddItemToCart() functions.
         */
        internal void SearchForAndAddProduct(string product1, string product2)
        {
            string[] products = { product1, product2 };

            // Loop through both products passed in from feature file and add to cart.
            foreach (string product in products) {
                // Search for the product
                SearchForProduct(product);

                // Allow store contents one second to load.
                Helper Myhelper = new(_driver);
                Myhelper.WaitForElement(1, _AddToCartLocator);

                // Add item to cart and allow cart time to update
                AddItemToCart();
                Myhelper.WaitForElement(2, _ViewCartLocator);
            }
        }

        /*
         * Searches for given product name
         *   - The SeachForProduct() function simply searches for the provided product via the search bar.
         */
        internal void SearchForProduct(string product1)
        {
            SearchBar.Clear();
            SearchBar.SendKeys(product1 + Keys.Enter);
        }

        /*
         * Add items to the Cart
         *   - The AddItemsToCart() function simply adds an item to the cart.
         */
        internal void AddItemToCart()
        {
            AddToCartButton.Click();
        }
    }
}
