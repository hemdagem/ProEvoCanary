using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using AutoMapper;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Domain.Authentication;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Controllers;
using ProEvoCanary.Web.Models;
using PlayerModel = ProEvoCanary.Domain.Models.PlayerModel;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class EventControllerTests
    {
        readonly AddEventModel _eventModel = new AddEventModel(It.IsAny<TournamentType>(), It.IsAny<string>(), It.IsAny<DateTime>());
        private Mock<IEventRepository> _repo;
        private Mock<IResultRepository> _resultRepositoryMock;
        private Mock<IAppUser> _appUser;
        private EventController _eventController;
        private Mock<IMapper> _mapper;
        private Mock<IPlayerRepository> _mockPlayerRepository;

        public EventControllerTests()
        {
            _repo = new Mock<IEventRepository>();
            _mockPlayerRepository = new Mock<IPlayerRepository>();
            _appUser = new Mock<IAppUser>();
            _mapper = new Mock<IMapper>();
            _resultRepositoryMock = new Mock<IResultRepository>();
            _appUser.Setup(x => x.CurrentUser).Returns(new UserClaimsPrincipal(new ClaimsPrincipal()));

            _eventController = new EventController(_repo.Object, _appUser.Object, _mockPlayerRepository.Object, _mapper.Object, _resultRepositoryMock.Object);
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
            _repo.Setup(x => x.CreateEvent(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>())).Returns(1);

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
            _repo.Setup(x => x.GetEvent(It.IsAny<int>()))
                .Returns((Domain.Models.EventModel)null);

            //when + then
            Assert.Throws<NullReferenceException>(() => _eventController.GenerateFixtures(It.IsAny<int>()));
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

            _appUser.Setup(x => x.CurrentUser).Returns(new UserClaimsPrincipal(claimsPrincipal));

            var playerModels = new List<Web.Models.PlayerModel>()
            {
                new Web.Models.PlayerModel
                {
                    PlayerName = "Test",
                    PlayerId = 1
                }
            };
            var domainPlayerModels = new List<PlayerModel>()
            {
                new PlayerModel
                {
                    PlayerName = "Test",
                    PlayerId = 1
                }
            };
            _mockPlayerRepository.Setup(x => x.GetAllPlayers()).Returns(domainPlayerModels);

            _mapper.Setup(x => x.Map<List<Web.Models.PlayerModel>>(domainPlayerModels)).Returns(playerModels);

            var eventModel = new EventModel
            {
                Completed = true,
                Date = date,
                TournamentId = 10,
               TournamentType = TournamentType.Friendly,
                FixturesGenerated = true,
                TournamentName = "Test",
                OwnerId = 4
            };

            var domainEventModel = new Domain.Models.EventModel
            {
                Completed = true,
                Date = date,
                TournamentId = 10,
               TournamentType = Domain.Models.TournamentType.Friendly,
                FixturesGenerated = true,
                TournamentName = "Test",
                OwnerId = 4
            };
            _repo.Setup(x => x.GetEventForEdit(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(domainEventModel);

            _mapper.Setup(x => x.Map<EventModel>(domainEventModel)).Returns(eventModel);

            //when
            var viewResult = _eventController.GenerateFixtures(It.IsAny<int>()) as ViewResult;
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
