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

        // Locators needed for the login page. The => means each time the variable is used, find element is called.
        IWebElement UsernameField => _driver.FindElement(By.Id("username"));
        IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        IWebElement LoginButton => _driver.FindElement(By.Name("login"));

        // Sets the given userame into the username field.
        public LoginPOM SetUsername(string username)
        {
            UsernameField.Clear();
            UsernameField.SendKeys(username);
            return this;
        }

        // Sets the given password in the password field.
        public LoginPOM SetPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            return this;
        }

        // Clicks the login link.
        public void GoLogin()
        {
            LoginButton.Click();
        }

        // Logs in with valid credentials.
        public bool LoginWithValidCredentials(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            GoLogin();

            // If the login is not successful, then we receive an alert. If alert is not detected, catch is triggered to show successful login.
            try
            {
                _driver.FindElement(By.ClassName("woocommerce-error"));
            }
            catch (Exception error)
            {
                return true; // Alert not displayed so user was able to login.
            }
            return false; // If alert is visible then we couldnt login with the right details.
        }
    }
}

