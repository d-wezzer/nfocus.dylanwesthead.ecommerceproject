using nfocus.dylanwesthead.ecommerceproject.POMPages;
using nfocus.dylanwesthead.ecommerceproject.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfocus.dylanwesthead.ecommerceproject.StepDefinitions
{
    [Binding]
    public class LoginAndAddToCartStepDefinitions
    {
        private IWebDriver _driver;
        private string baseUrl;
        private readonly ScenarioContext _scenarioContext;

        public LoginAndAddToCartStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["driver"];
            this.baseUrl = (string)_scenarioContext["baseUrl"];
        }


        /*
         * [Given] "I am on the Edgewords eCommerce website"
         *    - Navigates to the Edgewords eCommerce website login page.
         */

        [Given(@"I am on the Edgewords eCommerce website")]
        public void GivenIAmOnTheEdgewordsECommerceWebsite()
        {
            _driver.Url = baseUrl + "/my-account/";

            // If the Store Notice is displayed, dismiss it
            string dismissNotice = "woocommerce-store-notice__dismiss-link";
            if (_driver.FindElement(By.ClassName(dismissNotice)).Displayed)
            {
                _driver.FindElement(By.ClassName(dismissNotice)).Click();
            }
        }


        /*
         * [Given] "I am logged in"
         *    - Logs into the Edgewords eCommerce website with valid credentials.
         */

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            // Log into the website as a registered user
            string email = Environment.GetEnvironmentVariable("email");
            string password = Environment.GetEnvironmentVariable("password");

            LoginPOM loginPage = new LoginPOM(_driver);
            loginPage.LoginWithValidCredentials(email, password);
        }


        /*
         * [When] "I add products to my cart"
         *    - Adds two products to the cart, the 'Hoodie with Logo' and the 'Cap'. 
         */

        [When(@"I add products to my cart")]
        public void WhenIAddProductsToMyCart()
        {
            // Go to shop
            NavPOM_ex navBar = new NavPOM_ex(_driver);
            navBar.goToShop();

            // Allow store contents one second to load
            Helper myhelper = new Helper(_driver);
            myhelper.WaitForElement(1, By.XPath("//main[@id='main']/ul//a[@href='?add-to-cart=31']"));

            // Add items to the cart and navigate to cart
            ShopPOM shopPage = new ShopPOM(_driver);
            shopPage.addItemsToCart();

            // Go to cart
            navBar.goToCart();
        }
    }
}
