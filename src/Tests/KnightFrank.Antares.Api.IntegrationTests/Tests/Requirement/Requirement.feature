﻿Feature: Requirement 

@ResidentialSalesRequirements 
Scenario: Create residential sales requirement
	Given User gets GB address form for Requirement and country details
		And User creates contacts in database with following data 
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
	When User sets locations details for the requirement with max length fields
		And User creates following requirement using api
 			| MinPrice | MaxPrice | MinBedrooms | MaxBedrooms | MinReceptionRooms | MaxReceptionRooms | MinBathrooms | MaxBathrooms | MinParkingSpaces | MaxParkingSpaces | MinArea | MaxArea | MinLandArea | MaxLandArea | Description |
 			| 1000000  | 4000000  | 1           | 5           | 0                 | 2                 | 1            | 3            | 1                | 2                | 1200    | 2000    | 10000       | 20000       | max         |
	Then User should get OK http status code
		And Requirement should be the same as added

@ResidentialSalesRequirements 
Scenario: Create residential sales requirement with mandatory fields
	Given User gets GB address form for Requirement and country details
		And User creates contacts in database with following data 
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
	When User creates requirement with mandatory fields using api
	Then User should get OK http status code
		And Requirement should be the same as added

@ResidentialSalesRequirements 
Scenario: Get residential sales requirement
	Given User gets GB address form for Requirement and country details
		And User creates contacts in database with following data 
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
	When User sets locations details for the requirement
		| Postcode | City   | Line2   |
		| 1234     | London | Big Ben |
		And User creates following requirement in database
 			| MinPrice | MaxPrice | MinBedrooms | MaxBedrooms | MinReceptionRooms | MaxReceptionRooms | MinBathrooms | MaxBathrooms | MinParkingSpaces | MaxParkingSpaces | MinArea | MaxArea | MinLandArea | MaxLandArea | Description |
 			| 1000000  | 4000000  | 1           | 5           | 0                 | 2                 | 1            | 3            | 1                | 2                | 1200    | 2000    | 10000       | 20000       | Description |
		And User creates notes for requirement in database
			| description     |
			| Description foo |
			| Description bar |
		And User retrieves requirement for latest id
	Then User should get OK http status code
		And Requirement should be the same as added
		And Notes should be the same as added
		
@ResidentialSalesRequirements 
Scenario Outline: Create residential sales requirement without data
	Given User gets GB address form for Property and country details
		And User creates contacts in database with following data 
			| FirstName | Surname | Title  |
			| Tomasz    | Bien    | Mister |
	When User sets locations details for the requirement
		| Postcode | City   | Line2   |
		| 1234     | London | Big Ben |
		And User creates following requirement without <data> using api
			| MinPrice | MaxPrice | MinBedrooms | MaxBedrooms | MinReceptionRooms | MaxReceptionRooms | MinBathrooms | MaxBathrooms | MinParkingSpaces | MaxParkingSpaces | MinArea | MaxArea | MinLandArea | MaxLandArea | Description            |
			| 1000000  | 4000000  | 1           | 5           | 0                 | 2                 | 1            | 3            | 1                | 2                | 1200    | 2000    | 10000       | 20000       | RequirementDescription |
	Then User should get BadRequest http status code

	Examples: 
	| data         |
	| contact      |
	| country      |
	| address form |

@ResidentialSalesRequirements
Scenario: Create residential sales requirement with invalid contact
	Given User gets GB address form for Requirement and country details
	When User sets locations details for the requirement
		| Postcode | City   | Line2   |
		| 1234     | London | Big Ben |
		And User creates following requirement with invalid contact using api			
			| MinPrice | MaxPrice | MinBedrooms | MaxBedrooms | MinReceptionRooms | MaxReceptionRooms | MinBathrooms | MaxBathrooms | MinParkingSpaces | MaxParkingSpaces | MinArea | MaxArea | MinLandArea | MaxLandArea | Description            |
			| 1000000  | 4000000  | 1           | 5           | 0                 | 2                 | 1            | 3            | 1                | 2                | 1200    | 2000    | 10000       | 20000       | RequirementDescription |
	Then User should get BadRequest http status code

@Requirements
Scenario Outline: Get residential sales requirement with invalid data		
	When User retrieves requirement for <id> id
	Then User should get <statusCode> http status code

	Examples: 
	| id                                   | statusCode |
	| 00000000-0000-0000-0000-000000000000 | NotFound   |
	| A                                    | BadRequest |
