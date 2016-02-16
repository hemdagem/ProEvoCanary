using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using AutoMapper;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Controllers;
using ProEvoCanary.Domain.Authentication;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Models;
using PlayerModel = ProEvoCanary.Domain.Models.PlayerModel;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class EventControllerTests
    {
        readonly AddEventModel _eventModel = new AddEventModel(It.IsAny<EventTypes>(), It.IsAny<string>(), It.IsAny<DateTime>());
        private Mock<IAdminEventRepository> _repo;
        private Mock<IAppUser> _appUser;
        private EventController _eventController;
        private Mock<IMapper> _mapper;
        private Mock<IPlayerRepository> _mockPlayerRepository;

        private void Setup()
        {
            _repo = new Mock<IAdminEventRepository>();
            _mockPlayerRepository = new Mock<IPlayerRepository>();
            _appUser = new Mock<IAppUser>();
            _mapper = new Mock<IMapper>();
            _appUser.Setup(x => x.CurrentUser).Returns(new UserClaimsPrincipal(new ClaimsPrincipal()));

            _eventController = new EventController(_repo.Object, _appUser.Object, _mockPlayerRepository.Object, _mapper.Object);

        }

        [Test]
        public void ShouldAddDefaultSettingsForCreateAction()
        {
            //given
            Setup();

            //when
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
            Setup();
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
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldReturnExceptionWhenEventDoesntExist()
        {
            //given
            Setup();

            _repo.Setup(x => x.GetEvent(It.IsAny<int>()))
                .Returns((Domain.Models.EventModel)null);

            //when + then
            _eventController.GenerateFixtures(It.IsAny<int>());
        }

        [Test]
        public void ShouldSetModelPropertiesForGenerateFixturesModel()
        {
            //given
            Setup();

            var date = DateTime.Now.ToString();

            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,"4")
            }));

            _appUser.Setup(x => x.CurrentUser).Returns(new UserClaimsPrincipal(claimsPrincipal));

            var playerModels = new List<Models.PlayerModel>()
            {
                new Models.PlayerModel
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

            _mapper.Setup(x => x.Map<List<Models.PlayerModel>>(domainPlayerModels)).Returns(playerModels);

            var eventModel = new Models.EventModel
            {
                Completed = true,
                Date = date,
                EventId = 10,
               EventTypes = Models.EventTypes.Friendly,
                FixturesGenerated = true,
                EventName = "Test",
                OwnerId = 4
            };

            var domainEventModel = new Domain.Models.EventModel
            {
                Completed = true,
                Date = date,
                EventId = 10,
               EventTypes = Domain.Models.EventTypes.Friendly,
                FixturesGenerated = true,
                EventName = "Test",
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
            Assert.AreEqual(model.EventName, "Test");
            Assert.AreEqual(model.OwnerId, 4);
            Assert.AreEqual(model.Date, date);
            Assert.AreEqual(model.EventId, 10);
            Assert.AreEqual(1, model.Users.Count);
            Assert.AreEqual(model.EventTypes, EventTypes.Friendly);
        }
    }
}
