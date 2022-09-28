using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class CheckoutPOM
    {
        private IWebDriver _driver;

        public CheckoutPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators for the customer details field elements used on the billing page. The => means each time the variable is used, find element is called.
        IWebElement FirstNameField => _driver.FindElement(By.Id("billing_first_name"));
        IWebElement SurnameField => _driver.FindElement(By.Id("billing_last_name"));
        IWebElement HomeAddressField => _driver.FindElement(By.Id("billing_address_1"));
        IWebElement CityField => _driver.FindElement(By.Id("billing_city"));
        IWebElement PostcodeField => _driver.FindElement(By.Id("billing_postcode"));
        IWebElement PhoneField => _driver.FindElement(By.Id("billing_phone"));
        IWebElement EmailField => _driver.FindElement(By.Id("billing_email"));
        IWebElement PayByChequeRadioButton => _driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label"));
        IWebElement PlaceOrderButton => _driver.FindElement(By.Id("place_order"));

        // Enters the given first name into the corresponding field on the billing form.
        public void SetFirstNameField(string firstName)
        {
            FirstNameField.Clear();
            FirstNameField.SendKeys(firstName);
        }

        // Enters the given surname into the corresponding field on the billing form.
        public void SetSurnameField(string surname)
        {
            SurnameField.Clear();
            SurnameField.SendKeys(surname);
        }

        // Enters the given address into the corresponding field on the billing form.
        public void SetHomeAddressField(string address)
        {
            HomeAddressField.Clear();
            HomeAddressField.SendKeys(address);
        }

        // Enters the given city into the corresponding field on the billing form.
        public void SetCityField(string city)
        {
            CityField.Clear();
            CityField.SendKeys(city);
        }

        // Enters the given postcode into the corresponding field on the billing form.
        public void SetPostcodeField(string postcode)
        {
            PostcodeField.Clear();
            PostcodeField.SendKeys(postcode);
        }

        // Enters the given phone number into the corresponding field on the billing form.
        public void SetPhoneField(string phone)
        {
            PhoneField.Clear();
            PhoneField.SendKeys(phone);
        }

        // Enters the given email into the corresponding field on the billing form.
        public void SetEmailField(string email)
        {
            EmailField.Clear();
            EmailField.SendKeys(email);
        }

        // Uses all the above methods to populate the billing form with the correct information.
        public void PopulateBillingInfo(string first, string surname, string address, string city, string postcode, string phone, string email)
        {
            SetFirstNameField(first);
            SetSurnameField(surname);
            SetHomeAddressField(address);
            SetCityField(city);
            SetPostcodeField(postcode);
            SetPhoneField(phone);
            SetEmailField(email);
        }

        // Clicks the pay by dheque radio button. Uses a try/catch to repeat the process and prevent stale element exceptions.
        public void SelectPayByCheque()
        {
            try // Always perform at least once.
            {
                PayByChequeRadioButton.Click();
            }
            catch // Run function again if the 'try' attempt failed.
            {
                PayByChequeRadioButton.Click();
            }
        }

        // Clicks the place order button. Uses a try/catch to repeat the process and prevent any stale element exceptions.
        public void PlaceOrder()
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
    }
}
