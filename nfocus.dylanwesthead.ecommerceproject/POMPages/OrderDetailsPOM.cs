using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class OrderDetailsPOM
    {
        private IWebDriver _driver;

        public OrderDetailsPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators
        IWebElement OrderNumberField => _driver.FindElement(By.CssSelector(".order > strong"));
        IWebElement OrderDetailsFull => _driver.FindElement(By.ClassName("woocommerce-thankyou-order-details"));


        public string getOrderNumber()
        {
            try
            {
                return OrderNumberField.Text;
            }
            catch
            {
                return OrderNumberField.Text;
            }
        }

        public IWebElement GetOrderDetails()
        {
            return OrderDetailsFull;
        }
    }
}
