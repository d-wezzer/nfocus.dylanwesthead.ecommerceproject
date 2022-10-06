/*
 * Author: Dylan Westhead
 * Last Edited: 07/10/2022
 *
 *   - The Object Model for the navigation bar. Saves repeating the same code in each of the POMPages.
 */
using nfocus.dylanwesthead.ecommerceproject.Utils;
using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    /*
     * The Navigation Bar Object-Model
     *   - The NavigationBar is an object model containing the necessary locators for use with the tests.
     *   - Also contains the relevant functions to interact with the locators.
     */
    internal class NavigationBarPOM
    {

        private readonly IWebDriver _driver;

        internal NavigationBarPOM(IWebDriver driver)
        {
            _driver = driver;
        }

        /* Locators and elements needed for the navigation bar links. */
        // The => means each time the variable is used, find element is called.
        private By _myAccountLocator => By.LinkText("My account");
        private IWebElement _homeLink => _driver.FindElement(By.LinkText("Home"));
        private IWebElement _shopLink => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement _cartLink => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _checkoutLink => _driver.FindElement(By.LinkText("Checkout"));
        private IWebElement _myAccountLink => _driver.FindElement(_myAccountLocator);


        /*
         * GoToHome()
         *   - Navigates to the Edgewords eCommerce homepage.
         */
        internal void GoToHome()
        {
            _homeLink.Click();
        }


        /*
         * GoToShop()
         *   - Navigates to the Edgewords eCommerce shop.
         */
        internal void GoToShop()
        {
            _shopLink.Click();
        }


        /*
         * GoToCart()
         *   - Navigates to a customers cart on the Edgewords eCommerce site.
         */
        internal void GoToCart()
        {
            _cartLink.Click();
        }


        /*
         * GoToCheckout()
         *   - Navigates to the Edgewords eCommerce checkout page.
         */
        internal void GoToCheckout()
        {
            _checkoutLink.Click();
        }


        /*
         * GoToMyAccount()
         *   - Navigates to a customers profile/account page on the Edgewords eCommerce site.
         *   - Explicit wait of 2 seconds for the my account link to be displayed, before timing out.
         */
        internal void GoToMyAccount()
        {
            // Wait for link to be displayed on navigation menu.
            Helper waitForMyAccount = new(_driver);
            waitForMyAccount.WaitForElement(2, _myAccountLocator);

            _myAccountLink.Click();
        }
    }
}
