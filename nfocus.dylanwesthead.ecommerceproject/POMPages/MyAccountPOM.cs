using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class MyAccountPOM
    {
        private IWebDriver _driver;
        public MyAccountPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators
        IWebElement OrdersLink => _driver.FindElement(By.LinkText("Orders"));
        IWebElement LogOutLink => _driver.FindElement(By.LinkText("Logout"));

        public void goToOrders()
        {
            OrdersLink.Click();
        }

        public void logOutOfAccount()
        {
            LogOutLink.Click();
        }
    }
}
