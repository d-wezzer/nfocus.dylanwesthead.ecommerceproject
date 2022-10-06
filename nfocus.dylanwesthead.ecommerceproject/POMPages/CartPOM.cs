/*
 * Author: Dylan Westhead
 * Last Edited: 07/10/2022
 *
 *   - The Page Object Model for the cart page of the Edgewords eCommerce demo site.
 */
using nfocus.dylanwesthead.ecommerceproject.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class CartPOM
    {
        private readonly IWebDriver _driver;

        internal CartPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        /* Locators and elements used to calculate and gather the cart totals on the cart page. */
        // The => means each time the variable is used, find element is called.
        private By _cartDiscountLocator => By.ClassName("cart-discount");
        private IWebElement _couponCodeField => _driver.FindElement(By.Id("coupon_code"));
        private IWebElement _applyCouponButton => _driver.FindElement(By.Name("apply_coupon"));
        private IWebElement _subtotalField => _driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount"));
        private IWebElement _couponSavingsField => _driver.FindElement(By.CssSelector(".coupon-edgewords > td > .amount"));
        private IWebElement _shippingCostField => _driver.FindElement(By.CssSelector("label bdi"));
        private IWebElement _grandTotalField => _driver.FindElement(By.CssSelector("strong > .amount"));
        private IWebElement _allCartTotals => _driver.FindElement(By.ClassName("cart_totals"));
        private IWebElement _firstQuantityField => _driver.FindElement(By.ClassName("input-text"));
        private IWebElement _updateCartButton => _driver.FindElement(By.Name("update_cart"));


        /*
         * EnterCoupon(string)
         *   - Enters a coupon code into the coupon field. 
         *   - Try/catch in place to retry the same action twice.
         */
        internal void EnterCoupon(string coupon)
        {   // Try catch again to help prevent stale element exceptions
            try
            {
                _couponCodeField.SendKeys(coupon);
            }
            catch
            {
                _couponCodeField.SendKeys(coupon);
            }
        }


        /*
         * ApplyCoupon()
         *   - Clicks apply coupon button and waits for it to be applied.
         *   - Uses an explicit wait to wait 5 seconds for cart discount field to be displayed.
         */
        internal void ApplyCoupon()
        {
            _applyCouponButton.Click();

            // Wait for the coupon to be applied (if not already) by searching for a cart discount field
            Helper waitForCoupon = new Helper(_driver);
            waitForCoupon.WaitForElement(5, _cartDiscountLocator);
        }


        /*
         * GetSubtotalBeforeCoupon()
         *   - Retrieves the subtotal of order before coupon is applied.
         *   - Subtotal is captured directly from the webpage.
         *   - Removes £ symbol and parses to decimal for reliable currency calculations.
         */
        internal decimal GetSubtotalBeforeCoupon()
        {
            string subtotalString = _subtotalField.Text.Replace("£", "");
            decimal subtotal = decimal.Parse(subtotalString);
            return subtotal;
        }


        /*
         * GetCouponSavings()
         *   - Retrieves the total coupon savings of the order.
         *   - Coupon savings figure is captured directly from the webpage.
         *   - Removes £ symbol and parses to decimal for reliable currency calulcations.
         */
        internal decimal GetCouponSavings()
        {
            string couponSavingsString = _couponSavingsField.Text.Replace("£", "");
            decimal couponSavings = decimal.Parse(couponSavingsString);
            return couponSavings;
        }


        /*
         * GetShippingCost()
         *   - Retrieves the shipping cost of the order.
         *   - Shipping cost is captured directly from the webpage.
         *   - Removes £ symbol and parses to decimal for reliable currency calculations.
         */
        internal decimal GetShippingCost()
        {
            string shippingCostString = _shippingCostField.Text.Replace("£", "");
            decimal shippingCost = decimal.Parse(shippingCostString);
            return shippingCost;
        }


        /*
         * GetGrandTotal()
         *   - Retrieves actual grand total of the order.
         *   - Grand total is captured directly from the webpage.
         *   - Removes £ symbol and parses to decimal for reliable currency calculations.
         */
        internal decimal GetGrandTotal()
        {
            string grandTotalString = _grandTotalField.Text.Replace("£", "");
            decimal grandTotal = decimal.Parse(grandTotalString);
            return grandTotal;
        }


        /*
         * ChangeProductQuantity(string)
         *   - Changes the quantity field of the first item in the cart to the specified quantity.
         */
        internal void ChangeProductQuantity(string quantity)
        {
            _firstQuantityField.Clear();
            _firstQuantityField.SendKeys(quantity);
        }


        /*
         * CalculateExpectedAndActualTotals(int)
         *   - Calculates all the expected totals whilst also capturing webpage displayed totals.
         *   - Maps all the totals to a dictionary for use in the step definitions.
         */
        internal Dictionary<string, decimal> CalculateExpectedAndActualTotals(int savingsPercentage)
        {
            // Captures subtotal before coupon is applied, and adds to totals dictionary.
            Decimal subtotalBeforeCoupon = GetSubtotalBeforeCoupon();

            // Calculates expected coupon savings - what it should be.
            Decimal expectedSavings = subtotalBeforeCoupon / 100 * savingsPercentage;
            // Captures the actual savings directly from the webpage - what it is.
            Decimal actualSavings = GetCouponSavings();

            // Calculates expected total after applying savings but before applying shipping costs.
            // E.g. 85% of the total implies a 15% discount. Subtotal / 100 = 1% | 1% * (100 - 15) = 85%
            Decimal expectedTotalBeforeShipping = subtotalBeforeCoupon / 100 * (100 - savingsPercentage);

            // Calculates actual total before shipping using totals captured directly from webpage.
            Decimal actualTotalBeforeShipping = subtotalBeforeCoupon - actualSavings;

            // Calculates the actual savings percentage that was applied.
            Decimal actualSavingsPercentage = actualSavings / subtotalBeforeCoupon * 100;

            // Captures the shipping cost directly from the webpage.
            Decimal shippingCost = GetShippingCost();

            // Calculates expected grand total by adding shipping cost to discounted total.
            Decimal expectedGrandTotal = expectedTotalBeforeShipping + shippingCost;

            // Captures actual grand total directly from webpage.
            Decimal actualGrandTotal = GetGrandTotal();

            // Initialise a dictionary containing all the cart totals with reasonably named keys (identifiers).
            Dictionary<string, decimal> cartTotals = new Dictionary<string, decimal>()
            {
                { nameof(subtotalBeforeCoupon), subtotalBeforeCoupon },
                { nameof(expectedSavings), expectedSavings },
                { nameof(actualSavings), actualSavings },
                { nameof(expectedTotalBeforeShipping), expectedTotalBeforeShipping },
                { nameof(actualTotalBeforeShipping), actualTotalBeforeShipping },
                { nameof(shippingCost), shippingCost },
                { nameof(expectedGrandTotal), expectedGrandTotal },
                { nameof(actualGrandTotal), actualGrandTotal },
                { nameof(actualSavingsPercentage), actualSavingsPercentage }
            };
            return cartTotals;
        }


        /*
         * UpdateCart()
         *   - Updates the cart by clicking the update button.
         *   - Explicit wait of 3 seconds to allow button to be enabled after applying coupon.
         */
        internal void UpdateCart()
        {
            try
            {
                // Waits 3 seconds for the update button to be enabled.
                WebDriverWait waitForUpdateButtonClickable = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
                waitForUpdateButtonClickable.Until(drv => _updateCartButton.Enabled);
                _updateCartButton.Click();
            }
            catch
            {
                Console.WriteLine("The update button wasn't clickable");
            }

        }


        /*
         * ScreenshotAllTotals()
         *   - Takes screenshot of the calculated cart totals for the order.
         *   - Attaches to the test context for verification purposes.
         */
        internal void ScreenshotAllTotals()
        {
            // Takes screenshot of the cart totals table.
            Helper ssHelper = new(_driver);
            ssHelper.TakeScreenshotElement(_allCartTotals, "all_cart_totals");
        }
    }
}
