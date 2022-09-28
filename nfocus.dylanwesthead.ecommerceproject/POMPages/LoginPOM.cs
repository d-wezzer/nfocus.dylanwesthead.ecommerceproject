using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class LoginPOM
    {
        private IWebDriver _driver;

        public LoginPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators. The => means each time the variable is used, find element is called 
        IWebElement UsernameField => _driver.FindElement(By.Id("username"));
        IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        IWebElement LoginButton => _driver.FindElement(By.Name("login"));

        public LoginPOM SetUsername(string username)
        {
            UsernameField.Clear();
            UsernameField.SendKeys(username);
            return this;
        }

        public LoginPOM SetPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            return this;
        }

        public void GoLogin()
        {
            LoginButton.Click();
        }

        public bool LoginWithValidCredentials(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            GoLogin();

            try
            {
                _driver.FindElement(By.ClassName("woocommerce-error"));
            }
            catch (Exception error)
            {
                return true; // Alert not displayed so user was able to login
            }
            return false; // If alert is visible then we couldnt login with the right details.
        }

        public bool LoginWithInvalidCredentials(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            GoLogin();

            try
            {
                // Attempt to switch to alert, if alert is displayed then login was unsuccessful
                _driver.FindElement(By.ClassName("woocommerce-error"));
            }
            catch (Exception error)
            {
                return false; // Error, you should not have been able to login with invalid credentials!
            }
            return true; // The expected result, user did not login.
        }
    }
}

