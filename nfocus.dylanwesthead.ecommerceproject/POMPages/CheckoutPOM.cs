using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IWebElement firstNameField => _driver.FindElement(By.Id("billing_first_name"));
        IWebElement surnameField => _driver.FindElement(By.Id("billing_last_name"));
        IWebElement homeAddressField => _driver.FindElement(By.Id("billing_address_1"));
        IWebElement cityField => _driver.FindElement(By.Id("billing_city"));
        IWebElement postcodeField => _driver.FindElement(By.Id("billing_postcode"));
        IWebElement phoneField => _driver.FindElement(By.Id("billing_phone"));
        IWebElement emailField => _driver.FindElement(By.Id("billing_email"));
        IWebElement payByChequeRadioButton => _driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label"));
        IWebElement placeOrderButton => _driver.FindElement(By.Id("place_order"));


        public void setFirstNameField(string firstName)
        {
            firstNameField.Clear();
            firstNameField.SendKeys(firstName);
        }

        public void setSurnameField(string surname)
        {
            surnameField.Clear();
            surnameField.SendKeys(surname);
        }

        public void setHomeAddressField(string address)
        {
            homeAddressField.Clear();
            homeAddressField.SendKeys(address);
        }

        public void setCityField(string city)
        {
            cityField.Clear();
            cityField.SendKeys(city);
        }

        public void setPostcodeField(string postcode)
        {
            postcodeField.Clear();
            postcodeField.SendKeys(postcode);
        }

        public void setPhoneField(string phone)
        {
            phoneField.Clear();
            phoneField.SendKeys(phone);
        }

        public void setEmailField(string email)
        {
            emailField.Clear();
            emailField.SendKeys(email);
        }

        public void populateBillingInfo(string first, string surname, string address, string city, string postcode, string phone, string email)
        {
            setFirstNameField(first);
            setSurnameField(surname);
            setHomeAddressField(address);
            setCityField(city);
            setPostcodeField(postcode);
            setPhoneField(phone);
            setEmailField(email);
        }

        public void selectPayByCheque()
        {
            try
            {
                payByChequeRadioButton.Click();
            }
            catch
            {
                payByChequeRadioButton.Click();
            }
        }

        public void placeOrder()
        {
            try
            {
                placeOrderButton.Click();
            }
            catch
            {
                placeOrderButton.Click();
            }
        }
    }
}
