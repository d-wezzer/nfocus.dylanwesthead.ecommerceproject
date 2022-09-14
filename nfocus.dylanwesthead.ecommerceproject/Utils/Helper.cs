using NUnit.Framework;
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

        // Allows the driver a specified amount of time to complete it's action before fully timing out
        public void WaitForElement(int Seconds, By Locator)
        {
            WebDriverWait waitForElemDisplay = new WebDriverWait(_driver, TimeSpan.FromSeconds(Seconds));
            waitForElemDisplay.Until(drv => drv.FindElement(Locator).Displayed);
        }

        public void TakeScreenshotElement(IWebElement elem, string FileName)
        {
            ITakesScreenshot sselem = elem as ITakesScreenshot;
            Screenshot screenshot = sselem.GetScreenshot();
            screenshot.SaveAsFile(@"C:\Users\DylanWesthead\Documents\VS_Screenshots\nfocus.dylanwesthead.ecommerceproject." + FileName + ".png", ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(@"C:\Users\DylanWesthead\Documents\VS_Screenshots\nfocus.dylanwesthead.ecommerceproject." + FileName + ".png");
        }
    }
}
