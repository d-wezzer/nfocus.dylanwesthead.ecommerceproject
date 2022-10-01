# Author: Dylan Westhead
# Last Edited: 01/10/2022
#
#   - Feature file running different features at the same time for demonstration purposes.
#	- The feature and scenario in this file do not contribute to the initial tests.

Feature: (demo)Demonstrate Feature Parallelisation
This feature serves to demonstrate running features in paralell, to allow faster test times and most efficient use of available resources.

Background:
	Given I am on the Edgewords eCommerce website
	Given I am logged in

@Coupon
Scenario: (demo)Create purchase order with coupon
	When I add 'Hoodie with Logo' and 'Cap' to my cart
	And I edit product quantity to '12' directly from cart
	And I apply the coupon 'edgewords' to the cart
	Then '15'% of the subtotal is deducted
# Can swap out product parameters. Different items available to use are:
#		- Hoodie with Logo		- Cap		- Beanie
#		- Sunglasses			- Belt		- Long Sleeve Tee
#		Can essentially add any item that is available on the eCommerce site (case sensitive).