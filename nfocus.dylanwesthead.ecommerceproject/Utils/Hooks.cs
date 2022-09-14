using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Before]
        public void Setup()
        {
            string browser = Environment.GetEnvironmentVariable("BROWSER");
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

            _scenarioContext["driver"] = _driver;
            _scenarioContext["baseUrl"] = _baseUrl;
            _scenarioContext["customer"] = _customer;
        }

        [After]
        public void Teardown()
        {
            Thread.Sleep(5000);
            _driver.Quit();
        }
    }
}
