# Author: Dylan Westhead
# Last Edited: 29/09/2022
#
#   - Feature file running different features at the same time for demonstration purposes.
#	- The feature and scenario in this file do not contribute to the initial tests.

Feature: Demonstrate Feature Parallelisation
This feature serves to demonstrate running features in paralell, to allow faster test times and most efficient use of available resources.

Background:
	Given I am on the Edgewords eCommerce website
	Given I am logged in

@Coupon
Scenario: Creating a purchase order with a coupon
	When I add products to my cart
	And I edit product quantity to '123' directly from cart
	And I apply the coupon 'edgewords' to the cart
	Then '15'% of the subtotal is deducted