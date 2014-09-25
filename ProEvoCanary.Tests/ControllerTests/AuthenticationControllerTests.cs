using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Areas.Admin.Controllers;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.ViewModels;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        readonly LoginModel _loginModel = new LoginModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());


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
            repo.Verify(x => x.CreateUser(_loginModel.Username, _loginModel.Forename, _loginModel.Surname, _loginModel.EmailAddress), Times.Once);


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
            repo.Verify(x => x.CreateUser(_loginModel.Username, _loginModel.Forename, _loginModel.Surname, _loginModel.EmailAddress), Times.Never);


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
            Assert.IsInstanceOf<LoginModel>(view.Model);

        }

        [Test]
        public void ShouldNotRedirectToHomePageWhenCreateUserFailed()
        {
            //given
            var repo = new Mock<IUserRepository>();

            repo.Setup(x => x.CreateUser(_loginModel.Username, _loginModel.Forename, _loginModel.Surname, _loginModel.EmailAddress)).Returns(0);
            var authenticationController = new AuthenticationController(repo.Object);

            //when
            var redirectToRouteResult = authenticationController.Create(_loginModel) as RedirectToRouteResult;
            var view = authenticationController.Create(_loginModel) as ViewResult;

            //then
            Assert.IsNull(redirectToRouteResult);
            Assert.IsInstanceOf<LoginModel>(view.Model);
        }
    }
}
