﻿namespace KnightFrank.Antares.UITests.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using FluentAssertions;

    using KnightFrank.Antares.Dal.Model.Contacts;
    using KnightFrank.Antares.Dal.Model.Property;
    using KnightFrank.Antares.UITests.Pages;
    using KnightFrank.Antares.UITests.Pages.Panels;

    using Objectivity.Test.Automation.Common;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    using Xunit;

    [Binding]
    public class ViewPropertySteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        public ViewPropertySteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException(nameof(scenarioContext));
            }

            this.scenarioContext = scenarioContext;
            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [When(@"User navigates to view property page with id")]
        public void OpenViewRequirementPageWithId()
        {
            Guid propertyId = this.scenarioContext.Get<Property>("Property").Id;
            ViewPropertyPage page = new ViewPropertyPage(this.driverContext).OpenViewPropertyPageWithId(propertyId.ToString());
            this.scenarioContext.Set(page, "ViewPropertyPage");
        }

        [When(@"User clicks add activites button on view property page")]
        public void ClickAddActivityButton()
        {
            this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage").AddActivity().WaitForSidePanelToShow();
        }

        [When(@"User clicks edit property button on view property page")]
        public void WhenUserClicksEditButtonOnCreatePropertyPage()
        {
            this.scenarioContext.Set(this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage").EditProperty(),
                "CreatePropertyPage");
        }

        [When(@"User selects contacts for ownership on view property page")]
        public void SelectContactsForOwnership(Table table)
        {
            ViewPropertyPage page =
                this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage").SetOwnership().WaitForSidePanelToShow();
            IEnumerable<Contact> contacts = table.CreateSet<Contact>();

            foreach (Contact contact in contacts)
            {
                page.ContactsList.WaitForContactsListToLoad().SelectContact(contact.FirstName, contact.Surname);
            }
            page.ContactsList.ConfigureOwnership();
        }

        [When(@"User clicks activity details link on view property page")]
        public void OpenActivitiesPreview()
        {
            this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage").OpenActivityDetails().WaitForSidePanelToShow();
        }

        [When(@"User clicks view activity link from activity on view property page")]
        public void OpenViewActivityPage()
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            this.scenarioContext.Set(page.PreviewDetails.ClickViewActivity(), "ViewActivityPage");
        }

        [When(@"User selects (.*) activity type on create activity page")]
        public void SelectActivityType(string type)
        {
            this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage").Activity.SelectActivityType(type);
        }

        [When(@"User selects (.*) activity status on create activity page")]
        public void SelectActivityStatus(string status)
        {
            this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage").Activity.SelectActivityStatus(status);
        }

        [When(@"User clicks save button on create activity page")]
        public void ClickSaveButtonOnActivityPanel()
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            page.Activity.SaveActivity();
            page.WaitForSidePanelToHide();
        }

        [When(@"User fills in ownership details on view property page")]
        public void FillInOwnershipDetails(Table table)
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            var details = table.CreateInstance<OwnershipDetails>();

            if (Convert.ToBoolean(details.Current))
            {
                page.Ownership.SetCurrentOwnership()
                    .SelectOwnershipType(details.Type);
                if (!string.IsNullOrEmpty(details.PurchasePrice))
                {
                    page.Ownership.SetPurchasePrice(details.PurchasePrice);
                }
                if (!string.IsNullOrEmpty(details.PurchaseDate))
                {
                    page.Ownership.SetPurchaseDate(details.PurchaseDate);
                }
            }
            else
            {
                page.Ownership.SetOwnership()
                    .SelectOwnershipType(details.Type);
                if (!string.IsNullOrEmpty(details.PurchasePrice))
                {
                    page.Ownership.SetPurchasePrice(details.PurchasePrice);
                }
                if (!string.IsNullOrEmpty(details.SellPrice))
                {
                    page.Ownership.SetSellPrice(details.SellPrice);
                }
                if (!string.IsNullOrEmpty(details.PurchaseDate))
                {
                    page.Ownership.SetPurchaseDate(details.PurchaseDate);
                }
                if (!string.IsNullOrEmpty(details.SellDate))
                {
                    page.Ownership.SetSellDate(details.SellDate);
                }
            }
            page.Ownership.SaveOwnership();
            page.WaitForSidePanelToHide();
        }

        [When(@"User clicks add area breakdown button on view property page")]
        public void AddArea()
        {
            this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage").CreateAreaBreakdown().WaitForSidePanelToShow();
        }

        [When(@"User fills in area details on view property page")]
        [When(@"User updates area details on view property page")]
        public void AddAreaDetails(Table table)
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            List<PropertyAreaBreakdown> areaBreakdowns = table.CreateSet<PropertyAreaBreakdown>().ToList();

            var place = 1;
            foreach (PropertyAreaBreakdown areaBreakdown in areaBreakdowns)
            {
                page.Area.SetAreaDetails(areaBreakdown.Name, areaBreakdown.Size, place++);
            }
        }

        [When(@"User clicks save area button on view property page")]
        public void SaveAreas()
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            page.Area.SaveArea();
            page.WaitForSidePanelToHide();
        }

        [When(@"User clicks edit area button for (.*) area on view property page")]
        public void EditArea(int position)
        {
            this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage")
                .OpenAreaActions(position)
                .EditArea(1)
                .WaitForSidePanelToShow();
        }

        [Then(@"Activity details are set on view property page")]
        public void CheckActivityDetails(Table table)
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            var details = table.CreateInstance<ActivityDetails>();

            Verify.That(this.driverContext,
                () => Assert.Equal(details.Vendor, page.ActivityVendor),
                () => Assert.Equal(details.Status, page.ActivityStatus),
                () => Assert.Equal(details.Type, page.ActivityType),
                () => Assert.Equal(DateTime.UtcNow.ToString("dd-MM-yyyy"), page.GetActivityDate()));
        }

        [Then(@"Property should be updated with address details")]
        [Then(@"New property should be created with address details")]
        public void CheckIfPropertyCreated(Table table)
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");

            foreach (string field in table.Rows.SelectMany(row => row.Values))
            {
                Assert.True(field.Equals(string.Empty)
                    ? page.IsAddressDetailsNotVisible(field)
                    : page.IsAddressDetailsVisible(field));
            }
        }

        [Then(@"New property should be created with (.*) property type and following attributes")]
        [Then(@"Property should be updated with (.*) property type and following attributes")]
        public void CheckPropertyType(string propertyType, Table table)
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            var details = table.CreateInstance<PropertyDetails>();

            Dictionary<string, string> actualDetails = page.GetPropertyDetails();
            Dictionary<string, string> expectedDetails =
                details.GetType()
                       .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                       .ToDictionary(prop => prop.Name.ToLower(), prop => prop.GetValue(details, null))
                       .Where(item => item.Value != null)
                       .ToDictionary(x => x.Key, x => x.Value.ToString());

            Assert.Equal(propertyType, page.PropertyType);
            actualDetails.ShouldBeEquivalentTo(expectedDetails);
        }

        [Then(@"Ownership details should contain following data on view property page")]
        public void CheckOwnershipContacts(Table table)
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            IEnumerable<OwnershipDetails> details = table.CreateSet<OwnershipDetails>();

            foreach (OwnershipDetails ownershipDetails in details)
            {
                string contact = page.GetOwnershipContact(ownershipDetails.Position);
                string currentOwnershipDetails = page.GetOwnershipDetails(ownershipDetails.Position);
                string expectdOwnershipDetails = ownershipDetails.Type.ToUpper();

                if (!string.IsNullOrEmpty(ownershipDetails.PurchaseDate) && string.IsNullOrEmpty(ownershipDetails.SellDate))
                {
                    expectdOwnershipDetails += " " + ownershipDetails.PurchaseDate + " -";
                }
                else if (string.IsNullOrEmpty(ownershipDetails.PurchaseDate) && !string.IsNullOrEmpty(ownershipDetails.SellDate))
                {
                    expectdOwnershipDetails += " - " + ownershipDetails.SellDate;
                }
                else if (string.IsNullOrEmpty(ownershipDetails.PurchaseDate) && string.IsNullOrEmpty(ownershipDetails.SellDate))
                {
                    expectdOwnershipDetails += " -";
                }
                else if (!string.IsNullOrEmpty(ownershipDetails.PurchaseDate) && !string.IsNullOrEmpty(ownershipDetails.SellDate))
                {
                    expectdOwnershipDetails += " " + ownershipDetails.PurchaseDate + " - " + ownershipDetails.SellDate;
                }

                Assert.Equal(ownershipDetails.ContactName + " " + ownershipDetails.ContactSurname, contact);
                Assert.Equal(expectdOwnershipDetails, currentOwnershipDetails);
            }
        }

        [Then(@"View property page is displayed")]
        public void CheckIfViewPropertyPresent()
        {
            Assert.True(this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage").IsViewPropertyFormPresent());
        }

        [Then(@"Activity details are set on create activity page")]
        public void CheckActivityDetailsonActivityPanel(Table table)
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            var details = table.CreateInstance<ActivityDetails>();

            Verify.That(this.driverContext,
                () => Assert.Equal(details.Vendor, page.Activity.GetActivityVendor()),
                () => Assert.Equal(details.Status, page.Activity.GetActivityStatus()));
        }

        [Then(@"Characteristics are displayed on view property page")]
        public void CheckCharacteristics(Table table)
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            Dictionary<string, string> actualDetails = page.GetCharacteristics();
            Dictionary<string, string> characteristics = table.CreateSet<Characteristic>()
                                                              .ToDictionary(x => x.Name, x => x.Comment);

            actualDetails.ShouldBeEquivalentTo(characteristics);
        }

        [Then(@"Area breakdown order is following on view property page")]
        public void CheckAreasOrder(Table table)
        {
            List<PropertyAreaBreakdown> expectedAreas = table.CreateSet<PropertyAreaBreakdown>().ToList();
            List<PropertyAreaBreakdown> actualAreas = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage").GetAreas();
            actualAreas.Should().Equal(expectedAreas, (c1, c2) =>
                c1.Name.Equals(c2.Name) &&
                c1.Order.Equals(c2.Order) &&
                c1.Size.Equals(c2.Size));
        }

        [Then(@"Property created success message should be displayed")]
        public void CheckIfSuccessMessageDisplayed()
        {
            var page = this.scenarioContext.Get<ViewPropertyPage>("ViewPropertyPage");
            Verify.That(this.driverContext,
                () => Assert.True(page.IsSuccessMessageDisplayed()),
                () => Assert.Equal("New property has been created", page.SuccessMessage));
            page.WaitForSuccessMessageToHide();
        }
    }
}
