﻿namespace KnightFrank.Antares.UITests.Pages.Panels
{
    using KnightFrank.Antares.UITests.Extensions;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Common.WebElements;

    public class CreateOfferPage : ProjectPageBase
    {
        private readonly ElementLocator activityDetails = new ElementLocator(Locator.Id, "offer-add-activity-details");
        private readonly ElementLocator offer = new ElementLocator(Locator.Id, "offer-price");
        private readonly ElementLocator offerDate = new ElementLocator(Locator.Id, "offer-date");
        private readonly ElementLocator proposedCompletionDate = new ElementLocator(Locator.Id, "proposed-completion-date");
        private readonly ElementLocator proposedExchangeDate = new ElementLocator(Locator.Id, "offer-proposed-exchange-date");
        private readonly ElementLocator saveOffer = new ElementLocator(Locator.CssSelector, "button[ng-click *= 'saveOffer']");
        private readonly ElementLocator specialConditions = new ElementLocator(Locator.Name, "specialConditions");
        private readonly ElementLocator status = new ElementLocator(Locator.Id, "offer-status");
        private readonly ElementLocator viewLink = new ElementLocator(Locator.CssSelector, ".slide-in div:nth-of-type(1) a");

        public CreateOfferPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public string Details => this.Driver.GetElement(this.activityDetails).Text;

        public CreateOfferPage SelectStatus(string text)
        {
            this.Driver.GetElement<Select>(this.status).SelectByText(text);
            return this;
        }

        public CreateOfferPage SetOffer(string price)
        {
            this.Driver.SendKeys(this.offer, price);
            return this;
        }

        public CreateOfferPage SetOfferDate(string date)
        {
            this.Driver.SendKeys(this.offerDate, date);
            return this;
        }

        public CreateOfferPage SetSpecialConditions(string conditions)
        {
            this.Driver.SendKeys(this.specialConditions, conditions);
            return this;
        }

        public CreateOfferPage SetProposedExchangeDate(string date)
        {
            this.Driver.SendKeys(this.proposedExchangeDate, date);
            return this;
        }

        public CreateOfferPage SetProposedCompletionDate(string date)
        {
            this.Driver.SendKeys(this.proposedCompletionDate, date);
            return this;
        }

        public CreateOfferPage SaveOffer()
        {
            this.Driver.GetElement(this.saveOffer).Click();
            return this;
        }

        public CreateOfferPage ClickViewLink()
        {
            this.Driver.GetElement(this.viewLink).Click();
            return this;
        }
    }

    internal class OfferData
    {
        public string Status { get; set; }

        public string Offer { get; set; }

        public string OfferDate { get; set; }

        public string SpecialConditions { get; set; }

        public string ExchangeDate { get; set; }

        public string CompletionDate { get; set; }
    }
}