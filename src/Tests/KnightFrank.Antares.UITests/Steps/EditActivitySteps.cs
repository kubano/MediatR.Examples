﻿namespace KnightFrank.Antares.UITests.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using KnightFrank.Antares.Dal.Model.Property.Activities;
    using KnightFrank.Antares.UITests.Pages;

    using Objectivity.Test.Automation.Common;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    using Xunit;

    [Binding]
    public class EditActivitySteps
    {
        private const string Format = "dd-MM-yyyy";
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;
        private EditActivityPage page;

        public EditActivitySteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException(nameof(scenarioContext));
            }

            this.scenarioContext = scenarioContext;
            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;

            if (this.page == null)
            {
                this.page = new EditActivityPage(this.driverContext);
            }
        }

        [When(@"User navigates to edit activity page with id")]
        public void OpenEditActivityPageWithId()
        {
            Guid activityId = this.scenarioContext.Get<Activity>("Activity").Id;
            this.page = new EditActivityPage(this.driverContext).OpenEditActivityPage(activityId.ToString());
        }

        [When(@"User edits activity details on edit activity page")]
        public void EditActivityDetails(Table table)
        {
            var details = table.CreateInstance<EditActivityDetails>();

            this.page.SelectActivityStatus(details.ActivityStatus)
                .SetKfValuationPricePerWeek(details.KfValuationShortLet, details.KfValuationLongLet);
        }

        [When(@"User fills in KF Valuation (.*) on edit activity page")]
        public void WhenUserFillsInKfValuationOnEditActivityPage(string kfValuation)
        {
            this.page.FillKfValuation(kfValuation);
        }

        [When(@"User selects (.*) disposal type on edit activity page")]
        public void WhenUserSelectsDisposalTypeOnEditActivityPage(string disposalType)
        {
            this.page.SelectDisposalType(disposalType);
        }

        [When(@"User clicks save button on edit activity page")]
        public void SaveActivty()
        {
            this.page.SaveActivity();
        }

        [When(@"User changes lead negotiator to (.*) on edit activity page")]
        public void UpdateLeadNegotiator(string user)
        {
            this.page.SetLeadNegotiator(user);
        }

        [When(@"User adds secondary negotiators on edit activity page")]
        public void AddSecondaryNegotiators(Table table)
        {
            IEnumerable<Negotiator> secondaryNegotiators = table.CreateSet<Negotiator>();
            foreach (Negotiator element in secondaryNegotiators)
            {
                this.page.SetSecondaryNegotiator(element);
            }
        }

        [When(@"User removes (.*) secondary negotiator on edit activity page")]
        public void RemoveSecondaryNegotiator(int secondaryNegotiator)
        {
            this.page.RemoveSecondaryNegotiator(secondaryNegotiator);
        }

        [When(@"User sets (.*) secondary negotiator as lead negotiator on edit activity page")]
        public void SetLeadNegotiatorFromSecondaryNegotiator(int position)
        {
            this.page.SetSecondaryNegotiatorAsLeadNegotiator(position);
        }

        [When(@"User edits secondary negotiators dates on edit activity page")]
        public void UpdateSecondaryNegotiatorsNextCall(Table table)
        {
            List<Negotiator> nextCall = table.CreateSet<Negotiator>().ToList();
            for (var position = 1; position <= nextCall.Count; position ++)
            {
                this.page.SetSecondaryNegotiatorsCallDate(position, nextCall[position - 1].NextCall);
            }
        }

        [When(@"User removes (.*) department on edit activity page")]
        public void RemoveDepartment(string departmentName)
        {
            this.page.RemoveDepartment(departmentName);
        }

        [When(@"User sets (.*) department as managing department on edit activity page")]
        public void DepartmentUpdate(string departmentName)
        {
            this.page.SetDepartmentAsManaging(departmentName);
        }

        [When(@"User fills in rent details on edit activity page")]
        public void FillInRentDetails(Table table)
        {
            var rentDetails = table.CreateInstance<LettingRent>();

            this.page.SelectRent(rentDetails.Rent)
                .SetAskingRentShortLet(rentDetails.RentShortLet)
                .SelectShortLetMatchFlexibility(rentDetails.RentShortLetMatchFlexibility)
                .SetShortLetMatchFlexibilityPercentage(rentDetails.RentShortLetMatchFlexibilityRentPercentage)
                .SetAskingRentLongLet(rentDetails.RentLongLet)
                .SelectLongLetMatchFlexibility(rentDetails.RentLongLetMatchFlexibility)
                .SetLongLetMatchFlexibilityRentValue(rentDetails.RentLongLetMatchFlexibilityRentValue);
        }

        [When(@"User fills in price details on edit activity page")]
        public void FillInPriceDetails(Table table)
        {
            var priceDetails = table.CreateInstance<Prices>();
            this.page.SelectPriceType(priceDetails.PriceType)
                .SetPrice(priceDetails.Price)
                .SelectMatchFlexibility(priceDetails.MatchFlexibility)
                .SetMatchFlexibilityValue(priceDetails.MatchFlexibility, priceDetails.MatchFlexibilityValue);
        }

        [Then(@"Lead negotiator next call is set to current date on edit activity page")]
        public void CheckLedNegotiatorNextCall()
        {
            Assert.Equal(DateTime.UtcNow.ToString(Format), this.page.LeadNegotiatorNextCall);
        }

        [Then(@"Secondary negotiators next calls are displayed on edit activity page")]
        public void ThenSecondaryNegotiatorsNextCallAreDisplayedOnEditActivityPage(Table table)
        {
            List<Negotiator> expectedNextCall = table.CreateSet<Negotiator>().ToList();
            foreach (Negotiator negotiator in expectedNextCall)
            {
                if (negotiator.NextCall != string.Empty)
                {
                    negotiator.NextCall =
                        DateTime.UtcNow.AddDays(int.Parse(negotiator.NextCall)).ToString(Format);
                }
            }
            List<string> actualNextCall = this.page.SecondaryNegotiatorsNextCalls;
            actualNextCall.Should().Equal(expectedNextCall.Select(el => el.NextCall));
        }
    }
}
