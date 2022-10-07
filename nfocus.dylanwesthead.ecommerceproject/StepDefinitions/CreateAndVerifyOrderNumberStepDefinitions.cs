/*
 * Author: Dylan Westhead
 * Last Edited: 06/10/2022
 *
 *   - Step definitions used by the order verification scenario.
 *   - Contains all the required information and logic to automate the steps through 
 *     integration of the POMPages. 
 */
using OpenQA.Selenium;
using NUnit.Framework;
using nfocus.dylanwesthead.ecommerceproject.Utils;
using nfocus.dylanwesthead.ecommerceproject.POMPages;

namespace nfocus.dylanwesthead.ecommerceproject.StepDefinitions
{
    [Binding]
    internal class CreateAndVerifyOrderNumberStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        protected private CreateAndVerifyOrderNumberStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["driver"];
        }


        /*
         * [Given] "I am a customer with the following details"
         *   - Creates a customer instance with provided details.
         */
        [Given(@"I am a customer with the following details")]
        protected private void GivenIAmACustomerWithTheFollowingDetails(Table customerDetails)
        {
            // Creates a customer instance with details from feature.
            CheckoutPOM checkoutPage = new(_driver);
            Customer customer = checkoutPage.SetCustomerDetails(customerDetails);

            // Stores the customer object in _scenarioContext for use in the next step.
            _scenarioContext["customer"] = customer;
        }


        /*
         * [When] "I place the order"
         *    - Enters all the revelant billing info and finalises the order.
         */
        [When(@"I place the order")]
        protected private void WhenIPlaceTheOrder()
        {
            // POMPages used in this step (in order).
            NavigationBarPOM navbar = new(_driver);
            CheckoutPOM checkoutPage = new(_driver);

            // Navigate to checkout page.
            navbar.GoToCheckout();

            // Fills in billing form with customer details.
            checkoutPage.PopulateBillingInfo((Customer)_scenarioContext["customer"]);

            // Click radio button to pay by cheque. 
            try 
            {
                checkoutPage.SelectPayByCheque();
            }
            catch
            {
                // Cheque button fragile to stale elements, so retry if failed first time.
                checkoutPage.SelectPayByCheque();
            }

            // Place order and wait for confirmation.
            checkoutPage.PlaceOrder();
            checkoutPage.WaitForOrderConfirmed();
        }


        /*
         * [Then] "the order number should appear on my orders page"
         *    - Verifies that the newly generated order number (from: When I place the order) is present on the page 
         *      containing all orders. 
         */
        [Then(@"the order number should appear on my orders page")]
        protected private void ThenTheOrderNumberShouldAppearOnMyOrdersPage()
        {
            // POM Pages used in this step (in order).
            OrderDetailsPOM orderDetailsPage = new(_driver);
            NavigationBarPOM navBar = new(_driver);
            MyAccountPOM myAccountPage = new(_driver);
            AllOrdersPOM allOrdersPage = new(_driver);

            // Captures the new order number
            string newOrderNumber = orderDetailsPage.GetOrderNumber();

            // Navigates to my account page.
            navBar.GoToMyAccount();

            // Navigates to order history page.
            myAccountPage.GoToOrders();

            // Captures the top order number (most recent) from the order history.
            string topOrderNumber = allOrdersPage.GetTopOrderNumber();

            Console.WriteLine($"\nYour new order number: {newOrderNumber}\nOrders table contains: {topOrderNumber}\n");
            
            // Verify order number from the order confirmation page, is equal to top most-recent order number on order history page.
            Assert.That(topOrderNumber, Does.Contain(newOrderNumber), $"The order with order number {newOrderNumber} was not found on your orders page. Most recent order has order number {topOrderNumber}");

        }
    }
}
