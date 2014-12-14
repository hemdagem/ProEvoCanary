using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Controllers;
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


        [Test]
        public void ShouldSetDefaultViewNameToLoginForLoginPage()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var authenticationController = new AuthenticationController(repo.Object);

            //when
            var viewResult = authenticationController.Login() as ViewResult;

            //then

            Assert.That(viewResult.ViewName, Is.EqualTo("Login"));
        }
        [Test]
        public void ShouldSetDefaultViewNameToCreateForCreatePage()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var authenticationController = new AuthenticationController(repo.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then

            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void ShouldCallUserRepositoryWhenModelIsValid()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var authenticationController = new AuthenticationController(repo.Object);
            //when
            authenticationController.Login(_loginModel, It.IsAny<string>());

            //then
            repo.Verify(x => x.Login(_loginModel), Times.Once);


        }

        [Test]
        public void ShouldNotCallUserRepositoryWhenModelIsInValid()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var authenticationController = new AuthenticationController(repo.Object);
            //when
            authenticationController.ModelState.AddModelError("forename", "Missing forename");
            authenticationController.Create(_createUserModel);

            //then
            repo.Verify(x => x.CreateUser(_createUserModel.Username, _createUserModel.Forename, _createUserModel.Surname, _createUserModel.EmailAddress,_createUserModel.Password), Times.Never);


        }

        [Test]
        public void ShouldNotRedirectToHomePageOnFailedModelEntry()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var authenticationController = new AuthenticationController(repo.Object);
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
            var repo = new Mock<IUserRepository>();

            repo.Setup(x => x.CreateUser(_createUserModel.Username, _createUserModel.Forename, _createUserModel.Surname, _createUserModel.EmailAddress, _createUserModel.Password)).Returns(0);
            var authenticationController = new AuthenticationController(repo.Object);

            //when
            var redirectToRouteResult = authenticationController.Create(_createUserModel) as RedirectToRouteResult;
            var view = authenticationController.Create(_createUserModel) as ViewResult;

            //then
            Assert.IsNull(redirectToRouteResult);
            Assert.IsInstanceOf<CreateUserModel>(view.Model);
        }
    }
}
