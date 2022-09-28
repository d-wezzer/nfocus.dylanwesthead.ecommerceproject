using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
            WebDriverWait WaitForElemDisplay = new WebDriverWait(_driver, TimeSpan.FromSeconds(Seconds));
            WaitForElemDisplay.Until(drv => drv.FindElement(Locator).Displayed);
        }

        public void TakeScreenshotElement(IWebElement Elem, string Element)
        {
            ITakesScreenshot SSelem = Elem as ITakesScreenshot;
            Screenshot Screenshot = SSelem.GetScreenshot();
            
            string FileName = Path.ChangeExtension(@"C:\Users\DylanWesthead\source\repos\nfocus.dylanwesthead.ecommerceproject\nfocus.dylanwesthead.ecommerceproject\bin\TestScreenshots\" + Element + "_" + Path.GetRandomFileName() + ".png",  "png");
            Screenshot.SaveAsFile(FileName, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(FileName);
        }
    }
}
