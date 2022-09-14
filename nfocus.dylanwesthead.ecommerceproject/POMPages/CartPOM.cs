using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IWebElement couponCodeField => _driver.FindElement(By.Id("coupon_code"));
        IWebElement applyCouponButton => _driver.FindElement(By.Name("apply_coupon"));
        IWebElement subtotalField => _driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount"));
        IWebElement couponSavingsField => _driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"));
        IWebElement shippingCostField => _driver.FindElement(By.CssSelector("label  bdi"));
        IWebElement grandTotalField => _driver.FindElement(By.CssSelector("strong > .amount.woocommerce-Price-amount > bdi"));
        IWebElement allCartTotals => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div"));

        // Enters a coupon code into the coupon field
        public CartPOM enterCoupon(string coupon)
        {
            couponCodeField.SendKeys(coupon);
            return this;
        }

        // Click the apply coupon button
        public void applyCoupon()
        {
            applyCouponButton.Click();
        }

        // Retrieves the Basket Total before the coupon is applied, and removes the £ symbol
        public decimal getSubtotalBeforeCoupon()
        {
            string basketTotalString = subtotalField.Text.Replace("£", "");
            decimal basketTotal = decimal.Parse(basketTotalString);
            return basketTotal;
        }

        // Retrieves the total coupon savings and removes the £ symbol
        public decimal getCouponSavings()
        {
            string couponSavingsString = couponSavingsField.Text.Replace("£", "");
            decimal couponSavings = decimal.Parse(couponSavingsString);
            return couponSavings;
        }

        // Retrieve the shipping cost and convert to a decimal
        public decimal getShippingCost()
        {
            string shippingCostString = shippingCostField.Text.Replace("£", "");
            decimal shippingCost = decimal.Parse(shippingCostString);
            return shippingCost;
        }

        // Retrieves actual grand total displayed on webpage, then converts to decimal
        public decimal getGrandTotal()
        {
            string grandTotalString = grandTotalField.Text.Replace("£", "");
            decimal grandTotal = decimal.Parse(grandTotalString);
            return grandTotal;
        }

        public IWebElement GetCartTotalsElement()
        {
            return allCartTotals;
        }
    }
}
