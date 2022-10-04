/*
 * Author: Dylan Westhead
 * Last Edited: 01/10/2022
 *
 *   - The Page Object Model for the checkout page of the Edgewords eCommerce demo site. 
 */
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class CheckoutPOM
    {
        private readonly IWebDriver _driver;

        internal CheckoutPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators for the customer details field elements used on the billing page. The => means each time the variable is used, find element is called.
        private IWebElement FirstNameField => _driver.FindElement(By.Id("billing_first_name"));
        private IWebElement SurnameField => _driver.FindElement(By.Id("billing_last_name"));
        private IWebElement HomeAddressField => _driver.FindElement(By.Id("billing_address_1"));
        private IWebElement CityField => _driver.FindElement(By.Id("billing_city"));
        private IWebElement PostcodeField => _driver.FindElement(By.Id("billing_postcode"));
        private IWebElement PhoneField => _driver.FindElement(By.Id("billing_phone"));
        private IWebElement EmailField => _driver.FindElement(By.Id("billing_email"));
        private IWebElement PayByChequeRadioButton => _driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label"));
        private IWebElement PlaceOrderButton => _driver.FindElement(By.Id("place_order"));

        // Enters the given first name into the corresponding field on the billing form.
        private void SetFirstNameField(string firstName)
        {
            FirstNameField.Clear();
            FirstNameField.SendKeys(firstName);
        }

        // Enters the given surname into the corresponding field on the billing form.
        private void SetSurnameField(string surname)
        {
            SurnameField.Clear();
            SurnameField.SendKeys(surname);
        }

        // Enters the given address into the corresponding field on the billing form.
        private void SetHomeAddressField(string address)
        {
            HomeAddressField.Clear();
            HomeAddressField.SendKeys(address);
        }

        // Enters the given city into the corresponding field on the billing form.
        private void SetCityField(string city)
        {
            CityField.Clear();
            CityField.SendKeys(city);
        }

        // Enters the given postcode into the corresponding field on the billing form.
        private void SetPostcodeField(string postcode)
        {
            PostcodeField.Clear();
            PostcodeField.SendKeys(postcode);
        }

        // Enters the given phone number into the corresponding field on the billing form.
        private void SetPhoneField(string phone)
        {
            PhoneField.Clear();
            PhoneField.SendKeys(phone);
        }

        // Enters the given email into the corresponding field on the billing form.
        private void SetEmailField(string email)
        {
            EmailField.Clear();
            EmailField.SendKeys(email);
        }

        // Uses all the above methods to populate the billing form with the correct information.
        internal void PopulateBillingInfo(string first, string surname, string address, string city, string postcode, string phone, string email)
        {
            SetFirstNameField(first);
            SetSurnameField(surname);
            SetHomeAddressField(address);
            SetCityField(city);
            SetPostcodeField(postcode);
            SetPhoneField(phone);
            SetEmailField(email);
        }

        // Clicks the pay by dheque radio button.
        // Uses a try/catch to repeat the process and prevent stale element exceptions.
        internal bool SelectPayByCheque()
        {
            try // Always perform at least once.
            {
                PayByChequeRadioButton.Click();
            }
            catch // Run function again if the 'try' attempt failed.
            {
                PayByChequeRadioButton.Click();
                return false;
            }
            return true;
        }

        // Clicks the place order button.
        // Uses a try/catch to repeat the process and prevent any stale element exceptions.
        internal void PlaceOrder()
        {
            try // Always perform at least once.
            {
                PlaceOrderButton.Click();
            }
            catch // Retry in catch if (try) attempt unsuccessful.
            {
                PlaceOrderButton.Click();
            }
        }

        /*
         * Wait for Order to be Received
         *   - Waits for the entry text 'Order received' to be displayed.
         *   - An explicit wait of 5 seconds has been allowed before timeout.
         */
        internal void WaitForOrderConfirmed()
        {
            // Wait for the order details page to be displayed. Creating an order takes a few seconds.
            WebDriverWait WaitForOrderConfirmation = new(_driver, TimeSpan.FromSeconds(5));
            WaitForOrderConfirmation.Until(drv => drv.FindElement(By.ClassName("entry-title")).Text.Contains("Order received"));

        }
    }
}
