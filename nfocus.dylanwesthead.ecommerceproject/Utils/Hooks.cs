using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Reflection.Metadata;
using System.Xml.Linq;
using TechTalk.SpecFlow.Infrastructure;

[assembly: Parallelizable(ParallelScope.Fixtures)] //Can only parallelise Features
[assembly: LevelOfParallelism(2)] //Worker thread i.e. max amount of Features to run in Parallel

namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    [Binding]
    internal class Hooks
    {
        private IWebDriver _driver;
        private string _baseUrl = "https://www.edgewordstraining.co.uk/demo-site/";
        private Customer _customer = new Customer("Dylan", "Westhead", "123 Sunshine Road", "St Helens", "WA9 9AW", "01234567890", "dylan.westhead@nfocus.co.uk");
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specflowOutputHelper;


        public Hooks(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
        {
            _scenarioContext = scenarioContext;
            _specflowOutputHelper = outputHelper;
        }


        [Before]
        public void Setup()
        {
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

        [AfterStep]
        public void TakeScreenshotAfterStep()
        {   
            // Can set the tests to take screenshots after each step, or not through the .runsettings file.
            if (Environment.GetEnvironmentVariable("STEPSCREENSHOT") == "true")
            {
                if (_driver is ITakesScreenshot ScreenshotCapture)
                {
                    DateTime now = DateTime.Now;
                    string FileName = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestScreenshots\StepScreenshots\" + "StepScreenShot_" + $"{now:yyyy-MM-dd_HH_mm_ss}" + ".png"));

                    ScreenshotCapture.GetScreenshot().SaveAsFile(FileName);
                    _specflowOutputHelper.AddAttachment(FileName);
                }
            }
        }

        [After]
        public void Teardown()
        {
            Thread.Sleep(5000);
            _driver.Quit();
        }
    }
}
