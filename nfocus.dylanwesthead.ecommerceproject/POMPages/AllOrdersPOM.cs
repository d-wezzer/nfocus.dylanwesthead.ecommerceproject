using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class AllOrdersPOM
    {
        private IWebDriver _driver;

        public AllOrdersPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators used to gather the most recent order number from the order history page.
        IWebElement OrderNumbers => _driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number"));
        IWebElement AllOrderNumbersTable => _driver.FindElement(By.ClassName("woocommerce-MyAccount-orders"));

        // Retrieves the top most recent order number.
        public string GetTopOrderNumber()
        {
            return OrderNumbers.Text;
        }

        // Retrieves the entire row of order numbers on the first page of the order history.
        public IWebElement GetAllOrderNumbersTable()
        {
            return AllOrderNumbersTable;
        }
    }
}
