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

        // Locators for the order number. The => means each time the variable is used, find element is called.
        IWebElement OrderNumberField => _driver.FindElement(By.CssSelector(".order > strong"));
        IWebElement OrderDetailsFull => _driver.FindElement(By.ClassName("woocommerce-thankyou-order-details"));


        /*
         * Get the Order Number
         *   Retrives the order number displayed on the order details page.
         *   Uses try/catch to repeat process until a valid string is returned. 
         */
        public string GetOrderNumber()
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


        /*
         * Get the Order Details
         *   Retrieves the order details from the order deails page in the form of an IWebElement.
         */
        public IWebElement GetOrderDetails()
        {
            return OrderDetailsFull;
        }
    }
}
