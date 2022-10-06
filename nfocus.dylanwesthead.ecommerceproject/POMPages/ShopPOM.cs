/*
 * Author: Dylan Westhead
 * Last Edited: 07/10/2022
 *
 *   - The Page Object Model for the shop of the Edgewords eCommerce demo site. 
 */
using nfocus.dylanwesthead.ecommerceproject.Utils;
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

        /* Locators and elements to search for, add items to, and view the cart from the shop page. */
        // The => means each time the variable is used, find element is called.
        private By _addToCartLocator => By.ClassName("single_add_to_cart_button");
        private By _viewCartLocator => By.LinkText("View cart");
        private IWebElement _addToCartButton => _driver.FindElement(_addToCartLocator);
        private IWebElement _searchBar => _driver.FindElement(By.Id("woocommerce-product-search-field-0"));


        /*
         * SearchForAndAddProduct(string, string)
         *   - Searches for the provided product names.
         *   - Iteratively searches for and adds both products to the cart.
         *   - Explicit wait of 2 seconds for the view cart button to be displayed.
         */
        internal void SearchForAndAddProduct(string product1, string product2)
        {
            string[] products = { product1, product2 };

            // Loop through both products passed in from feature file and add to cart.
            foreach (string product in products) {
                // Search for the product
                SearchForProduct(product);

                // Allow store contents one second to load.
                Helper myhelper = new(_driver);
                myhelper.WaitForElement(1, _addToCartLocator);

                // Add item to cart and allow cart time to update
                AddItemToCart();
                myhelper.WaitForElement(2, _viewCartLocator);
            }
        }


        /*
         * SearchForProduct(string)
         *   - Searches for the provided product via the search bar.
         */
        internal void SearchForProduct(string product1)
        {
            _searchBar.Clear();
            _searchBar.SendKeys(product1 + Keys.Enter);
        }

        /*
         * AddItemToCart()
         *   - Clicks the add to cart button to add an item to the cart.
         */
        internal void AddItemToCart()
        {
            _addToCartButton.Click();
        }
    }
}
