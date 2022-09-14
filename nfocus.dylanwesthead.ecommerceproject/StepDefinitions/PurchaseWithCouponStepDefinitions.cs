using nfocus.dylanwesthead.ecommerceproject.POMPages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using nfocus.dylanwesthead.ecommerceproject.Utils;
using NUnit.Framework;

namespace nfocus.dylanwesthead.ecommerceproject.StepDefinitions
{
    [Binding]
    public class PurchaseWithCouponStepDefinitions
    {
        private IWebDriver _driver;
        private string baseUrl;
        private readonly ScenarioContext _scenarioContext;

        public PurchaseWithCouponStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["driver"];
            this.baseUrl = (string)_scenarioContext["baseUrl"];
        }

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

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            // Log into the website as a registered user
            string email = Environment.GetEnvironmentVariable("email");
            string password = Environment.GetEnvironmentVariable("password");

            LoginPOM loginPage = new LoginPOM(_driver);
            loginPage.LoginWithValidCredentials(email, password);
        }

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

        [When(@"I apply a coupon to the cart")]
        public void WhenIApplyACouponToTheCart()
        {
            // Apply the 15% off coupon
            CartPOM cartPage = new CartPOM(_driver);
            cartPage.enterCoupon("edgewords").applyCoupon();

            /* ---------------------------------------------------------------------------------------------------------------------- */
            // If the coupon has already been applied then this waitForCoupon does nothing and leaves the basket no time to update the totals

            // Wait for the coupon to be applied
            Helper waitForCoupon = new Helper(_driver);
            waitForCoupon.WaitForElement(3, By.ClassName("cart-discount"));

            // Using a thread.sleep to bypass (if the waitForCoupon was too fast due to it already being applied)
            Thread.Sleep(1000);
        }

        [Then(@"15% of the subtotal is deducted")]
        public void ThenOfTheSubtotalIsDeducted()
        {
            CartPOM cartPage = new CartPOM(_driver);

            Decimal basketTotal = cartPage.getSubtotalBeforeCoupon();
            Decimal couponSavings = cartPage.getCouponSavings();

            // Works out 85% of the total, implying a 15% discount
            Decimal priceToPay = basketTotal / 100 * 85; // Total / 100 = 1% | 1% * 85 = 85%
            Console.WriteLine($"\nTotal before Coupon: {basketTotal}\nTotal Savings: {couponSavings}\nTotal after Coupon: {priceToPay}");

            // Verify the coupon saves exactly 15% of the original total
            Assert.That(priceToPay, Is.EqualTo(basketTotal - couponSavings), "Not equivalent to 15% off the original total cost.");

            // Calculates expected grand total by adding the shipping cost to the discounted total (with coupon)
            Decimal shippingCost = cartPage.getShippingCost();
            priceToPay += shippingCost;

            // Retrieve the grand total (total price to pay after deducting coupon savings and adding shipping cost)
            Decimal grandTotal = cartPage.getGrandTotal();

            Console.WriteLine($"Shipping Cost: {shippingCost}\n\nExpected Grand Total: {priceToPay}\nActual Grand Total: {grandTotal}\n");

            // Verify the expected grand total is identical to the actual grand total displayed on the webpage
            Assert.That(priceToPay, Is.EqualTo(grandTotal), "Grand total has not been calculated correctly.");
        }

    }
}
