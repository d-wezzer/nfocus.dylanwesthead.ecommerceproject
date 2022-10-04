/*
 * Author: Dylan Westhead
 * Last Edited: 29/09/2022
 *
 *   - The object model for the navigation bar. Saves repeating the same code in each of the POMPages.
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

        // Locators used for all the elements needed from the navigation bar.
        private readonly By _MyAccountLocator = By.LinkText("My account");
        private IWebElement HomeLink => _driver.FindElement(By.LinkText("Home"));
        private IWebElement ShopLink => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement CartLink => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement CheckoutLink => _driver.FindElement(By.LinkText("Checkout"));
        private IWebElement MyAccountLink => _driver.FindElement(_MyAccountLocator);

        // Navigates to the Edgewords eCommerce homepage.
        internal void GoToHome()
        {
            HomeLink.Click();
        }

        // Navigates to the Edgewords eCommerce shop.
        internal void GoToShop()
        {
            ShopLink.Click();
        }

        // Navigates to the Edgewords eCommerce cart page.
        internal void GoToCart()
        {
            CartLink.Click();
        }

        // Navigates to the Edgewords eCommerce checkout page.
        internal void GoToCheckout()
        {
            CheckoutLink.Click();
        }

        // Navigates to the Edgewords eCommerce my account page.
        internal void GoToMyAccount()
        {
            // Wait for my account link to be displayed on navigation menu.
            Helper WaitForMyAccount = new(_driver);
            WaitForMyAccount.WaitForElement(2, _MyAccountLocator);

            MyAccountLink.Click();
        }
    }
}
