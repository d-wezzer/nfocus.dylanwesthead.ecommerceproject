/*
 * Author: Dylan Westhead
 * Last Edited: 29/09/2022
 *
 *   - The Page Object Model for the order details page of the Edgewords eCommerce demo site. 
 */
using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class OrderDetailsPOM
    {
        private readonly IWebDriver _driver;

        internal OrderDetailsPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators for the order number. The => means each time the variable is used, find element is called.
        private IWebElement OrderNumberField => _driver.FindElement(By.CssSelector(".order > strong"));
        private IWebElement OrderDetailsFull => _driver.FindElement(By.ClassName("woocommerce-thankyou-order-details"));


        /*
         * Get the Order Number
         *   - Retrives the order number displayed on the order details page.
         *   - Uses try/catch to repeat process until a valid string is returned. 
         */
        internal string GetOrderNumber()
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
         *   - Retrieves the order details from the order deails page in the form of an IWebElement.
         */
        internal IWebElement GetOrderDetails()
        {
            return OrderDetailsFull;
        }
    }
}
