﻿namespace KnightFrank.Antares.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Domain.Contact.Commands;

    using KnightFrank.Antares.Dal.Model.Contacts;
    using KnightFrank.Antares.Domain.Contact.Queries;

    using MediatR;

    /// <summary>
    ///     Controller class for contacts
    /// </summary>
    [RoutePrefix("api/contacts")]
    public class ContactsController : ApiController
    {
        private readonly IMediator mediator;

        /// <summary>
        ///     Contacts controller constructor
        /// </summary>
        /// <param name="mediator">Mediator instance.</param>
        public ContactsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///     Get contact list
        /// </summary>
        /// <returns>Contact entity collection</returns>
        [HttpGet]
        [Route("")]
        public IEnumerable<Contact> GetContacts()
        {
            return this.mediator.Send(new ContactsQuery());
        }

        /// <summary>
        ///     Get contact
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>Contact entity</returns>
        [HttpGet]
        [Route("{id}")]
        public Contact GetContact(Guid id)
        {
            var query = new ContactQuery { Id = id };

            Contact contact = this.mediator.Send(query);

            if (contact == null)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact not found."));
            }

            return contact;
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="command">Contact entity</param>
        [HttpPost]
        [Route("")]
        public Contact CreateContact([FromBody] CreateContactCommand command)
        {
            Guid contactId = this.mediator.Send(command);

            return this.GetContact(contactId);
        }

        /// <summary>
        /// Get contact title list
        /// </summary>
        [HttpGet]
        [Route("titles")]
        public IList<ContactTitle> GetContactTitles()
        {
            return this.mediator.Send(new ContactTitleQuery()).ToList();
        }

        /// <summary>
        /// Updates a contact
        /// </summary>
        /// <param name="command">The command with contact to update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public Contact UpdateContact(UpdateContactCommand command)
        {
            Guid contactId = this.mediator.Send(command);

            return this.GetContact(contactId);
        }
    }
}
