/*
 * Author: Dylan Westhead
 * Last Edited: 07/10/2022
 *
 *   - The Page Object Model for the my account page of the Edgewords eCommerce demo site. 
 */
using nfocus.dylanwesthead.ecommerceproject.Utils;
using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class MyAccountPOM
    {
        private readonly IWebDriver _driver;
        internal MyAccountPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        /* Locators and elements needed to view order history from the my account page. */
        // The => means each time the variable is used, find element is called.
        private By _ordersLinkLocator => By.LinkText("Orders");
        private IWebElement _ordersLink => _driver.FindElement(_ordersLinkLocator);
        private IWebElement _logOutLink => _driver.FindElement(By.LinkText("Logout"));


        /*
         * GoToOrders()
         *   - Clicks and navigates to a customers order history page.
         *   - Explicit wait of 2 seconds to wait for orders link to be displayed, before timing out.
         */
        internal void GoToOrders()
        {
            // Wait for the orders link to be displayed in the side menu.
            Helper waitForOrders = new(_driver);
            waitForOrders.WaitForElement(2, _ordersLinkLocator);

            _ordersLink.Click();
        }


        /*
         * LogOutOfAccount()
         *   - Logs out of an account.
         */
        internal void LogOutOfAccount()
        {
            _logOutLink.Click();
        }
    }
}
