using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Moq;
using NUnit.Framework;
using ProEvoCanary.AdminWeb.Controllers;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;
using EventModel = ProEvoCanary.AdminWeb.Models.EventModel;
using PlayerModel = ProEvoCanary.AdminWeb.Models.PlayerModel;

namespace ProEvoCanary.AdminWeb.UnitTests
{
    [TestFixture]
    public class EventControllerTests
    {
        readonly EventModel _eventModel = new EventModel(It.IsAny<TournamentType>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<List<PlayerModel>>());
        Mock<IEventRepository> _adminEventRepo;
        Mock<IPlayerRepository> _playerRepositoryMock;
        Mock<IMapper> _mapper;
        private EventController _eventController;

        [SetUp]
        public void Setup()
        {
            _adminEventRepo = new Mock<IEventRepository>();
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _mapper = new Mock<IMapper>();
            _eventController = new EventController(_adminEventRepo.Object, _playerRepositoryMock.Object, _mapper.Object);
        }

        [Test]
        public void ShouldSetDefaultViewName()
        {
            //given + when
            var viewResult = _eventController.Create() as ViewResult;

            //then
            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void ShouldSetTournamentDateToToday()
        {
            //given + when
            var viewResult = _eventController.Create() as ViewResult;

            //then
            Assert.That(((EventModel)viewResult.Model).Date, Is.EqualTo(DateTime.Today));
        }


        [Test]
        public void ShouldSetDefaultModel()
        {
            //given + when
            var viewResult = _eventController.Create() as ViewResult;

            //then
            Assert.That(viewResult.Model, Is.TypeOf<EventModel>());
        }

        [Test]
        public void ShouldSetDefaultModelProperties()
        {
            //given
            var players = new List<Domain.Models.PlayerModel>();
            _playerRepositoryMock.Setup(x => x.GetAllPlayers()).Returns(new List<Domain.Models.PlayerModel>());
            _mapper.Setup(x => x.Map<List<PlayerModel>>(new List<Domain.Models.PlayerModel>())).Returns(new List<PlayerModel>());
            //when
            var viewResult = _eventController.Create() as ViewResult;

            //then
            var model = viewResult.Model as EventModel;

            Assert.That(viewResult.Model, Is.TypeOf<EventModel>());
            Assert.That(model.Players, Is.EqualTo(players));
        }

        [Test]
        public void ShouldNotCallUserRepositoryWhenModelIsInValid()
        {
            //given + when
            _eventController.ModelState.AddModelError("TournamentName", "Missing Tournament Name");
            _eventController.Create(_eventModel);

            //then
            _adminEventRepo.Verify(x => x.CreateEvent(_eventModel.TournamentName, _eventModel.Date, (int)_eventModel.TournamentType, It.IsAny<int>()), Times.Never);
        }
        [Test]
        public void ListModelShouldBeRepopulatedIfModelPostIsInvalid()
        {
            //given
            _playerRepositoryMock.Setup(x => x.GetAllPlayers()).Returns(new List<Domain.Models.PlayerModel>());

            _mapper.Setup(x => x.Map<List<PlayerModel>>(new List<Domain.Models.PlayerModel>())).Returns(new List<PlayerModel>());
            //when
            _eventController.ModelState.AddModelError("TournamentName", "Missing Tournament Name");
            var viewResult = _eventController.Create(_eventModel) as ViewResult;

            //then
            Assert.IsNotNull(((EventModel)viewResult.Model).Players);
        }
    }
}
