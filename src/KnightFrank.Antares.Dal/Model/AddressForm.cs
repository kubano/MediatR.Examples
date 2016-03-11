﻿namespace KnightFrank.Antares.Dal.Model
{
    using System;
    using System.Collections.Generic;

    public class AddressForm : BaseEntity
    {
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<AddressFieldDefinition> AddressFieldFormDefinitions { get; set; }
        public virtual ICollection<AddressFormEntityType> AddressFormEntityTypes { get; set; }
    }
}