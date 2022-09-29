using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow.Infrastructure;

[assembly: Parallelizable(ParallelScope.Fixtures)] // Can only parallelise Features.
[assembly: LevelOfParallelism(2)] // Worker thread i.e. max amount of Features to run in Parallel.

namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    [Binding]
    internal class Hooks
    {
        private IWebDriver _driver;
        private string _baseUrl = "https://www.edgewordstraining.co.uk/demo-site/";
        private readonly Customer _customer = new Customer("Dylan", "Westhead", "123 Sunshine Road", "St Helens", "WA9 9AW", "01234567890", "dylan.westhead@nfocus.co.uk");
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specflowOutputHelper;

        /*
         * Hooks Constructor
         *   The Hooks() constructor is used for context injection.
         *   We use it to share base data.
         */
        public Hooks(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
        {
            _scenarioContext = scenarioContext;
            _specflowOutputHelper = outputHelper;
        }


        /*
         * Setup Before
         *   This is run once before the features are run. It retrieves the browser and sets up the respective driver agent.
         *   Passes basic info via contect injection like the driver, baseUrl, and customer object.
         */
        [Before]
        public void Setup()
        {
            // Allows customisation of the browser to be used.
            string Browser = Environment.GetEnvironmentVariable("BROWSER");
            switch (Browser)
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

            _scenarioContext["driver"] = _driver;
            _scenarioContext["baseUrl"] = _baseUrl;
            _scenarioContext["customer"] = _customer;
        }


        /*
         * Take Screenshot of Page after every step
         *   This function runs after every step, and when enabled takes screenshots of the entire page after every step is performed from the feature file.
         *   Screenshot functioanlity can be set in the Environment configuration file (.runsettings).
         *   Screenshots are unique with date and time stamps, and then added to the Living Doc html report.
         */
        [AfterStep]
        public void TakeScreenshotAfterStep()
        {   
            // Can set the tests to take screenshots after each step, or not through the .runsettings file.
            if (Environment.GetEnvironmentVariable("STEPSCREENSHOT") == "true")
            {
                if (_driver is ITakesScreenshot ScreenshotCapture)
                {
                    // Get the current date and time in format YYYY-mm-dd_DD-hh-ss, and add that to file name.
                    DateTime now = DateTime.Now;
                    string FileName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestScreenshots\StepScreenshots\" + "StepScreenShot_" + $"{now:yyyy-MM-dd_HH_mm_ss}" + ".png"));

                    // Saves the screenshot to the correct path, and adds it to the Living Doc report.
                    ScreenshotCapture.GetScreenshot().SaveAsFile(FileName);
                    _specflowOutputHelper.AddAttachment(FileName);
                }
            }
        }


        /*
         * Teardown After
         *   The Teardown() function runs at the very end, when all tests have finished running.
         *   Handles quiting any active drivers.
         */
        [After]
        public void Teardown()
        {
            Thread.Sleep(5000);
            _driver.Quit();
        }
    }
}
