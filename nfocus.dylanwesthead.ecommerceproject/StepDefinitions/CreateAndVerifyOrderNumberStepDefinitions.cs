using nfocus.dylanwesthead.ecommerceproject.Utils;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using nfocus.dylanwesthead.ecommerceproject.POMPages;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace nfocus.dylanwesthead.ecommerceproject.StepDefinitions
{
    [Binding]
    public class CreateAndVerifyOrderNumberStepDefinitions
    {
        private IWebDriver _driver;
        private Customer _customer;
        private readonly ScenarioContext _scenarioContext;

        public CreateAndVerifyOrderNumberStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["driver"];
            this._customer = (Customer)_scenarioContext["customer"];
        }


        /*
         * [When] "I place the order"
         *    - Enters all the revelant billing info and finalises the order 
         */

        [When(@"I place the order")]
        public void WhenIPlaceTheOrder()
        {
            Helper elemWaiter = new Helper(_driver);
            elemWaiter.WaitForElement(2, By.LinkText("Proceed to checkout"));

            NavPOM_ex navBar = new NavPOM_ex(_driver);
            navBar.goToCheckout();

            // Fills in the billing form with the customers details
            CheckoutPOM checkoutPage = new CheckoutPOM(_driver);
            checkoutPage.populateBillingInfo(_customer.first, _customer.surname, _customer.address, _customer.town,
                _customer.postcode, _customer.phone, _customer.email);

            // Click the radio button to pay by cheque (NOT cash) and then place order
            checkoutPage.selectPayByCheque();
            checkoutPage.placeOrder();

            // Wait for the order details page to be displayed. Creating an order takes a few seconds
            WebDriverWait waitForOrderConfirmed = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            waitForOrderConfirmed.Until(drv => drv.FindElement(By.ClassName("entry-title")).Text.Contains("Order received"));
        }


        /*
         * [Then] "the order number should appear on my orders page"
         *    - Verifies that the newly generated order number (from: When I place the order) is present on the page 
         *      containing all orders. 
         */

        [Then(@"the order number should appear on my orders page")]
        public void ThenTheOrderNumberShouldAppearOnMyOrdersPage()
        {
            // Retrieves the unique order number
            OrderDetailsPOM orderDetailsPage = new OrderDetailsPOM(_driver);
            string newOrderNumber = orderDetailsPage.getOrderNumber();
            Console.WriteLine($"\nYour new order number: #{newOrderNumber}");

            NavPOM_ex navBar = new NavPOM_ex(_driver);
            navBar.goToMyAccount();

            // Go to the orders page to see all successfully placed orders
            MyAccountPOM myAccountPage = new MyAccountPOM(_driver);
            myAccountPage.goToOrders();

            IWebElement ordersTable = _driver.FindElement(By.CssSelector(".woocommerce-orders-table__cell-order-number"));
            Console.WriteLine($"Orders table contains: {ordersTable.Text}\n");
            Assert.That(ordersTable.Text, Does.Contain(newOrderNumber), $"The order with order number {newOrderNumber} was not found on your orders page");

        }
    }
}
