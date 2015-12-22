using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Authentication;
using ProEvoCanary.Controllers;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Models;
using LoginModel = ProEvoCanary.Models.LoginModel;
using UserModel = ProEvoCanary.Domain.Models.UserModel;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        readonly Domain.Models.LoginModel _loginModel = new Domain.Models.LoginModel(It.IsAny<string>(), It.IsAny<string>());
        private Mock<IUserRepository> _repo;
        private Mock<IAuthenticationHandler> _authenticationMock;

        private void Setup()
        {
            _repo = new Mock<IUserRepository>();
            _authenticationMock = new Mock<IAuthenticationHandler>();
        }

        [Test]
        public void ShouldSetDefaultViewNameToLoginForLoginPage()
        {
            //given
            Setup();
            var authenticationController = new AuthenticationController(_repo.Object, _authenticationMock.Object);

            //when
            var viewResult = authenticationController.Login(It.IsAny<string>()) as ViewResult;

            //then
            Assert.That(viewResult.ViewName, Is.EqualTo("Login"));
        }
        [Test]
        public void ShouldSetDefaultViewNameToCreateForCreatePage()
        {
            //given
            Setup();
            var authenticationController = new AuthenticationController(_repo.Object, _authenticationMock.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then
            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void ShouldCallUserRepositoryWhenModelIsValid()
        {
            //given
            Setup();
            _repo.Setup(x => x.Login(_loginModel)).Returns(new UserModel(1, "test", "test", "test", (int)UserType.Standard));
            var authenticationController = new AuthenticationController(_repo.Object, _authenticationMock.Object);

            //when
            authenticationController.Login(new LoginModel(), It.IsAny<string>());

            //then
            _repo.Verify(x => x.Login(_loginModel), Times.Once);
        }
    }
}
