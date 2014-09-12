using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Controllers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;
using ProEvoCanary.ViewModels;

namespace ProEvoCanary.ControllerTests
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        readonly LoginModel loginModel = new LoginModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());


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
            var model = loginModel;
            var authenticationController = new AuthenticationController(repo.Object);
            //when
            authenticationController.Create(model);

            //then
            repo.Verify(x => x.CreateUser(model.Username, model.Forename, model.Surname, model.EmailAddress), Times.Once);


        }

        [Test]
        public void ShouldNotCallUserRepositoryWhenModelIsInValid()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var model = loginModel;
            var authenticationController = new AuthenticationController(repo.Object);
            //when
            authenticationController.ModelState.AddModelError("forename", "Missing forename");
            authenticationController.Create(model);

            //then
            repo.Verify(x => x.CreateUser(model.Username, model.Forename, model.Surname, model.EmailAddress), Times.Never);


        }




        [Test]
        public void ShouldNotRedirectToHomePageOnFailedModelEntry()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var model = loginModel;

            var authenticationController = new AuthenticationController(repo.Object);
            //when
            authenticationController.ModelState.AddModelError("forename", "Missing forename");
            var redirectToRouteResult = authenticationController.Create(model) as RedirectToRouteResult;
            var view = authenticationController.Create(model) as ViewResult;

            //then
            Assert.IsNull(redirectToRouteResult);
            Assert.IsInstanceOf<LoginModel>(view.Model);

        }

        [Test]
        public void ShouldNotRedirectToHomePageWhenCreateUserFailed()
        {
            //given
            var repo = new Mock<IUserRepository>();
            var model = loginModel;

            repo.Setup(x => x.CreateUser(model.Username, model.Forename, model.Surname, model.EmailAddress)).Returns(0);
            var authenticationController = new AuthenticationController(repo.Object);

            //when
            var redirectToRouteResult = authenticationController.Create(model) as RedirectToRouteResult;
            var view = authenticationController.Create(model) as ViewResult;

            //then
            Assert.IsNull(redirectToRouteResult);
            Assert.IsInstanceOf<LoginModel>(view.Model);
        }
    }
}
