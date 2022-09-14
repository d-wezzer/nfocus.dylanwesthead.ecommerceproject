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

        [When(@"I apply a coupon to the cart")]
        public void WhenIApplyACouponToTheCart()
        {
            // Apply the 15% off coupon
            CartPOM cartPage = new CartPOM(_driver);
            cartPage.enterCoupon("edgewords").applyCoupon();

            // If the coupon has already been applied then this waitForCoupon doesn't wait at all
            // Wait for the coupon to be applied
            Helper waitForCoupon = new Helper(_driver);
            waitForCoupon.WaitForElement(3, By.ClassName("cart-discount"));
        }


        /*
         * [Then] "15% of the subtotal is deducted"
         *    - Calculates and verifies the coupon savings as well as the grand total.
         */

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
