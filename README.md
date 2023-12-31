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


# Things for future improvements

	- Add update endpoint in the ProductCatalogueService
	- Add Authorization
	- Add MediatR to user validation logic	
	- Make PriceNegotiationMappingProfile a bit more tidy :)
	- Logs are optional in this project, might as well be opted out in Services and Program.cs


# Additional notes

	- I used the MediatR library only in the PriceNegotiation API, because I wanted to show how I would use it in a real world scenario.
		- MediatR pipeline components: 
			- PriceNegotiationController
			- PriceNegotiationService
			- Queries/Commands
			- Handlers
   	- Unit tests are located in the .zip file in the main directory

	
