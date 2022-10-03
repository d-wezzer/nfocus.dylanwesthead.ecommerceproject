/*
 * Author: Dylan Westhead
 * Last Edited: 01/10/2022
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

        // Locators used to gather the most recent order number from the order history page.
        private IWebElement OrderNumbers => _driver.FindElement(By.ClassName("woocommerce-orders-table__cell-order-number"));
        private IWebElement AllOrderNumbersTable => _driver.FindElement(By.ClassName("woocommerce-MyAccount-orders"));

        // Retrieves the top most recent order number.
        internal string GetTopOrderNumber()
        {
            // Screenshot the entire orders table.
            ScreenshotAllOrders();

            // Remove the # symbol for a pure digit string.
            return OrderNumbers.Text.Replace("#", "");
        }

        // Retrieves the entire row of order numbers on the first page of the order history.
        internal IWebElement GetAllOrderNumbersTable()
        {
            return AllOrderNumbersTable;
        }

        internal void ScreenshotAllOrders()
        {
            Helper ElementScreenshot = new(_driver);
            // Takes a screenshot of the entire orders table in my account and attaches to the Test Details (for verification purposes).
            ElementScreenshot.TakeScreenshotElement(AllOrderNumbersTable, "top_order_from_all_orders");
        }
    }
}
