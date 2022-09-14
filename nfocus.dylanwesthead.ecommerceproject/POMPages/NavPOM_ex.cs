using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class NavPOM_ex
    {

        private IWebDriver _driver;

        public NavPOM_ex(IWebDriver driver)
        {
            this._driver = driver;
        }

        // locators
        IWebElement homeLink => _driver.FindElement(By.LinkText("Home"));
        IWebElement shopLink => _driver.FindElement(By.LinkText("Shop"));
        IWebElement cartLink => _driver.FindElement(By.LinkText("Cart"));
        IWebElement checkoutLink => _driver.FindElement(By.LinkText("Checkout"));
        IWebElement myAccountLink => _driver.FindElement(By.LinkText("My account"));


        public void goToHome()
        {
            homeLink.Click();
        }

        public void goToShop()
        {
            shopLink.Click();
        }

        public void goToCart()
        {
            cartLink.Click();
        }

        public void goToCheckout()
        {
            checkoutLink.Click();
        }

        public void goToMyAccount()
        {
            myAccountLink.Click();
        }

    }
}
