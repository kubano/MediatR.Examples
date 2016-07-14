﻿namespace KnightFrank.Antares.Domain.UnitTests.Company.Commands
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using FluentValidation.Resources;
    using FluentValidation.Results;

    using KnightFrank.Antares.Dal.Model.Contacts;
    using KnightFrank.Antares.Domain.Company.Commands;
    using KnightFrank.Antares.Domain.Company.CustomValidators;
    using KnightFrank.Antares.Tests.Common.Extensions.AutoFixture.Attributes;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    [Trait("FeatureTitle", "Company")]
    [Collection("UpdateCompanyValidator")]
    public class UpdateCompanyValidatorTests : IClassFixture<BaseTestClassFixture>
    {
        [Theory]
        [AutoMoqData]
        public void Given_CorrectUpdateCompanyCommand_When_Validating_Then_NoValidationErrors(
            [Frozen] Mock<ICompanyCommandCustomValidator> customCompanyCommandValidatorMock,
            UpdateCompanyCommandValidator validator,
            UpdateCompanyCommand cmd)
        {
            //Arrange
            this.SetupCompanyEnumsValidation(customCompanyCommandValidatorMock);

            // Act
            ValidationResult result = validator.Validate(cmd);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public void Given_EmptyName_When_Validating_Then_IsInvalidAndHasAppropriateErrorMsg(
            [Frozen] Mock<ICompanyCommandCustomValidator> customCompanyCommandValidatorMock,
            UpdateCompanyCommandValidator validator,
            UpdateCompanyCommand cmd)
        {
            // Arrange
            this.SetupCompanyEnumsValidation(customCompanyCommandValidatorMock);

            cmd.Name = string.Empty;

            // Act
            ValidationResult result = validator.Validate(cmd);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(cmd.Name));
            result.Errors.Should().Contain(e => e.ErrorCode == nameof(Messages.notempty_error));
        }

        [Theory]
        [AutoMoqData]
        public void Given_NameIsTooLong_When_Validating_Then_IsInvalidAndHasAppropriateErrorMsg(
            [Frozen] Mock<ICompanyCommandCustomValidator> customCompanyCommandValidatorMock,
            UpdateCompanyCommandValidator validator,
            UpdateCompanyCommand cmd)
        {
            // Arrange
            this.SetupCompanyEnumsValidation(customCompanyCommandValidatorMock);

            cmd.Name = new string('a', 129);

            // Act
            ValidationResult result = validator.Validate(cmd);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(cmd.Name));
            result.Errors.Should().Contain(e => e.ErrorCode == nameof(Messages.length_error));
        }

        [Theory]
        [AutoMoqData]
        public void Given_WebsiteUrlIsTooLong_When_Validating_Then_IsInvalidAndHasAppropriateErrorMsg(
            [Frozen] Mock<ICompanyCommandCustomValidator> customCompanyCommandValidatorMock,
            UpdateCompanyCommandValidator validator,
            UpdateCompanyCommand cmd)
        {
            // Arrange
            this.SetupCompanyEnumsValidation(customCompanyCommandValidatorMock);

            cmd.WebsiteUrl = new string('a', 2501);

            // Act
            ValidationResult result = validator.Validate(cmd);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(cmd.WebsiteUrl));
            result.Errors.Should().Contain(e => e.ErrorCode == nameof(Messages.length_error));
        }

        [Theory]
        [AutoMoqData]
        public void Given_ClientCarePageUrlIsTooLong_When_Validating_Then_IsInvalidAndHasAppropriateErrorMsg(
            [Frozen] Mock<ICompanyCommandCustomValidator> customCompanyCommandValidatorMock,
            UpdateCompanyCommandValidator validator,
            UpdateCompanyCommand cmd)
        {
            // Arrange
            this.SetupCompanyEnumsValidation(customCompanyCommandValidatorMock);

            cmd.ClientCarePageUrl = new string('a', 2501);

            // Act
            ValidationResult result = validator.Validate(cmd);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(cmd.ClientCarePageUrl));
            result.Errors.Should().Contain(e => e.ErrorCode == nameof(Messages.length_error));
        }

        [Theory]
        [AutoMoqData]
        public void Given_ContactsIdsListIsEmpty_When_Validating_Then_IsInvalidAndHasAppropriateErrorMsg(
            [Frozen] Mock<ICompanyCommandCustomValidator> customCompanyCommandValidatorMock,
            UpdateCompanyCommandValidator validator,
            UpdateCompanyCommand cmd)
        {
            // Arrange
            this.SetupCompanyEnumsValidation(customCompanyCommandValidatorMock);

            cmd.Contacts = new List<Contact>();

            // Act
            ValidationResult result = validator.Validate(cmd);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(cmd.Contacts));
            result.Errors.Should().Contain(e => e.ErrorCode == nameof(Messages.notempty_error));
        }

        [Theory]
        [AutoMoqData]
        public void Given_TooDescription_When_Validating_Then_IsInvalidAndHasAppropriateErrorMsg(
            [Frozen] Mock<ICompanyCommandCustomValidator> customCompanyCommandValidatorMock,
            CreateCompanyCommandValidator validator,
            CreateCompanyCommand cmd)
        {
            // Arrange
            this.SetupCompanyEnumsValidation(customCompanyCommandValidatorMock);

            cmd.Description = new string('a', 4001);

            // Act
            ValidationResult result = validator.Validate(cmd);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(cmd.Description));
            result.Errors.Should().Contain(e => e.ErrorCode == nameof(Messages.length_error));
        }

        [Theory]
        [AutoMoqData]
        public void Given_EmptyDescription_When_Validating_Then_IsValid(
            [Frozen] Mock<ICompanyCommandCustomValidator> customCompanyCommandValidatorMock,
            CreateCompanyCommandValidator validator,
            CreateCompanyCommand cmd)
        {
            // Arrange
            this.SetupCompanyEnumsValidation(customCompanyCommandValidatorMock);

            cmd.Description = null;

            // Act
            ValidationResult result = validator.Validate(cmd);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        private void SetupCompanyEnumsValidation(Mock<ICompanyCommandCustomValidator> customCompanyCommandValidatorMock)
        {
            customCompanyCommandValidatorMock.Setup(x => x.IsClientCareEnumValid(It.IsAny<Guid?>())).Returns(true);
            customCompanyCommandValidatorMock.Setup(x => x.IsCompanyCategoryEnumValid(It.IsAny<Guid?>())).Returns(true);
            customCompanyCommandValidatorMock.Setup(x => x.IsCompanyTypeEnumValid(It.IsAny<Guid?>())).Returns(true);
            customCompanyCommandValidatorMock.Setup(x => x.IsRelationshipManagerValid(It.IsAny<Guid?>())).Returns(true);
        }
    }
}
