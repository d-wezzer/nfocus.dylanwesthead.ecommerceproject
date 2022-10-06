/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
 *
 *   - The Page Object Model for the checkout page of the Edgewords eCommerce demo site. 
 */
using nfocus.dylanwesthead.ecommerceproject.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

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
        internal void PopulateBillingInfo(Customer customer)
        {
            SetFirstNameField(customer.First);
            SetSurnameField(customer.Surname);
            SetHomeAddressField(customer.Address);
            SetCityField(customer.Town);
            SetPostcodeField(customer.Postcode);
            SetPhoneField(customer.Phone);
            SetEmailField(customer.Email);
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

        /*
         * Create the Customer Object
         *   - Creates a customer instance with the details passed from feature file.
         *   - Returns the customer object with the given details.
         */
        public Customer SetCustomerDetails(Table customerDetails)
        {
            Dictionary<string, string> customer = ToDictionary(customerDetails);

            string first = customer["firstName"];
            string surname = customer["surname"];
            string address = customer["address"];
            string town = customer["town"];
            string postcode = customer["postcode"];
            string phone = customer["phone"];
            string email = customer["email"];

            return new Customer(first, surname, address, town, postcode, phone, email);
            }

        /*
         * Convert Table to Dictionary
         *   - Takes in a table and maps it out to a dictionary.
         *   - Used to assign meaningful keys to the relevant data.
         */
        internal Dictionary<string, string> ToDictionary(Table table)
        {
            // Retrieves the keys and values from the data table in the feature file.
            Dictionary<string, string> customer = new();
            var x = table.Rows[0];
            var keys = x.Keys.ToArray();
            var values = x.Values.ToArray();

            // Loops through the columns of the row and maps them to dictionary accordingly. 
            for (int i = 0; i < x.Values.Count; i++)
            {
                customer.Add(keys[i], values[i]);
            }

            return customer;
        }
    }
}
