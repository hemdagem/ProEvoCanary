using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;
using ProEvoCanary.ViewModels;

namespace ProEvoCanary.RepositoryTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        readonly LoginModel loginModel = new LoginModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());


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

            var helper = new Mock<IdBHelper>();
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

            var helper = new Mock<IdBHelper>();
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
            var helper = new Mock<IdBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(1);

            var repository = new UserRepository(helper.Object);

            //when
            var user = repository.CreateUser(loginModel.Username, loginModel.Forename, loginModel.Surname, loginModel.EmailAddress);

            //then
            Assert.That(user, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNotCreateUser()
        {
            var helper = new Mock<IdBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(0);

            var repository = new UserRepository(helper.Object);

            //when
            var user = repository.CreateUser(loginModel.Username, loginModel.Forename, loginModel.Surname, loginModel.EmailAddress);

            //then
            Assert.That(user, Is.EqualTo(0));
        }
    }
}

