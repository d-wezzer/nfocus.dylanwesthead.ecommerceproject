using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class AllOrdersPOM
    {
        private IWebDriver _driver;

        public AllOrdersPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators
        IWebElement orderNumbers => _driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number"));
        IWebElement allOrderNumbersTable => _driver.FindElement(By.ClassName("woocommerce-MyAccount-orders"));

        public string getTopOrderNumber()
        {
            return orderNumbers.Text;
        }

        public IWebElement getAllOrderNumbersTable()
        {
            return allOrderNumbersTable;
        }
    }
}
