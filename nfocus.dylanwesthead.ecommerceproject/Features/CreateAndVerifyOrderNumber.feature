Feature: CreateAndVerifyOrderNumber
When using a valid coupon on the cart screen, 15% of the subtotal price should be deducted.

Background:
	Given I am on the Edgewords eCommerce website

@OrderNumber
Scenario: Creating an order and verify its presence in all orders
	Given I am logged in
	When I add products to my cart
	And I place the order
	Then the order number should appear on my orders page