/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
 *
 *   - Hooks class used to share data and perform tasks at given points in the test process.
 *   - Contains methods to setup the environment, clean up the environment when finished, and screenshot after each test step.
 *   - Shares data between scenarios through the use of scenario context.
 */
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Globalization;
using TechTalk.SpecFlow.Infrastructure;

[assembly: Parallelizable(ParallelScope.Fixtures)] // States features can be run in paralell.
[assembly: LevelOfParallelism(2)] // Worker thread i.e. set max amount of Features to run in parallel.

namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    [Binding]
    internal class Hooks
    {
        private IWebDriver _driver;
        private readonly string _baseUrl = "https://www.edgewordstraining.co.uk/demo-site/";
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specflowOutputHelper;

        /*
         * Hooks(ScenarioContext, ISpecFlowOutputHelper)
         *   - Allows context injection through scenario context - used to share base data between scenarios.
         *   - Capable of running extra automation logic at given stages of the test process.
         */
#pragma warning disable CS8618 // Non-nullable field '_driver' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
        protected private Hooks(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
#pragma warning restore CS8618 // Non-nullable field '_driver' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
        {
            _scenarioContext = scenarioContext;
            _specflowOutputHelper = outputHelper;
        }


        /*
         * Setup()
         *   - Runs once before the features are run.
         *   - Retrieves the browser from env variavke and sets up respective driver agent.
         *   - Passes base info via context injection.
         */
        [Before]
        protected private void Setup()
        {
            // Allows customisation of the browser to be used.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string browser = Environment.GetEnvironmentVariable("BROWSER");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            switch (browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "chrome":
                    _driver = new ChromeDriver();
                    break;
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                default:
                    Console.WriteLine("Invalid broswer driver selected, using Chrome by default.");
                    _driver = new ChromeDriver();
                    break;
            }
            _driver.Manage().Window.Maximize();

            // Store the driver and baseUrl objects in the scenario context.
            _scenarioContext["driver"] = _driver;
            _scenarioContext["baseUrl"] = _baseUrl;
        }


        /*
         * TakeScreenshotAfterStep()
         *   - Runs after every step.
         *   - Takes screenshots of the entire page after every step is performed if enabled.
         *   - Screenshot functioanlity can be set via setting the STEPSCRRENSHOT env variable to true.
         *   - Screenshots are unique with date and time stamps, and then added to the Living Doc html report.
         */
        [AfterStep]
        protected private void TakeScreenshotAfterStep()
        {   
            // Can set the tests to take screenshots after each step, or not through the .runsettings file.
            if (Environment.GetEnvironmentVariable("STEPSCREENSHOT") == "true")
            {
                TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                if (_driver is ITakesScreenshot ScreenshotCapture)
                {
                    // Retrieve current scenario in PascalCasing.
                    string scenario = _scenarioContext.ScenarioInfo.Title.ToLower();
                    scenario = info.ToTitleCase(scenario).Replace(" ", string.Empty);

                    // Get the current working directory, date and time in format YYYY-mm-dd_DD-hh-ss, and add to file name.
                    DateTime now = DateTime.Now;
                    string fileName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestScreenshots\StepScreenshots\" + scenario + $"{now:yyyy-MM-dd_HH_mm_ss}" + ".png"));

                    // Saves the screenshot to the correct path, and adds it to the Living Doc report.
                    ScreenshotCapture.GetScreenshot().SaveAsFile(fileName);
                    _specflowOutputHelper.AddAttachment(fileName);
                }
            }
        }


        /*
         * Teardown()
         *   - Runs once at the very end, when all tests have finished running.
         *   - Handles quiting any active drivers.
         */
        [After]
        protected private void Teardown()
        {
            Thread.Sleep(3500);
            _driver.Quit();
        }
    }
}
