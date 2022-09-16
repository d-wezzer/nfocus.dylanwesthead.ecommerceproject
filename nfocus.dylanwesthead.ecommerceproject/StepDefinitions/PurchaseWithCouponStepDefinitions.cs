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
        private readonly ScenarioContext _scenarioContext;

        public PurchaseWithCouponStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["driver"];
        }


        /*
         * [When] "I apply a coupon to the cart"
         *    - Applies the 'edgewords' 15% off coupon to the cart 
         */

        [When(@"I apply the coupon '([^']*)' to the cart")]
        public void WhenIApplyTheCouponToTheCart(string coupon)
        {
            // Apply the 15% off coupon
            CartPOM cartPage = new CartPOM(_driver);
            cartPage.enterCoupon(coupon).applyCoupon();

            // Wait for the coupon to be applied (if not already)
            Helper waitForCoupon = new Helper(_driver);
            waitForCoupon.WaitForElement(3, By.ClassName("cart-discount"));
        }


        /*
         * [Then] "15% of the subtotal is deducted"
         *    - Calculates and verifies the coupon savings as well as the grand total.
         */

        [Then(@"'([^']*)'% of the subtotal is deducted")]
        public void ThenOfTheSubtotalIsDeducted(int savingsPercentage)
        {
            CartPOM cartPage = new CartPOM(_driver);

            Decimal basketTotal = cartPage.getSubtotalBeforeCoupon();
            
            Decimal expectedCouponSavings = basketTotal / 100 * savingsPercentage; // Works out expected coupon savings
            Decimal actualCouponSavings = cartPage.getCouponSavings(); // Retrieves actual coupon savings grabbed directly from website
            
            // Works out expected total after coupon savings. E.g. 85% of the total implies a 15% discount
            Decimal expectedPriceToPayBeforeShipping = basketTotal / 100 * (100 - savingsPercentage); // Total / 100 = 1% | 1% * (100 - 85) = 85%
            Decimal actualPriceToPayBeforeShipping = basketTotal - actualCouponSavings;

            // Calculates expected grand total by adding the shipping cost to the discounted total (with coupon)
            Decimal shippingCost = cartPage.getShippingCost();
            Decimal priceToPay = expectedPriceToPayBeforeShipping + shippingCost;

            // Retrieve the grand total (total price to pay after deducting coupon savings and adding shipping cost)
            Decimal grandTotal = cartPage.getGrandTotal();

            //Ensure element is visible before screenshot (chromedriver workaround)
            IJavaScriptExecutor js = _driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView();", _driver.FindElement(By.CssSelector(".cart-collaterals > div")));

            // Takes a screenshot of the cart totals table and attaches to the Test Details (for verification purposes)
            Helper elementScreenshot = new Helper(_driver);
            elementScreenshot.TakeScreenshotElement(cartPage.GetCartTotalsElement(), "all_cart_totals");

            Console.WriteLine($"\nTotal before Coupon: �{basketTotal}\nExpected Total Savings: �{expectedCouponSavings}\nActual Total Savings: �{actualCouponSavings}");
            Console.WriteLine($"\nExpected Total after Coupon: �{expectedPriceToPayBeforeShipping}\nActual Total after Coupon: �{actualPriceToPayBeforeShipping}");
            Console.WriteLine($"Shipping Cost: �{shippingCost}\n\nExpected Grand Total: �{priceToPay}\nActual Grand Total: �{grandTotal}\n");

            // Verify the coupon saves exactly 15% of the original total
            Assert.That(expectedPriceToPayBeforeShipping, Is.EqualTo(basketTotal - actualCouponSavings), $"Not equivalent to {savingsPercentage}% off the original total cost.");

            // Verify the expected grand total is identical to the actual grand total displayed on the webpage
            Assert.That(priceToPay, Is.EqualTo(grandTotal), "Grand total has not been calculated correctly.");
        }

    }
}
