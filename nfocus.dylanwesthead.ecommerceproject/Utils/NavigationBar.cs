using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    internal class NavigationBar
    {

        private IWebDriver _driver;

        public NavigationBar(IWebDriver driver)
        {
            _driver = driver;
        }

        // locators
        IWebElement HomeLink => _driver.FindElement(By.LinkText("Home"));
        IWebElement ShopLink => _driver.FindElement(By.LinkText("Shop"));
        IWebElement CartLink => _driver.FindElement(By.LinkText("Cart"));
        IWebElement CheckoutLink => _driver.FindElement(By.LinkText("Checkout"));
        IWebElement MyAccountLink => _driver.FindElement(By.LinkText("My account"));


        public void GoToHome()
        {
            HomeLink.Click();
        }

        public void GoToShop()
        {
            ShopLink.Click();
        }

        public void GoToCart()
        {
            CartLink.Click();
        }

        public void GoToCheckout()
        {
            CheckoutLink.Click();
        }

        public void GoToMyAccount()
        {
            MyAccountLink.Click();
        }

    }
}
