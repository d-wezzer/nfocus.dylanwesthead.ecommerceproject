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

        // Locators
        IWebElement OrderNumbers => _driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number"));
        IWebElement AllOrderNumbersTable => _driver.FindElement(By.ClassName("woocommerce-MyAccount-orders"));

        public string GetTopOrderNumber()
        {
            return OrderNumbers.Text;
        }

        public IWebElement GetAllOrderNumbersTable()
        {
            return AllOrderNumbersTable;
        }
    }
}
