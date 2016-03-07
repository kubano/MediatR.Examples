﻿namespace KnightFrank.Antares.UITests.Pages
{
    using System;

    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;

    public class NotesPage : ProjectPageBase
    {
        private readonly ElementLocator noteTextArea = new ElementLocator(Locator.Id, string.Empty);
        private readonly ElementLocator saveNoteButton = new ElementLocator(Locator.Id, string.Empty);
        private readonly ElementLocator listOfNotes = new ElementLocator(Locator.Id, string.Empty);

        public NotesPage(DriverContext driverContext) : base(driverContext)
        {
        }

        public NotesPage SetNoteText(string noteText)
        {
            this.Driver.GetElement(this.noteTextArea).SendKeys(noteText);
            return this;
        }

        public NotesPage SaveNote()
        {
            this.Driver.GetElement(this.saveNoteButton).Click();
            return this;
        }

        public int GetNumberOfNotes()
        {
            try
            {
                //TODO check timeout settings for getelements when there are no elements
                return this.Driver.GetElements(this.listOfNotes).Count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}