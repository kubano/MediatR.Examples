﻿Feature: Offer UI tests

@Requirement
@Offer
Scenario: Create residential sales offer on requirement
	Given Contacts are created in database
		| Title | FirstName | Surname  |
		| Sir   | John      | Soane    |
		| Sir   | Robert    | McAlpine |
		| Sir   | Edward    | Graham   |
		And Property with Residential division and House type is defined
		And Property attributes details are defined
			| MinBedrooms | MaxBedrooms | MinReceptions | MaxReceptions | MinBathrooms | MaxBathrooms | MinArea | MaxArea | MinLandArea | MaxLandArea | MinCarParkingSpaces | MaxCarParkingSpaces |
			| 10          | 12          | 2             | 4             | 8            | 10           | 20000   | 25000   | 30000       | 50000       | 10                  | 20                  |
		And Property characteristics are defined
		And Property in GB is created in database
			| PropertyNumber | PropertyName       | Line2                | Postcode | City   | County        |
			| 13             | John Soane’s house | Lincoln’s Inn Fields | WC2A 3BP | London | London county |
		And Property ownership is defined
			| PurchaseDate | BuyPrice  |
			| 10-01-1998   | 100000000 |
		And Property Freehold Sale activity is defined
		And Requirement for GB is created in database
			| MinPrice | MaxPrice  | MinBedrooms | MaxBedrooms | MinReceptionRooms | MaxReceptionRooms | MinBathrooms | MaxBathrooms | MinParkingSpaces | MaxParkingSpaces | MinArea | MaxArea | MinLandArea | MaxLandArea | Description |
			| 90000000 | 120000000 | 5           | 15          | 2                 | 4                 | 2            | 10           | 15               | 25               | 20000   | 25000   | 30000       | 50000       | Note        |
		And Viewing for requirement is defined
	When User navigates to view requirement page with id
		And User clicks make an offer button for 1 activity on view requirement page
	Then Activity details on view requirement page are same as the following
		| Details                                     |
		| John Soane’s house, 13 Lincoln’s Inn Fields |
	When User fills in offer details on view requirement page
		| Status | Offer  | SpecialConditions |
		| New    | 100000 | Text              |
		And User clicks save offer button on view requirement page
	Then New offer should be created and displayed on view requirement page
		And Offer details on 1 position on view requirement page are same as the following
			| Details                                     | Offer      | Status |
			| John Soane’s house, 13 Lincoln’s Inn Fields | 100000 GBP | NEW    |
	When User clicks 1 offer details on view requirement page
	Then Offer details on view requirement page are same as the following
		| Details                                     | Status | Offer      | SpecialConditions | Negotiator |
		| John Soane’s house, 13 Lincoln’s Inn Fields | New    | 100000 GBP | Text              | John Smith |
	When User clicks view activity from offer on view requirement page
	Then View activity page is displayed
		And Offer should be displayed on view activity page
		And Offer details on 1 position on view activity page are same as the following
			| Details                                    | Offer      | Status |
			| John Soane, Robert McAlpine, Edward Graham | 100000 GBP | NEW    |
	When User clicks 1 offer details on view activity page
	Then Offer details on view activity page are same as the following
		| Details                                    | Status | Offer      | SpecialConditions | Negotiator |
		| John Soane, Robert McAlpine, Edward Graham | New    | 100000 GBP | Text              | John Smith |

@Requirement
@Offer
Scenario: Update residential sales offer on requirement
	Given Contacts are created in database
		| Title | FirstName | Surname |
		| Dr    | Indiana   | Jackson |
		And Property with Residential division and House type is defined
		And Property attributes details are defined
			| MinBedrooms | MaxBedrooms | MinReceptions | MaxReceptions | MinBathrooms | MaxBathrooms | MinArea | MaxArea | MinLandArea | MaxLandArea | MinCarParkingSpaces | MaxCarParkingSpaces |
			| 1           | 3           | 1             | 1             | 1            | 2            | 1000    | 3000    | 1400        | 5000        | 1                   | 2                   |
		And Property characteristics are defined
		And Property in GB is created in database
			| PropertyNumber | PropertyName | Line2     | Postcode | City  | County         |
			| 22             | House        | Eltham Dr | LS6 2TU  | Leeds | West Yorkshire |
		And Property ownership is defined
			| PurchaseDate | BuyPrice |
			| 10-12-2013   | 10000000 |
		And Property Freehold Sale activity is defined
		And Requirement for GB is created in database
			| MinPrice | MaxPrice | MinBedrooms | MaxBedrooms | MinReceptionRooms | MaxReceptionRooms | MinBathrooms | MaxBathrooms | MinParkingSpaces | MaxParkingSpaces | MinArea | MaxArea | MinLandArea | MaxLandArea | Description |
			| 100000   | 500000   | 1           | 3           | 1                 | 2                 | 1            | 3            | 1                | 3                | 900     | 3500    | 1200        | 6000        | Note        |
		And Viewing for requirement is defined
		And Offer for requirement is defined
	When User navigates to view requirement page with id
		And User clicks edit offer button for 1 offer on view requirement page
	Then Activity details on view requirement page are same as the following
		| Details             |
		| House, 22 Eltham Dr |
	When User fills in offer details on view requirement page
		| Status   | Offer | SpecialConditions  |
		| Accepted | 2000  | Special conditions |
		And User clicks save offer button on view requirement page
		And User clicks 1 offer details on view requirement page
	Then Offer details on view requirement page are same as the following
		| Details             | Status   | Offer    | SpecialConditions  | Negotiator |
		| House, 22 Eltham Dr | Accepted | 2000 GBP | Special conditions | John Smith |
