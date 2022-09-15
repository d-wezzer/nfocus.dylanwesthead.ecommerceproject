Feature: PurchaseWithCoupon
After successfully placing an order, the new order number should be present in the orders page (containing all orders).

Background: 
	Given I am on the Edgewords eCommerce website

@Coupon
Scenario: Creating a purchase order with a 15% off coupon
	Given I am logged in
	When I add products to my cart
	And I apply the coupon 'edgewords' to the cart
	Then '15'% of the subtotal is deducted
