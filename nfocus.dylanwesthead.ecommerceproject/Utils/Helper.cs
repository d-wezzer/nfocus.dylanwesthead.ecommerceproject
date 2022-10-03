﻿/*
 * Author: Dylan Westhead
 * Last Edited: 29/09/2022
 *
 *   - Contains helper functions that are used throughout the program frequently; helps prevent code duplication.
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
         * Wait for an Element to be Displayed
         *   - The WaitForElement() function waits for a given amount of time for an element to be displayed.
         *   - If timeout occurs, an exception is thrown. Helps to prevent stale element exceptions.
         */
        internal void WaitForElement(int seconds, By locator)
        {
            WebDriverWait WaitForElemDisplay = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            //WaitForElemDisplay.Until(drv => drv.FindElement(locator).Displayed);
            WaitForElemDisplay.Until(drv => drv.FindElement(locator).Displayed);
        }


        /*
         * Take Screenshot of an Element
         *   - The TakeScreenshotElement() function takes a screenshot of given element, and customises the file name.
         *   - The file name is respective to the element, timestamped to the current date and time, and stored in the TestSceenshots directory.
         */
        internal void TakeScreenshotElement(IWebElement elem, string element)
        {
            if (Environment.GetEnvironmentVariable("STEPSCREENSHOT") == "true")
            {
                ITakesScreenshot SSelem = elem as ITakesScreenshot;
                Screenshot Screenshot = SSelem.GetScreenshot();

                // Find the current date and time, and reformats to a file friendly format.
                DateTime Now = DateTime.Now;
                string FileName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestScreenshots\" + element + "_" + $"{Now:yyyy-MM-dd_HH_mm_ss}" + ".png"));

                // Saves the screenshot and attaches to the Test Context.
                Screenshot.SaveAsFile(FileName, ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment(FileName);
            }
        }
    }
}
