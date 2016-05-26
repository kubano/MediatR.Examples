namespace KnightFrank.Antares.Domain.Company.Commands
{
    using System;
    using System.Collections.Generic;

    using MediatR;

    public class UpdateCompanyCommand : IRequest<Guid>
    {
        public UpdateCompanyCommand()
        {
            this.ContactIds = new List<Guid>();
        }

		public Guid Id { get; set; }

		public string Name { get; set; }

        public string WebsiteUrl { get; set; }

        public string ClientCarePageUrl { get; set; }

        public Guid? ClientCareStatusId { get; set; }

        public IList<Guid> ContactIds { get; set; }
    }
}