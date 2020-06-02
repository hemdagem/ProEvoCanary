using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Domain.Authentication;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Controllers;
using LoginModel = ProEvoCanary.Web.Models.LoginModel;
using UserModel = ProEvoCanary.Domain.Models.UserModel;
using UserType = ProEvoCanary.Domain.Authentication.UserType;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        private Mock<IUserRepository> _repo;
        private Mock<IAuthenticationHandler> _authenticationMock;

        public AuthenticationControllerTests()
        {
            _repo = new Mock<IUserRepository>();
            _authenticationMock = new Mock<IAuthenticationHandler>();
        }

        [Test]
        public void ShouldSetDefaultViewNameToLoginForLoginPage()
        {
            //given
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
            _repo.Setup(x => x.Login(It.IsAny<Domain.Models.LoginModel>())).Returns(new UserModel(1, "test", "test", "test", (int)UserType.Standard));
            var authenticationController = new AuthenticationController(_repo.Object, _authenticationMock.Object);

            //when
            authenticationController.Login(new LoginModel(string.Empty,string.Empty), It.IsAny<string>());

            //then
            _repo.Verify(x => x.Login(It.IsAny<Domain.Models.LoginModel>()), Times.Once);
        }
    }
}
