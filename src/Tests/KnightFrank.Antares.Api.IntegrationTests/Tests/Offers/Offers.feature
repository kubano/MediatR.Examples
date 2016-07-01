﻿Feature: Offers

@Offers
Scenario Outline: Create offer
	Given Property exists in database
		| PropertyType | Division    |
		| House        | Residential |
		And Activity exists in database
			| ActivityStatus | ActivityType  |
			| PreAppraisal   | Freehold Sale |
		And Contacts exists in database
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
		And Requirement of type <requirementType> exists in database
	When User creates New offer using api
	Then User should get OK http status code
		And Offer details should be the same as already added

	Examples:
	| requirementType    |
	| ResidentialSale    |
	| ResidentialLetting |

@Offers
Scenario Outline: Create offer with mandatory fields
	Given Property exists in database
		| PropertyType | Division    |
		| House        | Residential |
		And Activity exists in database
			| ActivityStatus | ActivityType  |
			| PreAppraisal   | Freehold Sale |
		And Contacts exists in database 
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
		And Requirement of type <requirementType> exists in database
	When User creates Accepted offer with mandatory fields using api
	Then User should get OK http status code
		And Offer details should be the same as already added

	Examples:
	| requirementType    |
	| ResidentialSale    |
	| ResidentialLetting |

@Offers
Scenario Outline: Create residential sales offer with invalid data
	Given Property exists in database
		| PropertyType | Division    |
		| House        | Residential |
		And Activity exists in database
			| ActivityStatus | ActivityType  |
			| PreAppraisal   | Freehold Sale |
		And Contacts exists in database
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
		And Requirement of type ResidentialSale exists in database
	When User creates offer with invalid <data> using api
	Then User should get BadRequest http status code

	Examples: 
	| data        |
	| requirement |
	| activity    |
	| status      |

@Offers
Scenario Outline: Get offer
	Given Property exists in database
		| PropertyType | Division    |
		| House        | Residential |
		And Activity exists in database
			| ActivityStatus | ActivityType  |
			| PreAppraisal   | Freehold Sale |
		And Contacts exists in database
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
		And Requirement of type <requirementType> exists in database
		And Offer with New status exists in database
	When User gets offer for latest id
	Then User should get OK http status code
		And Offer details should be the same as already added

	Examples:
	| requirementType    |
	| ResidentialSale    |
	| ResidentialLetting |

@Offers
Scenario Outline: Get Accepted offer
	Given Contacts exists in database
		| FirstName | Surname | Title |
		| Jon       | Lajoie  | Dude  |
		And Company exists in database
		And Property exists in database
			| PropertyType | Division    |
			| House        | Residential |
		And Activity exists in database
			| ActivityStatus | ActivityType  |
			| PreAppraisal   | Freehold Sale |
		And Contacts exists in database
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
		And Requirement of type <requirementType> exists in database
		And Offer with Accepted status exists in database
	When User gets offer for latest id
	Then User should get OK http status code
		And Offer details should be the same as already added

	Examples:
	| requirementType    |
	| ResidentialSale    |
	| ResidentialLetting |

@Offers
Scenario: Get residential sales offer with invalid data
	When User gets offer for invalid id
	Then User should get NotFound http status code

@Offers
Scenario Outline: Update offer
	Given Property exists in database
		| PropertyType | Division    |
		| House        | Residential |
		And Activity exists in database
			| ActivityStatus | ActivityType  |
			| PreAppraisal   | Freehold Sale |
		And Contacts exists in database
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
		And Requirement of type <requirementType> exists in database
		And Offer with New status exists in database
	When User updates offer with New status
	Then User should get OK http status code
		And Offer details should be updated

	Examples:
	| requirementType    |
	| ResidentialSale    |
	| ResidentialLetting |

@Offers
Scenario Outline: Update accepted offer
	Given Contacts exists in database
		| FirstName | Surname | Title |
		| Jon       | Lajoie  | Dude  |
		And Company exists in database
		And Property exists in database
			| PropertyType | Division    |
			| House        | Residential |
		And Activity exists in database
			| ActivityStatus | ActivityType  |
			| PreAppraisal   | Freehold Sale |
		And Contacts exists in database
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
		And Requirement of type <requirementType> exists in database
		And Offer with <offerStatus> status exists in database
		And Contacts exists in database
			| FirstName | Surname | Title |
			| Adam      | Lajoie  | Sir   |
		And Company exists in database
	When User updates offer with <newOfferStatus> status
	Then User should get OK http status code
		And Offer details should be updated

	Examples:
	| offerStatus | newOfferStatus | requirementType    |
	| New         | Accepted       | ResidentialSale    |
	| Accepted    | Accepted       | ResidentialSale    |
	| Accepted    | New            | ResidentialSale    |
	| New         | Accepted       | ResidentialLetting |
	| Accepted    | Accepted       | ResidentialLetting |
	| Accepted    | New            | ResidentialLetting |

@Offers
Scenario Outline: Update residential sales offer with invalid data
	Given Property exists in database
		| PropertyType | Division    |
		| House        | Residential |
		And Activity exists in database
			| ActivityStatus | ActivityType  |
			| PreAppraisal   | Freehold Sale |
		And Contacts exists in database
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
		And Requirement of type ResidentialSale exists in database
		And Offer with New status exists in database
	When User updates New offer with invalid <data> data
	Then User should get BadRequest http status code

	Examples: 
	| data   |
	| status |
	| offer  |

@Offers
Scenario Outline: Update accepted residential sales offer with invalid data
	Given Contacts exists in database
		| FirstName | Surname | Title |
		| Jon       | Lajoie  | Dude  |
		And Company exists in database
		And Property exists in database
			| PropertyType | Division    |
			| House        | Residential |
		And Activity exists in database
			| ActivityStatus | ActivityType  |
			| PreAppraisal   | Freehold Sale |
		And Contacts exists in database
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
		And Requirement of type ResidentialSale exists in database
		And Offer with Accepted status exists in database
	When User updates Accepted offer with invalid <data> data
	Then User should get BadRequest http status code

	Examples: 
	| data                      |
	| broker                    |
	| brokerCompany             |
	| lender                    |
	| lenderCompany             |
	| surveyor                  |
	| surveyorCompany           |
	| additionalSurveyor        |
	| additionalSurveyorCompany |
	| mortgageStatus            |
	| mortgageSurveyStatus      |
	| additionalSurveyStatus    |
	| searchStatus              |
	| enquiries                 |
