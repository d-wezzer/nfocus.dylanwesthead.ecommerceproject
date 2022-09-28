#Feature: edgeWords eCommerce User Tests
#We want to test the following two end-to-end user tests:
#1. When using a valid coupon on the cart screen, 15% of the subtotal price should be deducted.
#2. After successfully placing an order, the new order number should be present in the orders page (containing all orders).
#
#Background:
#	Given I am on the Edgewords eCommerce website
#	Given I am logged in
#
#@OrderNumber
#Scenario: Creating an order and verify its presence in all orders
#	When I add products to my cart
#	And I place the order
#	Then the order number should appear on my orders page
#
#	
#@Coupon
#Scenario: Creating a purchase order with a coupon
#	When I add products to my cart
#	And I apply the coupon 'edgewords' to the cart
#	Then '15'% of the subtotal is deducted


### Not using one feature file as you can't run scenarios in parallel ###