using System;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Controllers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class EventControllerTests
    {
        readonly AddEventModel _eventModel = new AddEventModel(It.IsAny<EventTypes>(), It.IsAny<string>(), It.IsAny<DateTime>());

        [Test]
        public void ShouldSetDefaultViewName()
        {
            //given
            var repo = new Mock<IAdminEventRepository>();
            var authenticationController = new EventController(repo.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then

            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void ShouldSetTournamentDateToToday()
        {
            //given
            var repo = new Mock<IAdminEventRepository>(); 
            var authenticationController = new EventController(repo.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then
            Assert.That(((AddEventModel)viewResult.Model).Date, Is.EqualTo(DateTime.Today));
        }


        [Test]
        public void ShouldSetDefaultModel()
        {
            //given
            var repo = new Mock<IAdminEventRepository>();

            var authenticationController = new EventController(repo.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then

            Assert.That(viewResult.Model, Is.TypeOf<AddEventModel>());
        }

        [Test]
        public void ShouldSetDefaultModelProperties()
        {
            //given
            var repo = new Mock<IAdminEventRepository>();
            var userrepo = new Mock<IPlayerRepository>();

            var authenticationController = new EventController(repo.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then

            Assert.That(viewResult.Model, Is.TypeOf<AddEventModel>());
        }

        [Test]
        public void ShouldNotCallUserRepositoryWhenModelIsInValid()
        {
            //given
            var repo = new Mock<IAdminEventRepository>();
            var authenticationController = new EventController(repo.Object);

            //when
            authenticationController.ModelState.AddModelError("TournamentName", "Missing Tournament Name");
            authenticationController.Create(_eventModel);

            //then
            repo.Verify(x => x.CreateEvent(_eventModel.TournamentName, _eventModel.Date, _eventModel.EventType, It.IsAny<int>()), Times.Never);


        }

    }
}
