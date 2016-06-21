﻿namespace KnightFrank.Antares.Domain.Property.QueryHandlers
{
    using System.Data.Entity;
    using System.Linq;

    using KnightFrank.Antares.Dal.Model.Property;
    using KnightFrank.Antares.Dal.Repository;
    using KnightFrank.Antares.Domain.Property.Queries;

    using MediatR;

    public class PropertyQueryHandler : IRequestHandler<PropertyQuery, Property>
    {
        private readonly IReadGenericRepository<Property> propertyRepository;

        public PropertyQueryHandler(IReadGenericRepository<Property> propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public Property Handle(PropertyQuery message)
        {
            Property result =
                this.propertyRepository.Get()
                    .Include(p => p.Ownerships.Select(o => o.Contacts))
                    .Include(p => p.Ownerships.Select(o => o.OwnershipType))
                    .Include(p => p.Address)
                    .Include(p => p.AttributeValues)
                    .Include(p => p.Division)
                    .Include(p => p.Activities.Select(o => o.Contacts))
                    .Include(p => p.Activities.Select(a => a.ActivityStatus))
                    .Include(p => p.PropertyCharacteristics)
                    .Include(p => p.PropertyAreaBreakdowns)
                    .Include(a => a.Attachments)
                    .Include(a => a.Attachments.Select(at => at.User))
                    .FirstOrDefault(p => p.Id == message.Id);

            if (result != null)
            {
                result.PropertyAreaBreakdowns = result.PropertyAreaBreakdowns.OrderBy(x => x.Order).ToList();
            }

            return result;
        }
    }
}
