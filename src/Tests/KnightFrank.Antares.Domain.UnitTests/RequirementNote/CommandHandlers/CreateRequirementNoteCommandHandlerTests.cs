﻿namespace KnightFrank.Antares.Domain.UnitTests.RequirementNote.CommandHandlers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using FluentValidation.Results;

    using KnightFrank.Antares.Dal.Model.Property;
    using KnightFrank.Antares.Dal.Model.User;
    using KnightFrank.Antares.Dal.Repository;
    using KnightFrank.Antares.Domain.Common;
    using KnightFrank.Antares.Domain.Common.Exceptions;
    using KnightFrank.Antares.Domain.RequirementNote.CommandHandlers;
    using KnightFrank.Antares.Domain.RequirementNote.Commands;

    using Moq;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;
    [Trait("FeatureTitle", "RequirementNote")]
    [Collection("CreateRequirementNoteCommandHandler")]
    public class CreateRequirementNoteCommandHandlerTests : IClassFixture<BaseTestClassFixture>
    {
        [Theory]
        [AutoMoqData]
        public void Given_ValidCommand_When_Handling_Then_ShouldSaveRequirementNote(
            [Frozen] Mock<IGenericRepository<RequirementNote>> requirementNoteRepository,
            [Frozen] Mock<IGenericRepository<User>> userRepository,
            User user,
            CreateRequirementNoteCommandHandler handler,
            CreateRequirementNoteCommand cmd,
            Guid expectedRequirementNoteId)
        {
            // Arrange
            RequirementNote requirementNote = null;
            requirementNoteRepository.Setup(r => r.Add(It.IsAny<RequirementNote>()))
                              .Returns((RequirementNote rn) =>
                              {
                                  requirementNote = rn;
                                  return requirementNote;
                              });
            requirementNoteRepository.Setup(r => r.Save()).Callback(() => { requirementNote.Id = expectedRequirementNoteId; });

            userRepository.Setup(p => p.FindBy(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User> { user });

            // Act
            handler.Handle(cmd);

            // Assert
            requirementNote.Should().NotBeNull();
            requirementNote.ShouldBeEquivalentTo(cmd, opt => opt.IncludingProperties().ExcludingMissingMembers());
            requirementNote.Id.ShouldBeEquivalentTo(expectedRequirementNoteId);
            requirementNoteRepository.Verify(r => r.Add(It.IsAny<RequirementNote>()), Times.Once());
            requirementNoteRepository.Verify(r => r.Save(), Times.Once());
        }

        [Theory]
        [AutoMoqData]
        public void Given_CommandHasInvalidRequirement_When_Handling_Then_ShouldThrowDomainValidatorException(
            [Frozen] Mock<IDomainValidator<CreateRequirementNoteCommand>> domainValidator,
            CreateRequirementNoteCommandHandler handler,
            CreateRequirementNoteCommand cmd)
        {
            // Arrange
            ICollection<ValidationFailure> validationFailures = new List<ValidationFailure> { new ValidationFailure(nameof(cmd.RequirementId), "Error") };

            domainValidator.Setup(v => v.Validate(It.IsAny<CreateRequirementNoteCommand>()))
                           .Returns(new ValidationResult(validationFailures));

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => { handler.Handle(cmd); });
            Assert.Throws<DomainValidationException>(() => handler.Handle(cmd)).Errors.ShouldBeEquivalentTo(validationFailures);


        }
    }
}