﻿Feature: Requirement attachments UI tests

@Requirement
Scenario: Upload attachment for requirement
	Given Contacts are created in database
		| Title | FirstName | LastName |
		| Miss  | Alana     | Duran   |
		And Requirement for GB is created in database
			| MinPrice | MaxPrice | MinBedrooms | MaxBedrooms | MinReceptionRooms | MaxReceptionRooms | MinBathrooms | MaxBathrooms | MinParkingSpaces | MaxParkingSpaces | MinArea | MaxArea | MinLandArea | MaxLandArea | Description |
			| 1000     | 5000     | 1           | 1           | 1                 | 1                 | 1            | 1            | 1                | 1                | 900     | 1500    | 2000        | 3000        | Note        |
	When User navigates to view requirement page with id
		And User clicks add attachment button on view requirement page
		And User adds PDF document.pdf file with Terms of Business type on view requirement page
	Then Attachment should be displayed on view requirement page
		| FileName         | Type              | Size   |
		| PDF document.pdf | Terms of Business | 2.9 MB |
	When User clicks attachment card on view requirement page
	Then Attachment details on attachment preview page are the same like on view requirement page
		And Requirement attachment PDF document.pdf should be downloaded
		And User closes attachment preview page on view requirement page
