/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
 *
 *   - The Page Object Model for the login page of the Edgewords eCommerce demo site. 
 */
using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class LoginPOM
    {
        private readonly IWebDriver _driver;

        internal LoginPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        /* Locators and elements needed to login on the login page. */
        // The => means each time the variable is used, find element is called.
        private IWebElement _usernameField => _driver.FindElement(By.Id("username"));
        private IWebElement _passwordField => _driver.FindElement(By.Id("password"));
        private IWebElement _loginButton => _driver.FindElement(By.Name("login"));


        /*
         * SetUsername(string)
         *   - Enters the given username into the username field.
         */
        private LoginPOM SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);
            return this;
        }


        /*
         * SetPassword(string)
         *   - Enters the given password into the password field.
         */
        private LoginPOM SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);
            return this;
        }


        /*
         * GoLogin()
         *   - Clicks the login button.
         */
        internal void GoLogin()
        {
            _loginButton.Click();
        }


        /*
         * DismissNotice()
         *   - Dismisses the store notice if it is displayed.
         */
        internal void DismissNotice()
        {
            // If the Store Notice is displayed, dismiss it.
            string dismissNotice = "woocommerce-store-notice__dismiss-link";
            if (_driver.FindElement(By.ClassName(dismissNotice)).Displayed)
            {
                _driver.FindElement(By.ClassName(dismissNotice)).Click();
            }
        }


        /*
         * LoginWithValidCredentials(string, string)
         *   - Logs into the website with the provided credentials - valid credentials expected.
         *   - Returns bool to indicate outcome. (true = success)
         */
        internal bool LoginWithValidCredentials(string username, string password)
        {
            SetUsername(username).SetPassword(password);
            GoLogin();

            // If the login is not successful, then we receive an alert.
            try
            {
                _driver.FindElement(By.ClassName("woocommerce-error"));
            }
            catch
            {

                // If alert not detected, catch is triggered to return successful login.
                return true; 
            }
            return false; // If alert visible then we entered invalid credentials.
        }
    }
}

