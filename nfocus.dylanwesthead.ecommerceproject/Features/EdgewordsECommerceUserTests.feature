# Author: Dylan Westhead
# Last Edited: 06/10/2022
#
#   - Feature file for the two user tests of the project. Scenarios are labelled and tagged accordingly. 
#	- Background scenario used where the Given steps cross over between scenarios.

Feature: edgeWords eCommerce User Tests
We want to test the following two end-to-end user scenarios:
1. Tag[OrderNumber]
   After successfully placing an order, the new order number should be present in the orders history page (containing all orders).
2. Tag[Coupon]
   As a user, when I apply a coupon on the cart screen, the correct percentage of the subtotal price should be deducted.

Background:
	Given I am on the Edgewords eCommerce website
	Given I am logged in

@OrderNumber
Scenario: Create order and verify its presence in all orders
	Given I am a customer with the following details
		| firstName | surname  | address         | town      | postcode | phone       | email                       |
		| Dylan     | Westhead | 123 Sunshine Rd | St Helens | WA9 9AW  | 01234567890 | dylan.westhead@nfocus.co.uk |
	When I add 'Hoodie with Logo' and 'Cap' to my cart
	And I place the order
	Then the order number should appear on my orders page
# Can swap out product parameters. Different items available to use are:
#		- Hoodie with Logo		- Cap		- Beanie
#		- Sunglasses			- Belt		- Long Sleeve Tee
#		Can essentially add any item that is available on the eCommerce site (case sensitive).

@Coupon
Scenario: Create purchase order with coupon
	When I add 'Hoodie with Logo' and 'Cap' to my cart
	And I apply the coupon 'edgewords' to the cart
	Then '15'% of the subtotal is deducted