﻿namespace KnightFrank.Antares.UITests.Steps
{
    using System;
    using System.Collections.Generic;

    using KnightFrank.Antares.Dal.Model;
    using KnightFrank.Antares.UITests.Pages;

    using Objectivity.Test.Automation.Common;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class CreatePropertySteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        public CreatePropertySteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException(nameof(scenarioContext));
            }

            this.scenarioContext = scenarioContext;
            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [Given(@"User navigates to create property page")]
        public void OpenCreatePropertyPage()
        {
            var page = new CreatePropertyPage(this.driverContext);
            this.scenarioContext["CreatePropertyPage"] = page;
        }

        [When(@"User selects country on create property page")]
        public void SelectCountryFromDropDownList(Table table)
        {
            var address = table.CreateInstance<Address>();
            this.scenarioContext.Get<CreatePropertyPage>("CreatePropertyPage")
                .AddressTemplate.SelectPropertyCountry(address.Country.IsoCode);
        }

        [When(@"User fills in address details on create property page")]
        public void FillInAddressDetails(Table table)
        {
            var address = table.CreateInstance<Address>();
            var page = this.scenarioContext.Get<CreatePropertyPage>("CreatePropertyPage");

            page.AddressTemplate
                .SetPropertyNumber(address.PropertyNumber)
                .SetPropertyName(address.PropertyName)
                .SetPropertyAddressLine2(address.Line2)
                .SetPropertyAddressLine3(address.Line3)
                .SetPropertyPostCode(address.Postcode)
                .SetPropertyCity(address.City)
                .SetPropertyCounty(address.County);
        }

        [When(@"User selects property types on create property page")]
        public void SelectPropertyTypes(Table table)
        {
            var types = table.CreateInstance<List<PropertyType>>();
            var page = this.scenarioContext.Get<CreatePropertyPage>("CreatePropertyPage");

            foreach (PropertyType type in types)
            {
                page.SelectPropertyType(type.Type);
            }
        }

        [When(@"User clicks save button on create property page")]
        public void ClickSaveButton()
        {
            this.scenarioContext.Get<CreatePropertyPage>("CreatePropertyPage").SaveProperty();
        }
    }

    internal class PropertyType
    {
        public string Type { get; set; }
    }
}
