/*
 * Author: Dylan Westhead
 * Last Edited: 01/10/2022
 *
 *   - The step definitions used by the coupon scenario.
 *   - Contains all the required information and logic to automate the steps through 
 *     integration of the POMPages. 
 */
using OpenQA.Selenium;
using NUnit.Framework;
using nfocus.dylanwesthead.ecommerceproject.POMPages;
using nfocus.dylanwesthead.ecommerceproject.Utils;

namespace nfocus.dylanwesthead.ecommerceproject.StepDefinitions
{
    [Binding]
    internal class PurchaseWithCouponStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        protected private PurchaseWithCouponStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["driver"];
        }


        /*
         * [When] "I apply a coupon to the cart"
         *    - Applies the 'edgewords' 15% off coupon to the cart.
         */
        [When(@"I apply the coupon '([^']*)' to the cart")]
        protected private void WhenIApplyTheCouponToTheCart(string coupon)
        {   
            // Stale element exception on coupon input field without wait.
            Thread.Sleep(3000);

            // Apply the 15% off coupon
            CartPOM CartPage = new(_driver);
            CartPage.EnterCoupon(coupon).ApplyCoupon();

            // Wait for the coupon to be applied (if not already)
            Helper WaitForCoupon = new Helper(_driver);
            WaitForCoupon.WaitForElement(3, By.ClassName("cart-discount"));
        }


        /*
         * [Then] "15% of the subtotal is deducted"
         *    - Calculates and verifies the coupon savings as well as the grand total.
         */
        [Then(@"'([^']*)'% of the subtotal is deducted")]
        protected private void ThenOfTheSubtotalIsDeducted(int savingsPercentage)
        {
            CartPOM CartPage = new(_driver);

            Decimal BasketTotal = CartPage.GetSubtotalBeforeCoupon();
            
            Decimal ExpectedCouponSavings = BasketTotal / 100 * savingsPercentage; // Works out expected coupon savings.
            Decimal ActualCouponSavings = CartPage.GetCouponSavings(); // Retrieves actual coupon savings grabbed directly from website.
            
            // Works out expected total after coupon savings. E.g. 85% of the total implies a 15% discount
            Decimal ExpectedPriceToPayBeforeShipping = BasketTotal / 100 * (100 - savingsPercentage); // Total / 100 = 1% | 1% * (100 - 85) = 85%
            Decimal ActualPriceToPayBeforeShipping = BasketTotal - ActualCouponSavings;

            // Calculates expected grand total by adding the shipping cost to the discounted total (with coupon).
            Decimal ShippingCost = CartPage.GetShippingCost();
            Decimal PriceToPay = ExpectedPriceToPayBeforeShipping + ShippingCost;

            // Retrieve the grand total (total price to pay after deducting coupon savings and adding shipping cost).
            Decimal GrandTotal = CartPage.GetGrandTotal();

            //Ensure element is visible before screenshot (chromedriver workaround).
            IJavaScriptExecutor? Js = _driver as IJavaScriptExecutor;
            Js.ExecuteScript("arguments[0].scrollIntoView();", _driver.FindElement(By.CssSelector(".cart-collaterals > div")));

            // If the environment variable SCREENSHOT is true, then take a screenshot.
            Helper ElementScreenshot = new(_driver);
            if (Environment.GetEnvironmentVariable("STEPSCREENSHOT") == "true")
            {
                // Takes a screenshot of the cart totals table and attaches to the Test Details (for verification purposes).
                ElementScreenshot.TakeScreenshotElement(CartPage.GetCartTotalsElement(), "all_cart_totals");
            }

            Console.WriteLine($"\nTotal before Coupon: £{BasketTotal}\nExpected Total Savings: £{ExpectedCouponSavings}\nActual Total Savings: £{ActualCouponSavings}");
            Console.WriteLine($"\nExpected Total after Coupon: £{ExpectedPriceToPayBeforeShipping}\nActual Total after Coupon: £{ActualPriceToPayBeforeShipping}");
            Console.WriteLine($"Shipping Cost: £{ShippingCost}\n\nExpected Grand Total: £{PriceToPay}\nActual Grand Total: £{GrandTotal}\n");

            // Verify the coupon saves exactly 15% of the original total.
            Assert.That(ExpectedPriceToPayBeforeShipping, Is.EqualTo(BasketTotal - ActualCouponSavings), $"Not equivalent to {savingsPercentage}% off the original total cost.");

            // Verify the expected grand total is identical to the actual grand total displayed on the webpage.
            Assert.That(PriceToPay, Is.EqualTo(GrandTotal), "Grand total has not been calculated correctly.");
        }

    }
}
