using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Areas.Admin.Controllers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;
using EventModel = ProEvoCanary.Areas.Admin.Models.EventModel;

namespace ProEvoCanary.Tests.ControllerTests.Admin
{
    [TestFixture]
    public class EventControllerTests
    {
        readonly EventModel _eventModel =new EventModel(It.IsAny<EventTypes>(),It.IsAny<string>(),It.IsAny<DateTime>(),It.IsAny<List<PlayerModel>>());

        [Test]
        public void ShouldSetDefaultViewName()
        {
            //given
            var repo = new Mock<IAdminEventRepository>();
            var userrepo = new Mock<IPlayerRepository>();
            var authenticationController = new EventController(repo.Object, userrepo.Object);

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
            var userrepo = new Mock<IPlayerRepository>();
            var authenticationController = new EventController(repo.Object, userrepo.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then
            Assert.That(((EventModel)viewResult.Model).Date, Is.EqualTo(DateTime.Today));
        }        

        
        [Test]
        public void ShouldSetDefaultModel()
        {
            //given
            var repo = new Mock<IAdminEventRepository>();
            var userrepo = new Mock<IPlayerRepository>();

            var authenticationController = new EventController(repo.Object, userrepo.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then

            Assert.That(viewResult.Model, Is.TypeOf<EventModel>());
        }
        
        [Test]
        public void ShouldSetDefaultModelProperties()
        {
            //given
            var repo = new Mock<IAdminEventRepository>();
            var userrepo = new Mock<IPlayerRepository>();
            var players = new List<PlayerModel>();
            userrepo.Setup(x => x.GetAllPlayers()).Returns(players);

            var authenticationController = new EventController(repo.Object, userrepo.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then
            var model = viewResult.Model as EventModel;
         
            Assert.That(viewResult.Model, Is.TypeOf<EventModel>());
            Assert.That(model.UserSelectListModel, Is.EqualTo(players));
        }

        [Test]
        public void ShouldNotCallUserRepositoryWhenModelIsInValid()
        {
            //given
            var repo = new Mock<IAdminEventRepository>();
            var userrepo = new Mock<IPlayerRepository>();
            var authenticationController = new EventController(repo.Object, userrepo.Object);

            //when
            authenticationController.ModelState.AddModelError("TournamentName", "Missing Tournament Name");
            authenticationController.Create(_eventModel);

            //then
            repo.Verify(x => x.CreateEvent(_eventModel.TournamentName, _eventModel.Date, _eventModel.EventType, It.IsAny<int>()), Times.Never);


        }
        [Test]
        public void SelectListModelShouldBeRepopulatedIfModelPostIsInvalid()
        {
            //given
            var repo = new Mock<IAdminEventRepository>();
            var userrepo = new Mock<IPlayerRepository>();

            userrepo.Setup(x => x.GetAllPlayers()).Returns(new List<PlayerModel>());

            var authenticationController = new EventController(repo.Object, userrepo.Object);

            //when
            authenticationController.ModelState.AddModelError("TournamentName", "Missing Tournament Name");
            var viewResult = authenticationController.Create(_eventModel) as ViewResult;

            //then
            Assert.IsNotNull(((EventModel)viewResult.Model).UserSelectListModel);


        }




    }
}
