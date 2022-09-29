/*
 * Author: Dylan Westhead
 * Last Edited: 29/09/2022
 *
 *   - The Page Object Model for the order history page of the Edgewords eCommerce demo site. 
 */
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
        private IWebElement OrderNumbers => _driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number"));
        private IWebElement AllOrderNumbersTable => _driver.FindElement(By.ClassName("woocommerce-MyAccount-orders"));

        // Retrieves the top most recent order number.
        internal string GetTopOrderNumber()
        {
            return OrderNumbers.Text;
        }

        // Retrieves the entire row of order numbers on the first page of the order history.
        internal IWebElement GetAllOrderNumbersTable()
        {
            return AllOrderNumbersTable;
        }
    }
}
