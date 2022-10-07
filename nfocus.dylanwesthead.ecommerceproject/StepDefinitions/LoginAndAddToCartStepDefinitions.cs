/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
 *
 *   - The step definitions used by both the order number verification and coupon scenarios.
 *   - Contains all the required information and logic to automate the steps through 
 *     integration of the POMPages. 
 */
using OpenQA.Selenium;
using nfocus.dylanwesthead.ecommerceproject.POMPages;

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
            // Navigates to login of Edgewords eCommerce site.
            _driver.Url = _baseUrl + "/my-account/";
        }


        /*
         * [Given] "I am logged in"
         *    - Logs into the Edgewords eCommerce website as a registered customer.
         */
        [Given(@"I am logged in")]
        protected private void GivenIAmLoggedIn()
        {
            // Retrieves sensitive email and password from environment. If variable is null, throw error.
            string email = Environment.GetEnvironmentVariable("email") ?? "Unknown environment variable.";
            string password = Environment.GetEnvironmentVariable("password") ?? "Unknown environment variable.";

            LoginPOM loginPage = new(_driver);

            // Dismisses store notice and logs in.
            loginPage.DismissNotice();
            loginPage.LoginWithValidCredentials(email, password);
        }


        /*
         * [When] "I add product1 and product2 to my cart"
         *    - Adds two products to the cart, the 'Hoodie with Logo' and the 'Cap' by default. 
         */
        [When(@"I add '([^']*)' and '([^']*)' to my cart")]
        protected private void WhenIAddAndToMyCart(string product1, string product2)
        {
            // POM Pages used in this step (in order).
            NavigationBarPOM navBar = new(_driver);
            ShopPOM shopPage = new(_driver);

            // Searches for products passed in from feature file and adds to cart.
            shopPage.SearchForAndAddProduct(product1, product2);

            // Navigate to the cart.
            navBar.GoToCart();
        }

        /*
         * [When] "I edit a product quantity directly from the cart"
         *    - Edits the quantity of a product item to 12345 and updates the cart.
         */
        [When(@"I edit product quantity to '([^']*)' directly from cart")]
        protected private void WhenIEditProductQuantityToDirectlyFromCart(string quantity)
        {
            CartPOM cartPage = new(_driver);

            // Change product quantity directly from the cart page.
            cartPage.ChangeProductQuantity(quantity);

            // Update totals after editing cart.
            cartPage.UpdateCart();
        }
    }
}
