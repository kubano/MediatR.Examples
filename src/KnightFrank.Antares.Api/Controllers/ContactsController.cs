﻿namespace KnightFrank.Antares.API.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Dal.Model;
    using Domain.Contact.Commands;

    using MediatR;

    /// <summary>
    ///     Controller class for contacts
    /// </summary>
    public class ContactsController : ApiController
    {
        private IMediator mediator;

        public ContactsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///     Get contact list
        /// </summary>
        /// <returns>Contact entity collection</returns>
        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            return new[]
            { new Contact { FirstName = "Tomasz", Surname = "Bien", Title="Mister" }, new Contact { FirstName = "David", Surname = "Dummy", Title="Mister" } };
        }

        /// <summary>
        ///     Get contact
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>Contact entity</returns>
        [HttpGet]
        public Contact GetContact(int id)
        {
            return new Contact { FirstName = "Tomasz", Surname = "Bien", Title = "Mister" };
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="command">Contact entity</param>
        [HttpPost]
        public int CreateContact([FromBody] CreateContactCommand command)
        {
            return this.mediator.Send(command);
        }

        /// <summary>
        ///     Update contact
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <param name="command">Contact entity</param>
        [HttpPut]
        public void UpdateContact(int id, [FromBody] UpdateContactCommand command)
        {
            this.mediator.Send(command);
        }

        /// <summary>
        ///     Delete contact
        /// </summary>
        /// <param name="id">Contact id</param>
        [HttpDelete]
        public void DeleteContact(int id)
        {
        }
    }
}
