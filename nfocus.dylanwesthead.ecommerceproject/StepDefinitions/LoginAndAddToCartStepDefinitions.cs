/*
 * Author: Dylan Westhead
 * Last Edited: 01/10/2022
 *
 *   - The step definitions used by both the order number verification and coupon scenarios.
 *   - Contains all the required information and logic to automate the steps through 
 *     integration of the POMPages. 
 */
using OpenQA.Selenium;
using nfocus.dylanwesthead.ecommerceproject.POMPages;
using nfocus.dylanwesthead.ecommerceproject.Utils;

namespace nfocus.dylanwesthead.ecommerceproject.StepDefinitions
{
    [Binding]
    internal class LoginAndAddToCartStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl;
        private readonly ScenarioContext _scenarioContext;

        protected private LoginAndAddToCartStepDefinitions(ScenarioContext scenarioContext)
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
        protected private void GivenIAmOnTheEdgewordsECommerceWebsite()
        {
            _driver.Url = _baseUrl + "/my-account/";

            // If the Store Notice is displayed, dismiss it.
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
        protected private void GivenIAmLoggedIn()
        {
            // Log into the website as a registered user
            string Email = Environment.GetEnvironmentVariable("email");
            string Password = Environment.GetEnvironmentVariable("password");

            LoginPOM LoginPage = new(_driver);
            LoginPage.LoginWithValidCredentials(Email, Password);
        }


        /*
         * [When] "I add product1 and product2 to my cart"
         *    - Adds two products to the cart, the 'Hoodie with Logo' and the 'Cap' by default. 
         */
        [When(@"I add '([^']*)' and '([^']*)' to my cart")]
        protected private void WhenIAddAndToMyCart(string product1, string product2)
        {
            NavigationBarPOM NavBar = new(_driver);
            ShopPOM ShopPage = new(_driver);

            // Loop through the products passed in though feature files.
            ShopPage.SearchForAndAddProduct(product1, product2);

            // Adds the item to cart, should only be one button.
            ShopPage.AddItemToCart();

            // Go to cart once updated with the items.
            NavBar.GoToCart();
        }

        /*
         * [When] "I edit a product quantity directly from the cart"
         *    - Edits the quantity of a product item to 12345 and updates the cart.
         */
        [When(@"I edit product quantity to '([^']*)' directly from cart")]
        protected private void WhenIEditProductQuantityToDirectlyFromCart(string quantity)
        {
            // Change product quantity directyl from the cart page
            CartPOM CartPage = new(_driver);
            CartPage.ChangeProductQuantity(quantity);

            // Update the totals after editing the cartS
            CartPage.UpdateCart();
        }
    }
}
