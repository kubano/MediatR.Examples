﻿namespace KnightFrank.Antares.Dal.Seed
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using KnightFrank.Antares.Dal.Model.Attribute;
    using KnightFrank.Antares.Dal.Model.Property;

    internal static class PropertyAttributeFromData
    {
        public static void Seed(KnightFrankContext context)
        {
            IEnumerable<PropertyAttributeForm> forms = CreatePropertyAttributeForms(context);

            context.PropertyAttributeForms.AddOrUpdate(p => new { p.PropertyTypeId, p.CountryId }, forms.ToArray());

            context.SaveChanges();
        }

        private static IEnumerable<PropertyAttributeForm> CreatePropertyAttributeForms(KnightFrankContext context)
        {
            List<PropertyType> propertyTypes = context.PropertyTypes.ToList();

            List<Attribute> attributes = context.Attributes.ToList();

            return from country in propertyAttributeFormData from data in country.Value select new PropertyAttributeForm
            {
                CountryId = context.Countries.Single(c => c.IsoCode == country.Key).Id,
                PropertyTypeId = propertyTypes.Single(p => p.Code == data.Key).Id,
                PropertyAttributeFormDefinitions = CreatePropertyAttributeFormDefinitions(attributes, data.Value).ToList()
            };
        }

        private static IEnumerable<PropertyAttributeFormDefinition> CreatePropertyAttributeFormDefinitions(List<Attribute> attributes, List<string> value)
        {
            var i = 0;
            foreach (Attribute attribute in attributes.Where(attribute => value.Contains(attribute.NameKey)))
            {
                yield return new PropertyAttributeFormDefinition
                {
                    AttributeId = attribute.Id,
                    Order = i
                };

                i++;
            }
        }

        private static readonly Dictionary<string, Dictionary<string, List<string>>> propertyAttributeFormData = new Dictionary
            <string, Dictionary<string, List<string>>>
        {
            ["GB"] = new Dictionary<string, List<string>>
            {
                ["House"] = new List<string>      { "Bedrooms", "Receptions", "Bathrooms", "Area", "LandArea", "CarParkingSpaces" },
                ["Flat"] = new List<string>       { "Bedrooms", "Receptions", "Bathrooms", "Area", "LandArea", "CarParkingSpaces" },
                ["Bungalow"] = new List<string>   { "Bedrooms", "Receptions", "Bathrooms", "Area", "LandArea", "CarParkingSpaces" },
                ["Houseboat"] = new List<string>  { "Bedrooms", "Receptions", "Bathrooms", "Area" },
                ["Maisonette"] = new List<string> { "Bedrooms", "Receptions", "Bathrooms", "Area", "CarParkingSpaces" },
                ["Development Plot"] = new List<string> { "LandArea" },
                ["Hotel"] = new List<string> { "Area", "LandArea", "GuestRooms", "FunctionRooms", "CarParkingSpaces" }
            }
        };
    }
}
