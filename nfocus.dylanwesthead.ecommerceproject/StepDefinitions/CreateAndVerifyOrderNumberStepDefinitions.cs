/*
 * Author: Dylan Westhead
 * Last Edited: 01/10/2022
 *
 *   - The step definitions used by the order verification scenario.
 *   - Contains all the required information and logic to automate the steps through 
 *     integration of the POMPages. 
 */
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using nfocus.dylanwesthead.ecommerceproject.Utils;
using nfocus.dylanwesthead.ecommerceproject.POMPages;

namespace nfocus.dylanwesthead.ecommerceproject.StepDefinitions
{
    [Binding]
    internal class CreateAndVerifyOrderNumberStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly Customer _customer;
        private readonly ScenarioContext _scenarioContext;

        protected private CreateAndVerifyOrderNumberStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["driver"];
            this._customer = (Customer)_scenarioContext["customer"];
        }


        /*
         * [When] "I place the order"
         *    - Enters all the revelant billing info and finalises the order.
         */

        [When(@"I place the order")]
        protected private void WhenIPlaceTheOrder()
        {
            NavigationBar Navbar = new(_driver);
            Navbar.GoToCheckout();

            // Fills in the billing form with the customers details.
            CheckoutPOM CheckoutPage = new(_driver);
            CheckoutPage.PopulateBillingInfo(_customer.First, _customer.Surname, _customer.Address, _customer.Town,
                _customer.Postcode, _customer.Phone, _customer.Email);

            // Click the radio button to pay by cheque (NOT cash).
            try // Cheque radio button is extremely fragile to stale elements.
            {
                CheckoutPage.SelectPayByCheque();
            }
            catch
            {
                CheckoutPage.SelectPayByCheque();
            }

            // Place the order and wait for confirmation.
            CheckoutPage.PlaceOrder();
            CheckoutPage.WaitForOrderConfirmed();
        }


        /*
         * [Then] "the order number should appear on my orders page"
         *    - Verifies that the newly generated order number (from: When I place the order) is present on the page 
         *      containing all orders. 
         */

        [Then(@"the order number should appear on my orders page")]
        protected private void ThenTheOrderNumberShouldAppearOnMyOrdersPage()
        {
            // Retrieves the unique order number.
            OrderDetailsPOM OrderDetailsPage = new(_driver);
            string NewOrderNumber = OrderDetailsPage.GetOrderNumber();

            // Navigate to the my account page.
            NavigationBar NavBar = new(_driver);
            NavBar.GoToMyAccount();

            // Go to the orders page to see all successfully placed orders.
            MyAccountPOM MyAccountPage = new(_driver);
            MyAccountPage.GoToOrders();

            // Collect the top most order number, this should be the most recent order.
            AllOrdersPOM AllOrdersPage = new(_driver);
            string TopOrderNumber = AllOrdersPage.GetTopOrderNumber();

            Console.WriteLine($"\nYour new order number: {NewOrderNumber}\nOrders table contains: {TopOrderNumber}\n");
            Assert.That(TopOrderNumber, Does.Contain(NewOrderNumber), $"The order with order number {NewOrderNumber} was not found on your orders page");

        }
    }
}
