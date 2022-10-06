/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
 *
 *   - The Page Object Model for the order history page of the Edgewords eCommerce demo site. 
 */
using nfocus.dylanwesthead.ecommerceproject.Utils;
using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class AllOrdersPOM
    {
        private readonly IWebDriver _driver;

        internal AllOrdersPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        /* Locators and elements used to gather the most recent order number from the order history page. */
        // The => means each time the variable is used, find element is called.
        private IWebElement _orderNumbers => _driver.FindElement(By.ClassName("woocommerce-orders-table__cell-order-number"));
        private IWebElement _allOrderNumbersTable => _driver.FindElement(By.ClassName("woocommerce-MyAccount-orders"));


        /*
         * GetTopOrderNumber()
         *   - Retrieves the top (most recent) order number.
         *   - Takes a screenshot of all order numbers if enabled.
         */
        internal string GetTopOrderNumber()
        {
            // Screenshot the entire orders table.
            ScreenshotAllOrders();

            // Remove the # symbol for a pure digit string.
            return _orderNumbers.Text.Replace("#", "");
        }


        /*
         * ScreenshotAllOrders()
         *   - Takes a screenshot of the table containing all orders.
         *   - Attaches to the test context for verification purposes.
         */
        internal void ScreenshotAllOrders()
        {
            Helper elementScreenshot = new(_driver);
            // Takes screenshot of the entire orders table in my account.
            elementScreenshot.TakeScreenshotElement(_allOrderNumbersTable, "top_order_from_all_orders");
        }
    }
}
