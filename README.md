# PriceNegotiationApp

Business Context:
The application aims to facilitate price negotiations for a product between the customer and an employee of an online store. The customer can submit a price proposal, and the employee has the option to accept or reject the proposal. In case of rejection, the customer can attempt again (up to a maximum of 3 times). If the proposed price exceeds twice the base price of the product, the proposal is automatically rejected.

Implementation Requirements:
• Creation of a Product Catalog module API - Implementing the ability to browse products (list), add a product, and remove a product.
• Creation of a Negotiation module API - Implementing the described process of price negotiation.
• The Web API interface must comply with the REST standard.
• Each application request should be verified; for example, the proposed price must be greater than 0.
• Database integration is optional; you can use an in-memory database or a dictionary in memory.
• It is required to use a version control system such as GIT during project implementation.
• Unit tests.


# Things for further improvement
	- Logs are optional in this project, might as well be opted out in ProductCatalogueService
	- Add update endpoint in the product catalogue service
	- Add date to negotiated prices
	- Add MediatR to user validation logic	
	- Add pswd Hasher


# To do:
	- REWORK ADDPRICEPROPOSAL AND UPDATESTATUS LOGIC!!!
	- user .ReverseMap() in automapper
	- add authentication (GetProductDto add price and that it can be accessed only by someone logged as admin)
	- add unit tests
	