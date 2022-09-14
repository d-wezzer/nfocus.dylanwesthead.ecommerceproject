using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IWebElement ordersLink => _driver.FindElement(By.LinkText("Orders"));
        IWebElement logOutLink => _driver.FindElement(By.LinkText("Logout"));

        public void goToOrders()
        {
            ordersLink.Click();
        }

        public void logOutOfAccount()
        {
            logOutLink.Click();
        }
    }
}
