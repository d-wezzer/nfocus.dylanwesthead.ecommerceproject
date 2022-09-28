using OpenQA.Selenium;

namespace nfocus.dylanwesthead.ecommerceproject.Utils
{
    /*
     * The Navigation Bar Object-Model
     *   The NavigationBar is an object model containing the necessary locators for use with the tests.
     *   Also contains the relevant functions to interact with the locators.
     */
    internal class NavigationBar
    {

        private IWebDriver _driver;

        public NavigationBar(IWebDriver driver)
        {
            _driver = driver;
        }

        // Locators used for all the elements needed from the navigation bar.
        IWebElement HomeLink => _driver.FindElement(By.LinkText("Home"));
        IWebElement ShopLink => _driver.FindElement(By.LinkText("Shop"));
        IWebElement CartLink => _driver.FindElement(By.LinkText("Cart"));
        IWebElement CheckoutLink => _driver.FindElement(By.LinkText("Checkout"));
        IWebElement MyAccountLink => _driver.FindElement(By.LinkText("My account"));

        // Navigates to the Edgewords eCommerce homepage.
        public void GoToHome()
        {
            HomeLink.Click();
        }

        // Navigates to the Edgewords eCommerce shop.
        public void GoToShop()
        {
            ShopLink.Click();
        }

        // Navigates to the Edgewords eCommerce cart page.
        public void GoToCart()
        {
            CartLink.Click();
        }

        // Navigates to the Edgewords eCommerce checkout page.
        public void GoToCheckout()
        {
            CheckoutLink.Click();
        }

        // Navigates to the Edgewords eCommerce my account page.
        public void GoToMyAccount()
        {
            MyAccountLink.Click();
        }

    }
}
