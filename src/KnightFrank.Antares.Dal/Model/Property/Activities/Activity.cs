﻿namespace KnightFrank.Antares.Dal.Model.Property.Activities
{
    using System;
    using System.Collections.Generic;

    using KnightFrank.Antares.Dal.Model.Contacts;
    using KnightFrank.Antares.Dal.Model.Enum;

    public class Activity : BaseAuditableEntity
    {
        public Guid PropertyId { get; set; }

        public virtual Property Property { get; set; }
        
        public Guid ActivityStatusId { get; set; }

        public EnumTypeItem ActivityStatus { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public decimal? MarketAppraisalPrice { get; set; }

        public decimal? RecommendedPrice { get; set; }

        public decimal? VendorEstimatedPrice { get; set; }
    }
}