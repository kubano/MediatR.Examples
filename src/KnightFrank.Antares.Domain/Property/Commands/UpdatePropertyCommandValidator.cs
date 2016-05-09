﻿namespace KnightFrank.Antares.Domain.Property.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using FluentValidation;
    using FluentValidation.Results;

    using KnightFrank.Antares.Dal.Model.Enum;
    using KnightFrank.Antares.Dal.Model.Property;
    using KnightFrank.Antares.Dal.Repository;
    using KnightFrank.Antares.Domain.Common.Commands;
    using KnightFrank.Antares.Domain.Common.Validator;

    using Attribute = KnightFrank.Antares.Dal.Model.Attribute.Attribute;

    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        private readonly IGenericRepository<PropertyTypeDefinition> propertyTypeDefinitionRepository;
        private readonly IGenericRepository<PropertyAttributeForm> propertyAttributeFormRepository;

        public UpdatePropertyCommandValidator(IGenericRepository<PropertyType> propertyTypeRepository,
            IGenericRepository<PropertyTypeDefinition> propertyTypeDefinitionRepository,
            IGenericRepository<EnumTypeItem> enumTypeItemRepository,
            IGenericRepository<PropertyAttributeForm> propertyAttributeFormRepository)
        {
            this.propertyTypeDefinitionRepository = propertyTypeDefinitionRepository;
            this.propertyAttributeFormRepository = propertyAttributeFormRepository;

            this.RuleFor(v => v.Id).NotNull();
            this.RuleFor(x => x.Address).NotNull();
            this.RuleFor(x => x.Address).SetValidator(new CreateOrUpdateAddressValidator());
            this.RuleFor(v => v.PropertyTypeId).NotEqual(Guid.Empty).NotNull();
            
            this.RuleFor(x => x.DivisionId).NotEqual(Guid.Empty);

            this.RuleFor(x => x.AttributeValues).NotNull();

            this.When(x => x.AttributeValues != null, () =>
            {
                this.RuleFor(x => x.AttributeValues)
                    .Must(this.BeOverOrEqualZero).WithMessage("Attributes values cannot be lower than 0.")
                    .Must(this.BeBetweenMinMax).WithMessage("Attributes values minumum cannot be greater than maximum.")
                    .Must(this.BeAllowedForPropertyType).WithMessage("Property contains attributes incorrect for given property type.");
            });

            this.When(x => x.PropertyCharacteristics != null,() =>
            {
                this.RuleFor(x => x.PropertyCharacteristics).SetValidator(new PropertyCharacteristicsUniqueValidator());
                this.RuleFor(x => x.PropertyCharacteristics).SetCollectionValidator(new CreateOrUpdatePropertyCharacteristicValidator());
            });
            
            this.Custom(this.PropertyTypeIsValid);
        }

        public ValidationFailure PropertyTypeIsValid(UpdatePropertyCommand updatePropertyCommand)
        {
            bool propertyTypeDefinitionExist =
                this.propertyTypeDefinitionRepository.Any(p => p.PropertyTypeId.Equals(updatePropertyCommand.PropertyTypeId)
                                                               && p.DivisionId.Equals(updatePropertyCommand.DivisionId)
                                                               && p.CountryId.Equals(updatePropertyCommand.Address.CountryId));
            return !propertyTypeDefinitionExist ? new ValidationFailure(nameof(updatePropertyCommand.PropertyTypeId), "Specified property type is invalid") : null;
        }

        private bool BeOverOrEqualZero(CreateOrUpdatePropertyAttributeValues attributeValues)
        {
            if (attributeValues.MinBedrooms.HasValue && attributeValues.MinBedrooms < 0)
                return false;

            if (attributeValues.MaxBedrooms.HasValue && attributeValues.MaxBedrooms < 0)
                return false;

            if (attributeValues.MinReceptions.HasValue && attributeValues.MinReceptions < 0)
                return false;

            if (attributeValues.MaxReceptions.HasValue && attributeValues.MaxReceptions < 0)
                return false;

            if (attributeValues.MinBathrooms.HasValue && attributeValues.MinBathrooms < 0)
                return false;

            if (attributeValues.MaxBathrooms.HasValue && attributeValues.MaxBathrooms < 0)
                return false;

            if (attributeValues.MinArea.HasValue && attributeValues.MinArea < 0)
                return false;

            if (attributeValues.MaxArea.HasValue && attributeValues.MaxArea < 0)
                return false;

            if (attributeValues.MinLandArea.HasValue && attributeValues.MinLandArea < 0)
                return false;

            if (attributeValues.MaxLandArea.HasValue && attributeValues.MaxLandArea < 0)
                return false;

            if (attributeValues.MinGuestRooms.HasValue && attributeValues.MinGuestRooms < 0)
                return false;

            if (attributeValues.MaxGuestRooms.HasValue && attributeValues.MaxGuestRooms < 0)
                return false;

            if (attributeValues.MinFunctionRooms.HasValue && attributeValues.MinFunctionRooms < 0)
                return false;

            if (attributeValues.MaxFunctionRooms.HasValue && attributeValues.MaxFunctionRooms < 0)
                return false;

            if (attributeValues.MinCarParkingSpaces.HasValue && attributeValues.MinCarParkingSpaces < 0)
                return false;

            if (attributeValues.MaxCarParkingSpaces.HasValue && attributeValues.MaxCarParkingSpaces < 0)
                return false;

            return true;
        }

        private bool BeBetweenMinMax(CreateOrUpdatePropertyAttributeValues attributeValues)
        {
            if (attributeValues.MinBedrooms.HasValue && attributeValues.MaxBedrooms.HasValue)
                if (attributeValues.MinBedrooms > attributeValues.MaxBedrooms)
                    return false;

            if (attributeValues.MinReceptions.HasValue && attributeValues.MaxReceptions.HasValue)
                if (attributeValues.MinReceptions > attributeValues.MaxReceptions)
                    return false;

            if (attributeValues.MinBathrooms.HasValue && attributeValues.MaxBathrooms.HasValue)
                if (attributeValues.MinBathrooms > attributeValues.MaxBathrooms)
                    return false;

            if (attributeValues.MinArea.HasValue && attributeValues.MaxArea.HasValue)
                if (attributeValues.MinArea > attributeValues.MaxArea)
                    return false;

            if (attributeValues.MinLandArea.HasValue && attributeValues.MaxLandArea.HasValue)
                if (attributeValues.MinLandArea > attributeValues.MaxLandArea)
                    return false;

            if (attributeValues.MinGuestRooms.HasValue && attributeValues.MaxGuestRooms.HasValue)
                if (attributeValues.MinGuestRooms > attributeValues.MaxGuestRooms)
                    return false;

            if (attributeValues.MinFunctionRooms.HasValue && attributeValues.MaxFunctionRooms.HasValue)
                if (attributeValues.MinFunctionRooms > attributeValues.MaxFunctionRooms)
                    return false;

            if (attributeValues.MinCarParkingSpaces.HasValue && attributeValues.MaxCarParkingSpaces.HasValue)
                if (attributeValues.MinCarParkingSpaces > attributeValues.MaxCarParkingSpaces)
                    return false;

            return true;
        }
        private bool BeAllowedForPropertyType(UpdatePropertyCommand updatePropertyCommand, CreateOrUpdatePropertyAttributeValues attributeValues)
        {
            PropertyAttributeForm propertyAttributeForm = this.propertyAttributeFormRepository
                .GetWithInclude(p => p.PropertyTypeId == updatePropertyCommand.PropertyTypeId && p.Country.Id == updatePropertyCommand.Address.CountryId,
                    p => p.PropertyAttributeFormDefinitions.Select(s => s.Attribute))
                .SingleOrDefault();

            if (propertyAttributeForm == null)
                return true;

            IEnumerable<Attribute> allowedAttributes = propertyAttributeForm
                .PropertyAttributeFormDefinitions
                .Select(s => s.Attribute);

            List<string> allowedAttributeNames = allowedAttributes.Select(a => a.NameKey).ToList();

            PropertyInfo[] attributeValueProperties = typeof(CreateOrUpdatePropertyAttributeValues)
                .GetProperties();

            List<string> attributeValueNames = attributeValueProperties.Select(a => a.Name).ToList();

            IEnumerable<string> allowedAttributeValues =
                from attributeValueName in attributeValueNames
                from allowedAttributeName in allowedAttributeNames
                where attributeValueName == "Min" + allowedAttributeName
                   || attributeValueName == "Max" + allowedAttributeName
                select attributeValueName;

            IEnumerable<object> notAllowedAttributeValues = attributeValueProperties
                .Where(propertyInfo => !allowedAttributeValues.Contains(propertyInfo.Name))
                .Select(propertyInfo => propertyInfo.GetValue(attributeValues));

            bool allNotAllowedAttributeValuesAreNull = notAllowedAttributeValues.All(value => value == null);

            return allNotAllowedAttributeValuesAreNull;
        }
    }
}
