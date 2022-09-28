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

        // Locators for the orders page and logout link. The => means each time the variable is used, find element is called.
        IWebElement OrdersLink => _driver.FindElement(By.LinkText("Orders"));
        IWebElement LogOutLink => _driver.FindElement(By.LinkText("Logout"));

        // Navigates to the orders page link from the my account page.
        public void GoToOrders()
        {
            OrdersLink.Click();
        }

        // Logs out from the my account page.
        public void LogOutOfAccount()
        {
            LogOutLink.Click();
        }
    }
}
