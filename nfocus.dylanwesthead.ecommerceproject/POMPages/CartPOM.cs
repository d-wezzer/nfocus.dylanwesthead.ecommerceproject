using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class CartPOM
    {
        private IWebDriver _driver;

        public CartPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators
        IWebElement CouponCodeField => _driver.FindElement(By.Id("coupon_code"));
        IWebElement ApplyCouponButton => _driver.FindElement(By.Name("apply_coupon"));
        IWebElement SubtotalField => _driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount"));
        IWebElement CouponSavingsField => _driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"));
        IWebElement ShippingCostField => _driver.FindElement(By.CssSelector("label  bdi"));
        IWebElement GrandTotalField => _driver.FindElement(By.CssSelector("strong > .amount.woocommerce-Price-amount > bdi"));
        IWebElement AllCartTotals => _driver.FindElement(By.CssSelector(".cart-collaterals > div"));

        // Enters a coupon code into the coupon field
        public CartPOM EnterCoupon(string coupon)
        {
            CouponCodeField.SendKeys(coupon);
            return this;
        }

        // Click the apply coupon button
        public void ApplyCoupon()
        {
            ApplyCouponButton.Click();
        }

        // Retrieves the Basket Total before the coupon is applied, and removes the £ symbol
        public decimal GetSubtotalBeforeCoupon()
        {
            string BasketTotalString = SubtotalField.Text.Replace("£", "");
            decimal BasketTotal = decimal.Parse(BasketTotalString);
            return BasketTotal;
        }

        // Retrieves the total coupon savings and removes the £ symbol
        public decimal GetCouponSavings()
        {
            string CouponSavingsString = CouponSavingsField.Text.Replace("£", "");
            decimal CouponSavings = decimal.Parse(CouponSavingsString);
            return CouponSavings;
        }

        // Retrieve the shipping cost and convert to a decimal
        public decimal GetShippingCost()
        {
            string ShippingCostString = ShippingCostField.Text.Replace("£", "");
            decimal ShippingCost = decimal.Parse(ShippingCostString);
            return ShippingCost;
        }

        // Retrieves actual grand total displayed on webpage, then converts to decimal
        public decimal GetGrandTotal()
        {
            string GrandTotalString = GrandTotalField.Text.Replace("£", "");
            decimal GrandTotal = decimal.Parse(GrandTotalString);
            return GrandTotal;
        }

        public IWebElement GetCartTotalsElement()
        {
            return AllCartTotals;
        }
    }
}
