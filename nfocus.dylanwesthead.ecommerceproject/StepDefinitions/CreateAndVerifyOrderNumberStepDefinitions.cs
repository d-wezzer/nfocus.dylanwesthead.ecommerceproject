using nfocus.dylanwesthead.ecommerceproject.Utils;
using OpenQA.Selenium;
using nfocus.dylanwesthead.ecommerceproject.POMPages;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace nfocus.dylanwesthead.ecommerceproject.StepDefinitions
{
    [Binding]
    public class CreateAndVerifyOrderNumberStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly Customer _customer;
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
            //Helper elemWaiter = new Helper(_driver);
            //elemWaiter.WaitForElement(2, By.LinkText("Proceed to checkout"));

            NavigationBar Navbar = new NavigationBar(_driver);
            Navbar.GoToCheckout();

            // Fills in the billing form with the customers details
            CheckoutPOM CheckoutPage = new CheckoutPOM(_driver);
            CheckoutPage.populateBillingInfo(_customer.First, _customer.Surname, _customer.Address, _customer.Town,
                _customer.Postcode, _customer.Phone, _customer.Email);

            // Click the radio button to pay by cheque (NOT cash) and then place order
            CheckoutPage.selectPayByCheque();
            CheckoutPage.placeOrder();

            // Wait for the order details page to be displayed. Creating an order takes a few seconds
            WebDriverWait WaitForOrderConfirmation = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            WaitForOrderConfirmation.Until(drv => drv.FindElement(By.ClassName("entry-title")).Text.Contains("Order received"));
        }


        /*
         * [Then] "the order number should appear on my orders page"
         *    - Verifies that the newly generated order number (from: When I place the order) is present on the page 
         *      containing all orders. 
         */

        [Then(@"the order number should appear on my orders page")]
        public void ThenTheOrderNumberShouldAppearOnMyOrdersPage()
        {
            // Helper to wait for 'problematic' elements
            Helper MyHelper = new(_driver);

            // Retrieves the unique order number
            OrderDetailsPOM OrderDetailsPage = new OrderDetailsPOM(_driver);
            string NewOrderNumber = OrderDetailsPage.getOrderNumber();

            // Takes a screenshot of the new order number in order details and attaches to the Test Details (for verification purposes)
            Helper ElementScreenshot = new(_driver);
            ElementScreenshot.TakeScreenshotElement(OrderDetailsPage.GetOrderDetails(), "new_order_number");

            // Wait for my account link to be displayed on navigation menu
            MyHelper.WaitForElement(2, By.LinkText("My account"));

            NavigationBar NavBar = new(_driver);
            NavBar.GoToMyAccount();

            // Wait for the orders link to be displayed in the side menu
            MyHelper.WaitForElement(2, By.LinkText("Orders"));

            // Go to the orders page to see all successfully placed orders
            MyAccountPOM MyAccountPage = new(_driver);
            MyAccountPage.goToOrders();

            // Takes a screenshot of the entire orders table in my account and attaches to the Test Details (for verification purposes)
            AllOrdersPOM AllOrdersPage = new(_driver);
            ElementScreenshot.TakeScreenshotElement(AllOrdersPage.getAllOrderNumbersTable(), "top_order_from_all_orders");

            // Collect the top most order number, this should be the most recent order
            string TopOrderNumber = AllOrdersPage.getTopOrderNumber();

            Console.WriteLine($"\nYour new order number: #{NewOrderNumber}\nOrders table contains: {TopOrderNumber}\n");
            Assert.That(TopOrderNumber, Does.Contain(NewOrderNumber), $"The order with order number {NewOrderNumber} was not found on your orders page");

        }
    }
}
