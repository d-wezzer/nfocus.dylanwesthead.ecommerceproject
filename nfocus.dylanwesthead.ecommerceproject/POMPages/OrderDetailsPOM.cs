using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class OrderDetailsPOM
    {
        private IWebDriver _driver;

        public OrderDetailsPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators
        IWebElement orderNumberField => _driver.FindElement(By.CssSelector(".order > strong"));
        IWebElement orderDetailsFull => _driver.FindElement(By.ClassName("woocommerce-thankyou-order-details"));


        public string getOrderNumber()
        {
            try
            {
                return orderNumberField.Text;
            }
            catch
            {
                return orderNumberField.Text;
            }
        }

        public IWebElement GetOrderDetails()
        {
            return orderDetailsFull;
        }
    }
}
