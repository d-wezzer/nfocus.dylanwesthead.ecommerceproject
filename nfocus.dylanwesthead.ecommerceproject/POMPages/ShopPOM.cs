using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.POMPages
{
    internal class ShopPOM
    {
        private IWebDriver _driver;
        public ShopPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Locators for items in shop
        IWebElement HoodieWithLogo => _driver.FindElement(By.XPath("//main[@id='main']/ul//a[@href='?add-to-cart=31']"));
        IWebElement BaseballCap => _driver.FindElement(By.XPath("//main[@id='main']/ul//a[@href='?add-to-cart=29']"));

        public void addItemsToCart()
        {
            HoodieWithLogo.Click();
            BaseballCap.Click();
        }
    }
}
