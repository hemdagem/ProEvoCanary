using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Areas.Admin.Models;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.Tests.RepositoryTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        readonly LoginModel _loginModel = new LoginModel("Hemang", "Rajyaguru", "Hemang", "test@test.com");


        [Test]
        public void ShouldGetAdminUser()
        {
            //given
            var dictionary = new Dictionary<string, object>
            {
                {"UserId", 1},
                {"Forename", "Hemang"},
                {"Surname", "Rajyaguru"},
                {"Username", "hemdagem"},
                {"UserType", 1}
            };

            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(DataReaderTestHelper.Reader(dictionary));

            var repository = new UserRepository(helper.Object);

            //when
            var user = (AdminModel)repository.GetUser(It.IsAny<string>());

            //then
            Assert.That(user, Is.TypeOf(typeof(AdminModel)));
            Assert.That(user.UserId, Is.EqualTo(1));
            Assert.That(user.Forename, Is.EqualTo("Hemang"));
            Assert.That(user.Surname, Is.EqualTo("Rajyaguru"));
            Assert.That(user.Username, Is.EqualTo("hemdagem"));

        }


        [Test]
        public void ShouldGetStandardUser()
        {

            //given
            var dictionary = new Dictionary<string, object>
            {
                {"UserId", 1},
                {"Forename", "Hemang"},
                {"Surname", "Rajyaguru"},
                {"Username", "hemdagem"},
                {"UserType", 2}
            };

            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(DataReaderTestHelper.Reader(dictionary));

            var repository = new UserRepository(helper.Object);

            //when
            var user = (UserModel)repository.GetUser(It.IsAny<string>());

            //then
            Assert.That(user, Is.Not.Null);
            Assert.That(user.UserId, Is.EqualTo(1));
            Assert.That(user.Forename, Is.EqualTo("Hemang"));
            Assert.That(user.Surname, Is.EqualTo("Rajyaguru"));
            Assert.That(user.Username, Is.EqualTo("hemdagem"));

        }

        [Test]
        public void ShouldCreateUser()
        {
            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(1);

            var repository = new UserRepository(helper.Object);

            //when
            var user = repository.CreateUser(_loginModel.Username, _loginModel.Forename, _loginModel.Surname, _loginModel.EmailAddress);

            //then
            Assert.That(user, Is.EqualTo(1));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldThrowExceptionIfUsernameIsEmptyWhenCreatingAUser(string username)
        {
            //given
            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(0);

            var repository = new UserRepository(helper.Object);

            //then
            repository.CreateUser(username, _loginModel.Forename, _loginModel.Surname, _loginModel.EmailAddress);

        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldThrowExceptionIfForenameIsEmptyWhenCreatingAUser(string forename)
        {
            //given
            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(0);

            var repository = new UserRepository(helper.Object);

            //then
            repository.CreateUser(_loginModel.Username, forename, _loginModel.Surname, _loginModel.EmailAddress);

        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldThrowExceptionIfSurnameIsEmptyWhenCreatingAUser(string surname)
        {
            //given
            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(0);

            var repository = new UserRepository(helper.Object);

            //then
            repository.CreateUser(_loginModel.Username, _loginModel.Forename, surname, _loginModel.EmailAddress);

        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldThrowExceptionIfEmailIsEmptyWhenCreatingAUser(string email)
        {
            //given
            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(0);

            var repository = new UserRepository(helper.Object);

            //then
            repository.CreateUser(_loginModel.Username, _loginModel.Forename, _loginModel.Surname, email);

        }
    }
}

