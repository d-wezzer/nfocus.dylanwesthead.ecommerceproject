using OpenQA.Selenium;
using nfocus.dylanwesthead.ecommerceproject.POMPages;
using nfocus.dylanwesthead.ecommerceproject.Utils;

namespace nfocus.dylanwesthead.ecommerceproject.StepDefinitions
{
    [Binding]
    public class LoginAndAddToCartStepDefinitions
    {
        private IWebDriver _driver;
        private readonly string _baseUrl;
        private readonly ScenarioContext _scenarioContext;

        public LoginAndAddToCartStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["driver"];
            this._baseUrl = (string)_scenarioContext["baseUrl"];
        }


        /*
         * [Given] "I am on the Edgewords eCommerce website"
         *    - Navigates to the Edgewords eCommerce website login page.
         */

        [Given(@"I am on the Edgewords eCommerce website")]
        public void GivenIAmOnTheEdgewordsECommerceWebsite()
        {
            _driver.Url = _baseUrl + "/my-account/";

            // If the Store Notice is displayed, dismiss it
            string DismissNotice = "woocommerce-store-notice__dismiss-link";
            if (_driver.FindElement(By.ClassName(DismissNotice)).Displayed)
            {
                _driver.FindElement(By.ClassName(DismissNotice)).Click();
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
            string Email = Environment.GetEnvironmentVariable("email");
            string Password = Environment.GetEnvironmentVariable("password");

            LoginPOM LoginPage = new(_driver);
            LoginPage.LoginWithValidCredentials(Email, Password);
        }


        /*
         * [When] "I add products to my cart"
         *    - Adds two products to the cart, the 'Hoodie with Logo' and the 'Cap'. 
         */

        [When(@"I add products to my cart")]
        public void WhenIAddProductsToMyCart()
        {
            // Go to shop
            NavigationBar NavBar = new(_driver);
            NavBar.GoToShop();

            // Allow store contents one second to load
            Helper Myhelper = new(_driver);
            Myhelper.WaitForElement(1, By.XPath("//main[@id='main']/ul//a[@href='?add-to-cart=31']"));

            // Add items to the cart
            ShopPOM ShopPage = new(_driver);
            ShopPage.addItemsToCart();

            // Go to cart once the cart has updated with the items
            Myhelper.WaitForElement(2, By.LinkText("View cart"));
            NavBar.GoToCart();
        }
    }
}
