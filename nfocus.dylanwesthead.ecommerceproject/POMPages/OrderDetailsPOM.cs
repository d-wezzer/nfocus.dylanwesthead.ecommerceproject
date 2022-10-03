/*
 * Author: Dylan Westhead
 * Last Edited: 29/09/2022
 *
 *   - The Page Object Model for the order details page of the Edgewords eCommerce demo site. 
 */
using nfocus.dylanwesthead.ecommerceproject.Utils;
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
        private IWebElement OrderConfirmationFull => _driver.FindElement(By.ClassName("woocommerce-thankyou-order-details"));


        /*
         * Get the Order Number
         *   - Retrives the order number displayed on the order details page.
         *   - Uses try/catch to repeat process until a valid string is returned. 
         */
        internal string GetOrderNumber()
        {
            // Takes a screenshot of the newly generated order number.
            ScreenshotNewOrderNumber();

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
         *   - Retrieves the order details from the order details page as a screenshot.
         *   - Attaches screenshot to the test report details.
         */
        internal void ScreenshotNewOrderNumber()
        {
            Helper ElementScreenshot = new(_driver);
            // Takes a screenshot of the new order number and attaches to the Test Details (for verification purposes).
            ElementScreenshot.TakeScreenshotElement(OrderConfirmationFull, "new_order_number");
        }
    }
}
