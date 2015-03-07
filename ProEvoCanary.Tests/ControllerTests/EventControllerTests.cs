using System;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Controllers;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class EventControllerTests
    {
        readonly AddEventModel _eventModel = new AddEventModel(It.IsAny<EventTypes>(), It.IsAny<string>(), It.IsAny<DateTime>());
        private Mock<IAdminEventRepository> _repo;
        private Mock<IAppUser> _appUser;
        private EventController _eventController;

        private void Setup()
        {
            _repo = new Mock<IAdminEventRepository>();
            _appUser = new Mock<IAppUser>();
            _appUser.Setup(x => x.CurrentUser).Returns(new UserClaimsPrincipal(new ClaimsPrincipal()));
            _eventController = new EventController(_repo.Object, _appUser.Object);
        }

        [Test]
        public void ShouldSetDefaultViewName()
        {
            //given
            Setup();

            //when
            var viewResult = _eventController.Create() as ViewResult;

            //then
            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void ShouldSetTournamentDateToToday()
        {
            //given
            Setup();

            //when
            var viewResult = _eventController.Create() as ViewResult;

            //then
            Assert.That(((AddEventModel)viewResult.Model).Date, Is.EqualTo(DateTime.Today));
        }


        [Test]
        public void ShouldSetDefaultModel()
        {
            //given
            Setup();

            //when
            var viewResult = _eventController.Create() as ViewResult;

            //then
            Assert.That(viewResult.Model, Is.TypeOf<AddEventModel>());
        }

        [Test]
        public void ShouldSetDefaultModelProperties()
        {
            //given
            Setup();

            //when
            var viewResult = _eventController.Create() as ViewResult;

            //then
            Assert.That(viewResult.Model, Is.TypeOf<AddEventModel>());
        }

        [Test]
        public void ShouldNotCallUserRepositoryWhenModelIsInValid()
        {
            //given
            Setup();

            //when
            _eventController.ModelState.AddModelError("TournamentName", "Missing Tournament Name");
            _eventController.Create(_eventModel);

            //then
            _repo.Verify(x => x.CreateEvent(_eventModel.TournamentName, _eventModel.Date, _eventModel.EventType, It.IsAny<int>()), Times.Never);

        }

        [Test]
        public void ShouldRedirectToGenerateFixturesAction()
        {
            //given
            Setup();
            _repo.Setup(x => x.CreateEvent(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<EventTypes>(), It.IsAny<int>())).Returns(1);

            //when
            var viewResult = _eventController.Create(_eventModel) as RedirectToRouteResult;
            var actionRoute = viewResult.RouteValues["action"];
            var actionController = viewResult.RouteValues["controller"];

            //then
            Assert.AreEqual("GenerateFixtures", actionRoute);
            Assert.AreEqual("Event", actionController);
        }

    }
}
