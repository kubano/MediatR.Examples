﻿using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace KnightFrank.Antares.Domain.UnitTests.User.QueryHandlers
{
    using FluentAssertions;
    using Moq;

    using KnightFrank.Antares.Dal.Model.User;
    using KnightFrank.Antares.Dal.Repository;
    using KnightFrank.Antares.Domain.User.Queries;
    using KnightFrank.Antares.Domain.User.QueryHandlers;
    using KnightFrank.Antares.Domain.User.QueryResults;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Xunit2;

    [Collection("UserQueryHandler")]
    [Trait("FeatureTitle", "Users")]
    public class UsersQueryHandlerTests
    {
        private readonly Mock<IReadGenericRepository<User>> userRepository;
        private readonly UsersQueryHandler handler;
        private readonly Department mockedDepartmentData;
        private readonly UsersQuery query;

        public UsersQueryHandlerTests()
        {
            //???:Isn't it better to have a shared fixture that is rune once per class rather than one per test?
            IFixture fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Behaviors.Clear();
            fixture.RepeatCount = 1;
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            this.userRepository = fixture.Freeze<Mock<IReadGenericRepository<User>>>();
            this.mockedDepartmentData = fixture.Create<Department>();
            this.query = fixture.Create<UsersQuery>();
            this.handler = fixture.Create<UsersQueryHandler>();
        }

        [Theory]
        [InlineAutoData("jon")]
        [InlineAutoData("JON")]
        [InlineAutoData("Jon")]
        public void Given_ExistingUsersInQuery_When_Handling_Then_CorrectResultsReturned(string partialName)
        {
            // Arrange
            IList<User> userList = this.CreateUserList(this.mockedDepartmentData);
            this.userRepository.Setup(x => x.Get()).Returns(userList.AsQueryable());
            this.query.PartialName = partialName.ToLower();

            // Act
            IEnumerable<UsersQueryResult> resultUserList = this.handler.Handle(this.query).AsQueryable();

            //Assert
            resultUserList.Should().HaveCount(2);
            resultUserList.Should().BeInAscendingOrder(x => x.FirstName);

            //first name OR last name matches query.
            Assert.All(resultUserList,
                user => Assert.True(user.FirstName.StartsWith(this.query.PartialName, StringComparison.CurrentCultureIgnoreCase)
                                    || user.LastName.StartsWith(this.query.PartialName, StringComparison.CurrentCultureIgnoreCase))
                );
        }

        [Theory]
        [InlineAutoData("abc")]
        public void Given_NotExistsingUserInQuery_When_Handling_Then_ShouldReturnEmptyList(string partialName)
        {
            //Arrange
            IList<User> userList = this.CreateUserList(this.mockedDepartmentData);
            this.userRepository.Setup(x => x.Get()).Returns(userList.AsQueryable());

            this.query.PartialName = partialName;

            //Act
            IEnumerable<UsersQueryResult> resultUserList = this.handler.Handle(this.query).AsQueryable();

            //Assert
            resultUserList.Should().BeEmpty();
        }

        [Fact]
        public void Given_EmptyStringInQuery_When_Handling_Then_ShouldReturnEmptyList()
        {
            //Arrange
            IList<User> userList = this.CreateUserList(this.mockedDepartmentData);
            this.userRepository.Setup(x => x.Get()).Returns(userList.AsQueryable());

            this.query.PartialName = string.Empty;

            //Act
            IEnumerable<UsersQueryResult> resultUserList = this.handler.Handle(this.query).AsQueryable();

            //Assert
            resultUserList.Should().BeEmpty();
        }

        [Fact]
        public void Given_NullStringInQuery_When_Handling_Then_ShouldReturnEmptyList()
        {
            //Arrange
            IList<User> userList = this.CreateUserList(this.mockedDepartmentData);
            this.userRepository.Setup(x => x.Get()).Returns(userList.AsQueryable());

            this.query.PartialName = null;

            //Act
            IEnumerable<UsersQueryResult> resultUserList = this.handler.Handle(this.query).AsQueryable();

            //Assert
            resultUserList.Should().BeEmpty();
        }

        private IList<User> CreateUserList(Department userDepartment)
        {
            var userList = new List<User>
            {
                new User()
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000000"),
                    FirstName = "jon",
                    LastName = "smoth",
                    Department = userDepartment
                },
                new User()
                {
                    Id = new Guid("20000000-0000-0000-0000-000000000000"),
                    FirstName = "Andy",
                    LastName = "jon",
                    Department = userDepartment
                },
                new User()
                {
                    Id = new Guid("30000000-0000-0000-0000-000000000000"),
                    FirstName = "Andy",
                    LastName = "San",
                    Department = userDepartment
                }
            };
            return userList;
        }
    }
}