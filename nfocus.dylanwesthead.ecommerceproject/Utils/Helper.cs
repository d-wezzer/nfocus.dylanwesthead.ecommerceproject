using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    internal class Helper
    {
        private IWebDriver _driver;

        public Helper(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void WaitForElement(int Seconds, By Locator)
        {
            WebDriverWait waitForElemDisplay = new WebDriverWait(_driver, TimeSpan.FromSeconds(Seconds));
            waitForElemDisplay.Until(drv => drv.FindElement(Locator).Displayed);
        }
    }
}
