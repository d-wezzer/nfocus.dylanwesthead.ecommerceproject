/*
 * Author: Dylan Westhead
 * Last Edited: 29/09/2022
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

        // Locators for the orders page and logout link. The => means each time the variable is used, find element is called.
        private IWebElement OrdersLink => _driver.FindElement(By.LinkText("Orders"));
        private IWebElement LogOutLink => _driver.FindElement(By.LinkText("Logout"));

        // Navigates to the orders page link from the my account page.
        internal void GoToOrders()
        {
            // Wait for the orders link to be displayed in the side menu.
            Helper WaitForOrders = new(_driver);
            WaitForOrders.WaitForElement(2, By.LinkText("Orders"));

            OrdersLink.Click();
        }

        // Logs out from the my account page.
        internal void LogOutOfAccount()
        {
            LogOutLink.Click();
        }
    }
}
