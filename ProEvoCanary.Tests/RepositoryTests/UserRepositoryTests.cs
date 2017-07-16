using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Repositories;
using ProEvoCanary.Tests.HelperTests;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Tests.RepositoryTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        readonly CreateUserModel _loginModel = new CreateUserModel("Hemang", "Rajyaguru", "Hemang", "test@test.com", "password");
        Mock<IDbHelper> _helper;
        Mock<IPasswordHash> _passwordHash;
        private UserRepository _repository;
        readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>
            {
                {"UserId", 1},
                {"Forename", "Hemang"},
                {"Surname", "Rajyaguru"},
                {"Username", "hemdagem"},
                {"UserType", 2}
            };
        readonly Dictionary<string, object> _adminDictionary = new Dictionary<string, object>
            {
                {"UserId", 1},
                {"Forename", "Hemang"},
                {"Surname", "Rajyaguru"},
                {"Username", "hemdagem"},
                {"UserType", 1}
            };

        private void Setup()
        {
            _helper = new Mock<IDbHelper>();
            _passwordHash = new Mock<IPasswordHash>();
            _repository = new UserRepository(_helper.Object, _passwordHash.Object);
        }

        [Test]
        public void ShouldGetAdminUser()
        {
            //given
            Setup();
            _passwordHash.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _helper.Setup(x => x.ExecuteReader("up_GetLoginDetails", It.IsAny<object>())).Returns(DataReaderTestHelper.Reader(_adminDictionary));

            //when
            var user = _repository.GetUser(It.IsAny<string>());

            //then
            Assert.That(user.UserId, Is.EqualTo(1));
            Assert.That(user.Forename, Is.EqualTo("Hemang"));
            Assert.That(user.Surname, Is.EqualTo("Rajyaguru"));
            Assert.That(user.Username, Is.EqualTo("hemdagem"));
            Assert.That(user.UserType, Is.EqualTo((int)UserType.Admin));
        }

        [Test]
        public void ShouldGetStandardUser()
        {
            //given
            Setup();

            _helper.Setup(x => x.ExecuteReader("up_GetLoginDetails", It.IsAny<object>())).Returns(DataReaderTestHelper.Reader(_dictionary));

            //when
            var user = _repository.GetUser(It.IsAny<string>());

            //then
            Assert.That(user, Is.Not.Null);
            Assert.That(user.UserId, Is.EqualTo(1));
            Assert.That(user.Forename, Is.EqualTo("Hemang"));
            Assert.That(user.Surname, Is.EqualTo("Rajyaguru"));
            Assert.That(user.Username, Is.EqualTo("hemdagem"));
            Assert.That(user.UserType, Is.EqualTo((int)UserType.Standard));
        }

        [Test]
        public void ShouldCreateUser()
        {
            Setup();
            _helper.Setup(x => x.ExecuteScalar("up_AddUser", It.IsAny<object>())).Returns(1);

            //when
            var user = _repository.CreateUser(_loginModel.Username, _loginModel.Forename, _loginModel.Surname, _loginModel.EmailAddress, _loginModel.Password);

            //then
            Assert.That(user, Is.EqualTo(1));
        }
    }
}

