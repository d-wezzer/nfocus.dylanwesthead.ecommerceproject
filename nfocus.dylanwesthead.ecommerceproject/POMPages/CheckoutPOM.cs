/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
 *
 *   - The Page Object Model for the checkout page of the Edgewords eCommerce demo site. 
 */
using nfocus.dylanwesthead.ecommerceproject.Utils;
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

        /* Locators and elements required to complete the checkout pages billing form and place an order. */
        // The => means each time the variable is used, find element is called.
        private IWebElement _firstNameField => _driver.FindElement(By.Id("billing_first_name"));
        private IWebElement _surnameField => _driver.FindElement(By.Id("billing_last_name"));
        private IWebElement _homeAddressField => _driver.FindElement(By.Id("billing_address_1"));
        private IWebElement _cityField => _driver.FindElement(By.Id("billing_city"));
        private IWebElement _postcodeField => _driver.FindElement(By.Id("billing_postcode"));
        private IWebElement _phoneField => _driver.FindElement(By.Id("billing_phone"));
        private IWebElement _emailField => _driver.FindElement(By.Id("billing_email"));
        private IWebElement _payByChequeRadioButton => _driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label"));
        private IWebElement _placeOrderButton => _driver.FindElement(By.Id("place_order"));


        /*
         * SetFirstNameField(string)
         *   - Enters the given first name into the first name field on the billing form.
         */
        private void SetFirstNameField(string firstName)
        {
            _firstNameField.Clear();
            _firstNameField.SendKeys(firstName);
        }


        /*
         * SetSurnameField(string)
         *   - Enters the given surname into the surname field on the billing form.
         */
        private void SetSurnameField(string surname)
        {
            _surnameField.Clear();
            _surnameField.SendKeys(surname);
        }


        /*
         * SetHomeAddressField(string)
         *   - Enters the given home address into the address field on the billing form.
         */
        private void SetHomeAddressField(string address)
        {
            _homeAddressField.Clear();
            _homeAddressField.SendKeys(address);
        }


        /*
         * SetCityField(string)
         *   - Enters the given city into the city field on the billing form.
         */
        private void SetCityField(string city)
        {
            _cityField.Clear();
            _cityField.SendKeys(city);
        }


        /*
         * SetPostcodeField(string)
         *   - Enters the given postcode into the postcode field on the billing form.
         */
        private void SetPostcodeField(string postcode)
        {
            _postcodeField.Clear();
            _postcodeField.SendKeys(postcode);
        }


        /*
         * SetPhoneField(string)
         *   - Enters the given phone number into the phone field on the billing form.
         */
        private void SetPhoneField(string phone)
        {
            _phoneField.Clear();
            _phoneField.SendKeys(phone);
        }


        /*
         * SetEmailField(string)
         *   - Enters the given email address into the email field on the billing form.
         */
        private void SetEmailField(string email)
        {
            _emailField.Clear();
            _emailField.SendKeys(email);
        }


        /*
         * PopulateBillingInfo(Customer)
         *   - Enters the customers billing info into the billing form.
         *   - Uses setter methods to populate the form correctly.
         */
        internal void PopulateBillingInfo(Customer customer)
        {
            SetFirstNameField(customer._first);
            SetSurnameField(customer._surname);
            SetHomeAddressField(customer._address);
            SetCityField(customer._town);
            SetPostcodeField(customer._postcode);
            SetPhoneField(customer._phone);
            SetEmailField(customer._email);
        }


        /*
         * SelectPayByCheque()
         *   - Clicks the pay by cheque radio button.
         *   - Employs a try/catch to repeat process and prevent stale element exceptions.
         */
        internal void SelectPayByCheque()
        {
            try // Always perform at least once.
            {
                _payByChequeRadioButton.Click();
            }
            catch // Run function again if the 'try' attempt failed.
            {
                _payByChequeRadioButton.Click();
            }
        }


        /*
         * PlaceOrder()
         *   - Clicks the place order button.
         *   - Employs a try/catch to repeat process and prevent stale element exceptions.
         */
        internal void PlaceOrder()
        {
            try // Always perform at least once.
            {
                _placeOrderButton.Click();
            }
            catch // Retry in catch if (try) attempt unsuccessful.
            {
                _placeOrderButton.Click();
            }
        }


        /*
         * WaitForOrderConfirmed()
         *   - Waits for the entry text 'Order received' to be displayed.
         *   - An explicit wait of 5 seconds has been allowed before timeout.
         */
        internal void WaitForOrderConfirmed()
        {
            // Wait for the order details page to be displayed. Creating an order takes a few seconds.
            WebDriverWait waitForOrderConfirmation = new(_driver, TimeSpan.FromSeconds(5));
            waitForOrderConfirmation.Until(drv => drv.FindElement(By.ClassName("entry-title")).Text.Contains("Order received"));

        }


        /*
         * SetCustomerDetails(Table)
         *   - Creates a customer instance with the details passed from feature file.
         *   - Returns the customer object populated with the given details.
         */
        public Customer SetCustomerDetails(Table customerDetails)
        {
            // Convert the provided customer details table to a dictionary.
            Dictionary<string, string> customer = ToDictionary(customerDetails);

            // Assign the customer object properties to the values matching the given keys.
            string first = customer["firstName"]; // "firstName" is a key with the value "Dylan"
            string surname = customer["surname"];
            string address = customer["address"];
            string town = customer["town"];
            string postcode = customer["postcode"];
            string phone = customer["phone"];
            string email = customer["email"];

            return new Customer(first, surname, address, town, postcode, phone, email);
            }


        /*
         * ToDictionary(Table)
         *   - Takes in a table of one customer and maps it out to a dictionary.
         *   - Retrieves the keys and values from the data table in the feature file.
         *   - Used to assign meaningful keys to the relevant data.
         */
        private Dictionary<string, string> ToDictionary(Table table)
        {
            // Retrieves the keys and values from the data table in the feature file.
            var x = table.Rows[0]; // The row of the first customer.
            var keys = x.Keys.ToArray(); // Array of the keys row (table fields).
            var values = x.Values.ToArray(); // Array of the value row (customer details).

            // Loop through columns of the row and maps them to dictionary. 
            Dictionary<string, string> customer = new();
            for (int i = 0; i < x.Values.Count; i++)
            {
                // Add the field and value to the customer dictionary.
                customer.Add(keys[i], values[i]);
            }

            return customer;
        }
    }
}
