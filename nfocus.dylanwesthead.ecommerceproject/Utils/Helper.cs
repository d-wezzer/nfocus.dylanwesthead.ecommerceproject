/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
 *
 *   - Helper class with frequently used functions. Helps prevent code duplication throughout program.
 */
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    internal class Helper
    {
        private readonly IWebDriver _driver;

        internal Helper(IWebDriver driver)
        {
            this._driver = driver;
        }


        /*
         * WaitForElement(int, By)
         *   - Waits a given amount of time for an element to be displayed, before timing out.
         *   - If timeout occurs, an exception is thrown. Helps to prevent stale element exceptions.
         */
        internal void WaitForElement(int seconds, By locator)
        {
            WebDriverWait waitForElemDisplay = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            waitForElemDisplay.Until(drv => drv.FindElement(locator).Displayed);
        }


        /*
         * TakeScreenshotElement(IWebElement, string)
         *   - Takes screenshot of a given element, and customises the file name.
         *   - File name is respective to the element, timestamped to the current date and time.
         *   - Image is stored in the /TestSceenshots directory.
         *   - Only functions if the STEPSCREENSHOT env variable is set to true.
         */
        internal void TakeScreenshotElement(IWebElement elem, string element)
        {
            if (Environment.GetEnvironmentVariable("STEPSCREENSHOT") == "true")
            {
                // Passes in element and screenshots that element.
                ITakesScreenshot ssElem = elem as ITakesScreenshot;
                Screenshot screenshot = ssElem.GetScreenshot();

                // Gets current date and time, and reformats to a file friendly format.
                DateTime now = DateTime.Now;
                string fileName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestScreenshots\" + element + "_" + $"{now:yyyy-MM-dd_HH_mm_ss}" + ".png"));

                // Saves the screenshot and attaches to the Test Context.
                screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment(fileName);
            }
        }
    }
}
