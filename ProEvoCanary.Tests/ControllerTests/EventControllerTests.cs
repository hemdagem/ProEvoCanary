using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.DataAccess.Repositories;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.EventHandlers.Configuration;
using ProEvoCanary.Domain.EventHandlers.Events.AddEvent;
using ProEvoCanary.Domain.EventHandlers.Events.GenerateFixturesForEvent;
using ProEvoCanary.Domain.EventHandlers.Events.GetEvent;
using ProEvoCanary.Domain.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Web.Controllers;
using ProEvoCanary.Web.Models;
using EventModel = ProEvoCanary.Web.Models.EventModel;
using PlayerModel = ProEvoCanary.Domain.Models.PlayerModel;
using TournamentType = ProEvoCanary.Web.Models.TournamentType;

namespace ProEvoCanary.UnitTests.ControllerTests
{
    [TestFixture]
    public class EventControllerTests
    {
        readonly AddEventModel _eventModel = new AddEventModel(It.IsAny<TournamentType>(), It.IsAny<string>(), It.IsAny<DateTime>());
        private Mock<IEventReadRepository> _eventReadRepositoryMock;
        private Mock<IEventWriteRepository> _eventWriteRepositoryMock;
        private EventController _eventController;
        private Mock<IMapper> _mapper;
        private Mock<IQuery<List<PlayerModelDto>>> _mockPlayerRepository;
        private Mock<IQueryHandler<GetEvent, EventModelDto>> eventQueryHandlerBaseMock;
        private Mock<ICommandHandler<AddEventCommand, Guid>> eventCommandHandlerBaseMock;
        private readonly Mock<ICommandHandler<GenerateFixturesForEventCommand, Guid>> _geneQueryHandlerBase;


        public EventControllerTests()
        {
            _eventReadRepositoryMock = new Mock<IEventReadRepository>();
            _eventWriteRepositoryMock = new Mock<IEventWriteRepository>();
            _mockPlayerRepository = new Mock<IQuery<List<PlayerModelDto>>>();
            _mapper = new Mock<IMapper>();
            eventQueryHandlerBaseMock = new Mock<IQueryHandler<GetEvent, EventModelDto>>();
            _geneQueryHandlerBase = new Mock<ICommandHandler<GenerateFixturesForEventCommand, Guid>>();

            _eventController = new EventController(_mockPlayerRepository.Object, _mapper.Object, eventQueryHandlerBaseMock.Object, eventCommandHandlerBaseMock.Object, _geneQueryHandlerBase.Object);
        }

        [Test]
        public void ShouldAddDefaultSettingsForCreateAction()
        {
            //given + when
            var viewResult = _eventController.Create() as ViewResult;

            //then
            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
            Assert.That(((AddEventModel)viewResult.Model).Date, Is.EqualTo(DateTime.Today));
            Assert.That(viewResult.Model, Is.TypeOf<AddEventModel>());
        }

        [Test]
        public void ShouldRedirectToGenerateFixturesAction()
        {
            //given
            _eventWriteRepositoryMock.Setup(x => x.CreateEvent(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(1);

            //when
            var viewResult = _eventController.Create(_eventModel) as RedirectToRouteResult;
            var actionRoute = viewResult.RouteValues["action"];
            var actionController = viewResult.RouteValues["controller"];

            //then
            Assert.AreEqual("GenerateFixtures", actionRoute);
            Assert.AreEqual("Event", actionController);
        }


        [Test]
        public void ShouldReturnExceptionWhenEventDoesntExist()
        {
            //given
            _eventReadRepositoryMock.Setup(x => x.GetEvent(It.IsAny<Guid>()))
                .Returns((DataAccess.Models.EventModel)null);

            //when + then
            Assert.Throws<NullReferenceException>(() => _eventController.GenerateFixtures(It.IsAny<Guid>()));
        }

        [Test]
        public void ShouldSetModelPropertiesForGenerateFixturesModel()
        {
            //given
            var date = DateTime.Now.ToString();

            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,"4")
            }));


            var playerModels = new List<Web.Models.PlayerModel>()
            {
                new Web.Models.PlayerModel
                {
                    PlayerName = "Test",
                    PlayerId = 1
                }
            };
            var domainPlayerModels = new List<PlayerModelDto>()
            {
                new PlayerModelDto
                {
                    PlayerName = "Test",
                    PlayerId = 1
                }
            };
            _mockPlayerRepository.Setup(x => x.Handle()).Returns(domainPlayerModels);

            _mapper.Setup(x => x.Map<List<Web.Models.PlayerModel>>(domainPlayerModels)).Returns(playerModels);

            var eventModel = new EventModel
            {
                Completed = true,
                Date = date,
                TournamentId = Guid.NewGuid(),
               TournamentType = TournamentType.Friendly,
                FixturesGenerated = true,
                TournamentName = "Test",
                OwnerId = 4
            };

            var domainEventModel = new DataAccess.Models.EventModel
            {
                Completed = true,
                Date = date,
                TournamentId = Guid.NewGuid(),
               TournamentType = DataAccess.Models.TournamentType.Friendly,
                FixturesGenerated = true,
                TournamentName = "Test",
                OwnerId = 4
            };
            _eventReadRepositoryMock.Setup(x => x.GetEvent(It.IsAny<Guid>()))
                .Returns(domainEventModel);

            _mapper.Setup(x => x.Map<EventModel>(domainEventModel)).Returns(eventModel);

            //when
            var viewResult = _eventController.GenerateFixtures(It.IsAny<Guid>()) as ViewResult;
            var model = viewResult.Model as EventModel;
            //then
            Assert.That(viewResult.ViewName, Is.EqualTo("GenerateFixtures"));
            Assert.AreEqual(model.Completed, true);
            Assert.AreEqual(model.FixturesGenerated, true);
            Assert.AreEqual(model.TournamentName, "Test");
            Assert.AreEqual(model.OwnerId, 4);
            Assert.AreEqual(model.Date, date);
            Assert.AreEqual(model.TournamentId, 10);
            Assert.AreEqual(1, model.Users.Count);
            Assert.AreEqual(model.TournamentType, TournamentType.Friendly);
        }
    }
}
