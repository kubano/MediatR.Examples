﻿namespace KnightFrank.Antares.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using KnightFrank.Antares.Dal;
    using KnightFrank.Antares.Dal.Model.Property.Activities;
    using KnightFrank.Antares.Dal.Repository;
    using KnightFrank.Antares.Domain.Activity.Commands;
    using KnightFrank.Antares.Domain.Activity.Queries;
    using KnightFrank.Antares.Domain.Activity.QueryResults;

    using MediatR;

    /// <summary>
    ///     Controller class for contacts
    /// </summary>
    [RoutePrefix("api/activities")]
    public class ActivitiesController : ApiController
    {
        private readonly IMediator mediator;
        private readonly ReadGenericRepository<ActivityType> repository;

        /// <summary>
        ///     Contacts controller constructor
        /// </summary>
        /// <param name="mediator">Mediator instance.</param>
        public ActivitiesController(IMediator mediator, ReadGenericRepository<ActivityType> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        /// <summary>
        ///     Creates the activity
        /// </summary>
        /// <param name="command">Activity data to create</param>
        [HttpPost]
        [Route("")]
        public Activity CreateActivity([FromBody] CreateActivityCommand command)
        {
            Guid activityId = this.mediator.Send(command);

            return this.GetActivity(activityId);
        }

        /// <summary>
        ///     Gets the activity
        /// </summary>
        /// <param name="id">Activity id</param>
        /// <returns>Activity entity</returns>
        [HttpGet]
        [Route("{id}")]
        public Activity GetActivity(Guid id)
        {
            Activity activity = this.mediator.Send(new ActivityQuery { Id = id });

            if (activity == null)
            {
                throw new HttpResponseException(this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Activity not found."));
            }

            return activity;
        }

        /// <summary>
        ///     Updates the activity
        /// </summary>
        /// <param name="command">Activity data to update</param>
        /// <returns>Activity entity</returns>
        [HttpPut]
        [Route("")]
        public Activity UpdateActivity(UpdateActivityCommand command)
        {
            //TODO remove after id is sent by UI
            command.ActivityTypeId = this.repository.Get().First().Id;

            Guid activityId = this.mediator.Send(command);

            return this.GetActivity(activityId);
        }

        /// <summary>
        /// Gets the activity types.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>List of activity types</returns>
        [HttpGet]
        [Route("types")]
        public IEnumerable<ActivityTypeQueryResult> GetActivityTypes([FromUri(Name = "")]ActivityTypeQuery query)
        {
            return this.mediator.Send(query);
        }
    }
}
