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

        // Locators
        IWebElement FirstNameField => _driver.FindElement(By.Id("billing_first_name"));
        IWebElement SurnameField => _driver.FindElement(By.Id("billing_last_name"));
        IWebElement HomeAddressField => _driver.FindElement(By.Id("billing_address_1"));
        IWebElement CityField => _driver.FindElement(By.Id("billing_city"));
        IWebElement PostcodeField => _driver.FindElement(By.Id("billing_postcode"));
        IWebElement PhoneField => _driver.FindElement(By.Id("billing_phone"));
        IWebElement EmailField => _driver.FindElement(By.Id("billing_email"));
        IWebElement PayByChequeRadioButton => _driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label"));
        IWebElement PlaceOrderButton => _driver.FindElement(By.Id("place_order"));


        public void SetFirstNameField(string firstName)
        {
            FirstNameField.Clear();
            FirstNameField.SendKeys(firstName);
        }

        public void SetSurnameField(string surname)
        {
            SurnameField.Clear();
            SurnameField.SendKeys(surname);
        }

        public void SetHomeAddressField(string address)
        {
            HomeAddressField.Clear();
            HomeAddressField.SendKeys(address);
        }

        public void SetCityField(string city)
        {
            CityField.Clear();
            CityField.SendKeys(city);
        }

        public void SetPostcodeField(string postcode)
        {
            PostcodeField.Clear();
            PostcodeField.SendKeys(postcode);
        }

        public void SetPhoneField(string phone)
        {
            PhoneField.Clear();
            PhoneField.SendKeys(phone);
        }

        public void SetEmailField(string email)
        {
            EmailField.Clear();
            EmailField.SendKeys(email);
        }

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

        public void SelectPayByCheque()
        {
            try
            {
                PayByChequeRadioButton.Click();
            }
            catch
            {
                PayByChequeRadioButton.Click();
            }
        }

        public void PlaceOrder()
        {
            try
            {
                PlaceOrderButton.Click();
            }
            catch
            {
                PlaceOrderButton.Click();
            }
        }
    }
}
