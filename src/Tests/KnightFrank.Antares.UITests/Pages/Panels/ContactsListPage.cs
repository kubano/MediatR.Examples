﻿namespace KnightFrank.Antares.UITests.Pages.Panels
{
    using System.Linq;

    using KnightFrank.Antares.UITests.Extensions;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;

    public class ContactsListPage : ProjectPageBase
    {
        private readonly ElementLocator contact = new ElementLocator(Locator.XPath, "//label[contains(.,'{0}')]//input");
        private readonly ElementLocator loadingIndicator = new ElementLocator(Locator.CssSelector, "[ng-show *= 'isLoading']");
        private readonly ElementLocator saveButton = new ElementLocator(Locator.CssSelector, "button[ng-click *= 'updateContacts']");
        private readonly ElementLocator configureButton = new ElementLocator(Locator.CssSelector, "button[ng-click *= 'showOwnershipAdd']");

        public ContactsListPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public ContactsListPage WaitForContactsListToLoad()
        {
            this.Driver.WaitUntilElementIsNoLongerFound(this.loadingIndicator, BaseConfiguration.MediumTimeout);
            this.Driver.WaitForAngularToFinish();
            return this;
        }

        public ContactsListPage SelectContact(string firstName, string lastName)
        {
            string contactDetails = firstName + " " + lastName;

            if (!this.Driver.GetElements(this.contact.Format(contactDetails)).Last().Selected)
            {
                this.Driver.GetElements(this.contact.Format(contactDetails)).Last().Click();
            }
            return this;
        }

        public ContactsListPage SaveContact()
        {
            this.Driver.Click(this.saveButton);
            return this;
        }

        public void ConfigureOwnership()
        {
            this.Driver.Click(this.configureButton);
        }
    }
}
