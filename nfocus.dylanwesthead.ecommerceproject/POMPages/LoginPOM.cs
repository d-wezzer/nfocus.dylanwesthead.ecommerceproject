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

        // Locators needed for the login page. The => means each time the variable is used, find element is called.
        private IWebElement UsernameField => _driver.FindElement(By.Id("username"));
        private IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Name("login"));

        // Sets the given userame into the username field.
        private LoginPOM SetUsername(string username)
        {
            UsernameField.Clear();
            UsernameField.SendKeys(username);
            return this;
        }

        // Sets the given password in the password field.
        private LoginPOM SetPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            return this;
        }

        // Clicks the login link.
        internal void GoLogin()
        {
            LoginButton.Click();
        }

        // Dismisses the store notice if it is displayed.
        internal void DismissNotice()
        {
            // If the Store Notice is displayed, dismiss it.
            string DismissNotice = "woocommerce-store-notice__dismiss-link";
            if (_driver.FindElement(By.ClassName(DismissNotice)).Displayed)
            {
                _driver.FindElement(By.ClassName(DismissNotice)).Click();
            }
        }

        /*
         * Log in with Valid Credentials
         *   - Logs into the website with the provided credentials. Valid credentials are expected.
         *   - returns bool (true = success)
         */
        internal bool LoginWithValidCredentials(string username, string password)
        {
            SetUsername(username).SetPassword(password);
            GoLogin();

            // If the login is not successful, then we receive an alert.
            // If alert is not detected, catch is triggered to show successful login.
            try
            {
                _driver.FindElement(By.ClassName("woocommerce-error"));
            }
            catch
            {
                return true; // Alert not displayed so user was able to login.
            }
            return false; // If alert is visible then we couldnt login with the right details.
        }
    }
}

