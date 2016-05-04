﻿Feature: Contact UI tests

@Contact
Scenario: Create contact
	Given User navigates to create contact page
	When User fills in contact details on create contact page
		| FirstName | Surname   | Title |
		| Alan      | Macarthur | Sir   |
		And User clicks save button on create contact page
	Then New contact should be created