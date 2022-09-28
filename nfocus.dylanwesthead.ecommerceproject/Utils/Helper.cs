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
        public void WaitForElement(int seconds, By locator)
        {
            WebDriverWait WaitForElemDisplay = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            WaitForElemDisplay.Until(drv => drv.FindElement(locator).Displayed);
        }

        public void TakeScreenshotElement(IWebElement elem, string element)
        {
            ITakesScreenshot SSelem = elem as ITakesScreenshot;
            Screenshot Screenshot = SSelem.GetScreenshot();

            DateTime now = DateTime.Now;
            string FileName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestScreenshots\" + element + "_" + $"{now:yyyy-MM-dd_HH_mm_ss}" + ".png"));

            Screenshot.SaveAsFile(FileName, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(FileName);
        }
    }
}
