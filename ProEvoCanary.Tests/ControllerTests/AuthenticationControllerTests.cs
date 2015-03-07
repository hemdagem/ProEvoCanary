using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Controllers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;
using LoginModel = ProEvoCanary.Models.LoginModel;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        readonly CreateUserModel _createUserModel = new CreateUserModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
        readonly LoginModel _loginModel = new LoginModel(It.IsAny<string>(), It.IsAny<string>());
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
            authenticationController.Login(_loginModel, It.IsAny<string>());

            //then
            _repo.Verify(x => x.Login(_loginModel), Times.Once);
        }

        [Test]
        public void ShouldNotCallSignInMethodWhenLoginIsInvalid()
        {
            //given
            Setup();
            _repo.Setup(x => x.Login(_loginModel)).Returns((UserModel)null);
            _authenticationMock.Setup(x => x.SignIn(It.IsAny<UserModel>()));
            var authenticationController = new AuthenticationController(_repo.Object, _authenticationMock.Object);
            
            //when
            authenticationController.Login(_loginModel, It.IsAny<string>());

            //then
            _authenticationMock.Verify(x => x.SignIn(It.IsAny<UserModel>()), Times.Never);
        }

        [Test]
        public void ShouldNotCallUserRepositoryWhenModelIsInValid()
        {
            //given
            Setup();
            var authenticationController = new AuthenticationController(_repo.Object, _authenticationMock.Object);
            
            //when
            authenticationController.ModelState.AddModelError("forename", "Missing forename");
            authenticationController.Create(_createUserModel);

            //then
            _repo.Verify(x => x.CreateUser(_createUserModel.Username, _createUserModel.Forename, _createUserModel.Surname, _createUserModel.EmailAddress,_createUserModel.Password), Times.Never);
        }  
        


        [Test]
        public void ShouldNotRedirectToHomePageOnFailedModelEntry()
        {
            //given
            Setup();
            var authenticationController = new AuthenticationController(_repo.Object, _authenticationMock.Object);
            
            //when
            authenticationController.ModelState.AddModelError("forename", "Missing forename");
            var redirectToRouteResult = authenticationController.Create(_createUserModel) as RedirectToRouteResult;
            var view = authenticationController.Create(_createUserModel) as ViewResult;

            //then
            Assert.IsNull(redirectToRouteResult);
            Assert.IsInstanceOf<CreateUserModel>(view.Model);
        }

        [Test]
        public void ShouldNotRedirectToHomePageWhenCreateUserFailed()
        {
            //given
            Setup();
            _repo.Setup(x => x.CreateUser(_createUserModel.Username, _createUserModel.Forename, _createUserModel.Surname, _createUserModel.EmailAddress, _createUserModel.Password)).Returns(0);
            var authenticationController = new AuthenticationController(_repo.Object, _authenticationMock.Object);

            //when
            var redirectToRouteResult = authenticationController.Create(_createUserModel) as RedirectToRouteResult;
            var view = authenticationController.Create(_createUserModel) as ViewResult;

            //then
            Assert.IsNull(redirectToRouteResult);
            Assert.IsInstanceOf<CreateUserModel>(view.Model);
        }
    }
}
