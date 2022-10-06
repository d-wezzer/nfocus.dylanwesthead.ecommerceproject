/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
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

        /* Locators and elements for the order number on the order details page. */
        // The => means each time the variable is used, find element is called.
        private IWebElement _orderNumberField => _driver.FindElement(By.CssSelector(".order > strong"));
        private IWebElement _orderConfirmationFull => _driver.FindElement(By.ClassName("woocommerce-thankyou-order-details"));


        /*
         * GetOrderNumber()
         *   - Retrieves the order number displayed on the order details page.
         *   - Order number is captured directly from the webpage.
         *   - Uses try/catch to repeat process if the first attempt fails.
         */
        internal string GetOrderNumber()
        {
            // Takes a screenshot of the newly generated order number.
            ScreenshotNewOrderNumber();

            try
            {
                return _orderNumberField.Text;
            }
            catch
            {
                return _orderNumberField.Text;
            }
        }


        /*
         * ScreenshotNewOrderNumber()
         *   - Take screenshot of the new order details.
         *   - Attaches screenshot to the test context for verfication purposes.
         */
        private void ScreenshotNewOrderNumber()
        {
            // Takes a screenshot of the new order number and attaches to the Test Details (for verification purposes).
            Helper _elementScreenshot = new(_driver);
            _elementScreenshot.TakeScreenshotElement(_orderConfirmationFull, "new_order_number");
        }
    }
}
