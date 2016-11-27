using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.AdminWeb.Controllers;
using ProEvoCanary.AdminWeb.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.AdminWeb.UnitTests
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        readonly CreateUserModel _loginModel = new CreateUserModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>());

        [Test]
        public void ShouldSetDefaultViewName()
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
            authenticationController.Create(_loginModel);

            //then
            repo.Verify(x => x.CreateUser(_loginModel.Username, _loginModel.Forename, _loginModel.Surname, _loginModel.EmailAddress,_loginModel.Password), Times.Once);
        }

        [Test]
        public void ShouldNotCallUserRepositoryWhenModelIsInValid()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var authenticationController = new AuthenticationController(repo.Object);
            //when
            authenticationController.ModelState.AddModelError("forename", "Missing forename");
            authenticationController.Create(_loginModel);

            //then
            repo.Verify(x => x.CreateUser(_loginModel.Username, _loginModel.Forename, _loginModel.Surname, _loginModel.EmailAddress, _loginModel.Password), Times.Never);
        }

        [Test]
        public void ShouldNotRedirectToHomePageOnFailedModelEntry()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var authenticationController = new AuthenticationController(repo.Object);
            //when
            authenticationController.ModelState.AddModelError("forename", "Missing forename");
            var redirectToRouteResult = authenticationController.Create(_loginModel) as RedirectToRouteResult;
            var view = authenticationController.Create(_loginModel) as ViewResult;

            //then
            Assert.IsNull(redirectToRouteResult);
            Assert.IsInstanceOf<CreateUserModel>(view.Model);
        }

        [Test]
        public void ShouldNotRedirectToHomePageWhenCreateUserFailed()
        {
            //given
            var repo = new Mock<IUserRepository>();

            repo.Setup(x => x.CreateUser(_loginModel.Username, _loginModel.Forename, _loginModel.Surname, _loginModel.EmailAddress, _loginModel.Password)).Returns(0);
            var authenticationController = new AuthenticationController(repo.Object);

            //when
            var redirectToRouteResult = authenticationController.Create(_loginModel) as RedirectToRouteResult;
            var view = authenticationController.Create(_loginModel) as ViewResult;

            //then
            Assert.IsNull(redirectToRouteResult);
            Assert.IsInstanceOf<CreateUserModel>(view.Model);
        }
    }
}
