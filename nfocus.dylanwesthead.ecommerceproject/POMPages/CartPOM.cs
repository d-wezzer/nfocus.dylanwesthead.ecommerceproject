/*
 * Author: Dylan Westhead
 * Last Edited: 01/10/2022
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

        // Locators to the required elements on the cart page. The => means each time the variable is used, find element is called.
        private readonly By _CartDiscountLocator = By.ClassName("cart-discount");
        private IWebElement CouponCodeField => _driver.FindElement(By.Id("coupon_code"));
        private IWebElement ApplyCouponButton => _driver.FindElement(By.Name("apply_coupon"));
        private IWebElement SubtotalField => _driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount"));
        private IWebElement CouponSavingsField => _driver.FindElement(By.CssSelector(".coupon-edgewords > td > .amount"));
        private IWebElement ShippingCostField => _driver.FindElement(By.CssSelector("label bdi"));
        private IWebElement GrandTotalField => _driver.FindElement(By.CssSelector("strong > .amount"));
        private IWebElement AllCartTotals => _driver.FindElement(By.ClassName("cart_totals"));
        private IWebElement FirstQuantityField => _driver.FindElement(By.ClassName("input-text"));
        private IWebElement UpdateCartButton => _driver.FindElement(By.Name("update_cart"));

        // Enters a coupon code into the coupon field. Returns a CartPOM object.
        internal CartPOM EnterCoupon(string coupon)
        {   // Try catch again to help prevent stale element exceptions
            try
            {
                CouponCodeField.SendKeys(coupon);
            }
            catch
            {
                CouponCodeField.SendKeys(coupon);
            }

            return this;
        }

        // Clicks the apply coupon button and waits for it to be applied.
        internal void ApplyCoupon()
        {
            ApplyCouponButton.Click();

            // Wait for the coupon to be applied (if not already)
            Helper WaitForCoupon = new Helper(_driver);
            WaitForCoupon.WaitForElement(3, _CartDiscountLocator);
        }

        // Retrieves the Basket Total before the coupon is applied, and removes the £ symbol.
        internal decimal GetSubtotalBeforeCoupon()
        {
            string BasketTotalString = SubtotalField.Text.Replace("£", "");
            decimal BasketTotal = decimal.Parse(BasketTotalString);
            return BasketTotal;
        }

        // Retrieves the total coupon savings and removes the £ symbol.
        internal decimal GetCouponSavings()
        {
            string CouponSavingsString = CouponSavingsField.Text.Replace("£", "");
            decimal CouponSavings = decimal.Parse(CouponSavingsString);
            return CouponSavings;
        }

        // Retrieve the shipping cost and convert to a decimal.
        internal decimal GetShippingCost()
        {
            string ShippingCostString = ShippingCostField.Text.Replace("£", "");
            decimal ShippingCost = decimal.Parse(ShippingCostString);
            return ShippingCost;
        }

        // Retrieves actual grand total displayed on webpage, then converts to decimal.
        internal decimal GetGrandTotal()
        {
            string GrandTotalString = GrandTotalField.Text.Replace("£", "");
            decimal GrandTotal = decimal.Parse(GrandTotalString);
            return GrandTotal;
        }

        // Retrieves the entire totals table element.
        internal IWebElement GetCartTotalsElement()
        {
            return AllCartTotals;
        }

        /* This only works for one product, need to edit to make more robust to handle any product */
        // Changes the quantity of a product directly from cart
        internal void ChangeProductQuantity(string quantity)
        {
            FirstQuantityField.Clear();
            FirstQuantityField.SendKeys(quantity);
        }

        // Clicks the button to update the cart
        internal void UpdateCart()
        {
            try
            {
                // Waits 3 seconds for the update button to be enabled
                WebDriverWait WaitForUpdateButtonClickable = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
                WaitForUpdateButtonClickable.Until(drv => UpdateCartButton.Enabled);
                UpdateCartButton.Click();
            }
            catch
            {
                Console.WriteLine("The update button wasn't clickable");
            }

        }

        internal void TakeScreenshotTotals()
        {
            Helper ssHelper = new(_driver);

            if (Environment.GetEnvironmentVariable("STEPSCREENSHOT") == "true")
            {
                // Takes a screenshot of the cart totals table and attaches to the Test Details (for verification purposes).
                ssHelper.TakeScreenshotElement(GetCartTotalsElement(), "all_cart_totals");
            }
        }
    }
}
