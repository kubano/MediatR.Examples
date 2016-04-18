﻿namespace KnightFrank.Antares.Dal.Model.Characteristics
{
    using System;

    using KnightFrank.Antares.Dal.Model.Resource;

    public class CharacteristicLocalised : BaseEntity
    {
        public Guid CharacteristicId { get; set; }
        public Guid LocaleId { get; set; }
        public virtual Locale Locale { get; set; }
        public string Label { get; set; }
    }
}