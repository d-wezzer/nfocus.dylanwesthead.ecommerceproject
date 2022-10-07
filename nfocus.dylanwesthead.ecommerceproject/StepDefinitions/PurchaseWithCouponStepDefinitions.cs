/*
 * Author: Dylan Westhead
 * Last Edited: 07/10/2022
 *
 *   - The step definitions used by the coupon scenario.
 *   - Contains all the required information and logic to automate the steps through 
 *     integration of the POMPages. 
 */
using OpenQA.Selenium;
using NUnit.Framework;
using nfocus.dylanwesthead.ecommerceproject.POMPages;

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

            CartPOM cartPage = new(_driver);

            // Enter and apply the coupon.
            cartPage.EnterCoupon(coupon);
            cartPage.ApplyCoupon();
        }


        /*
         * [Then] "15% of the subtotal is deducted"
         *    - Calculates and verifies the coupon savings as well as the grand total.
         */
        [Then(@"'([^']*)'% of the subtotal is deducted")]
        protected private void ThenOfTheSubtotalIsDeducted(int savingsPercentage)
        {
            CartPOM cartPage = new(_driver);

            // Calculate and retrieve all the expected and actual totals from the cart page using savings percentage.
            Dictionary<string, decimal> cartTotals = cartPage.CalculateExpectedAndActualTotals(savingsPercentage);

            // If environment variable SCREENSHOT is true, then take a screenshot of all totals on cart page.
            cartPage.ScreenshotAllTotals();
            
            Console.WriteLine($"\nTotal before Coupon: £{cartTotals["subtotalBeforeCoupon"]}\nExpected Total Savings: £{cartTotals["expectedSavings"]}\nActual Total Savings: £{cartTotals["actualSavings"]}");
            Console.WriteLine($"\nExpected Total after Coupon: £{cartTotals["expectedTotalBeforeShipping"]}\nActual Total after Coupon: £{cartTotals["actualTotalBeforeShipping"]}");
            Console.WriteLine($"Shipping Cost: £{cartTotals["shippingCost"]}\n\nExpected Grand Total: £{cartTotals["expectedGrandTotal"]}\nActual Grand Total: £{cartTotals["actualGrandTotal"]}\n");

            // Verify the coupon deducts the correct percentage off the original total, before shipping costs applied.
            Assert.That(cartTotals["expectedTotalBeforeShipping"], Is.EqualTo(cartTotals["subtotalBeforeCoupon"] - cartTotals["actualSavings"]), $"Not equivalent to {savingsPercentage}% off. Coupon incorrectly applies a savings percentage of {cartTotals["actualSavingsPercentage"]}%.");

            // Verify expected grand total is identical to actual grand total displayed on webpage.
            Assert.That(cartTotals["expectedGrandTotal"], Is.EqualTo(cartTotals["actualGrandTotal"]), "Grand total has not been calculated correctly.");
        }

    }
}
