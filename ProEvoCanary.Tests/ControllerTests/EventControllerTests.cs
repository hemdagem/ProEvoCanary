using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
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
        private Mock<IPlayerRepository> _mockPlayerRepository;

        private void Setup()
        {
            _repo = new Mock<IAdminEventRepository>();
            _mockPlayerRepository = new Mock<IPlayerRepository>();
            _appUser = new Mock<IAppUser>();
            _appUser.Setup(x => x.CurrentUser).Returns(new UserClaimsPrincipal(new ClaimsPrincipal()));
            _eventController = new EventController(_repo.Object, _appUser.Object, _mockPlayerRepository.Object);
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


        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShouldReturnExceptionWhenEventIdIsLessThenZero()
        {
            // given
            Setup();

            // when + then
            _eventController.GenerateFixtures(-1);
        }


        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldReturnExceptionWhenEventDoesntExist()
        {
            //given
            Setup();

            _repo.Setup(x => x.GetEvent(It.IsAny<int>()))
                .Returns((EventModel)null);

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
                new Claim(ClaimTypes.NameIdentifier,"4"), 
            }));

            _appUser.Setup(x => x.CurrentUser).Returns(new UserClaimsPrincipal(claimsPrincipal));
            _mockPlayerRepository.Setup(x => x.GetAllPlayers()).Returns(new List<PlayerModel>()
            {
                new PlayerModel
                {
                    PlayerName = "Test",
                    PlayerId = 1
                },
                new PlayerModel
                {
                     PlayerName = "Test2",
                    PlayerId = 2
                }
            });
            _repo.Setup(x => x.GetEvent(It.IsAny<int>()))
                .Returns(new EventModel
                {
                    Completed = true,
                    Date = date,
                    EventId = 10,
                    EventTypes = EventTypes.Friendly,
                    FixturesGenerated = true,
                    EventName = "Test",
                    OwnerId = 4
                });
            //when
            var viewResult = _eventController.GenerateFixtures(It.IsAny<int>()) as ViewResult;
            var model = viewResult.Model as EventModel;
            //then
            Assert.That(viewResult.ViewName, Is.EqualTo("GenerateFixtures"));
            Assert.AreEqual(model.Completed, true);
            Assert.AreEqual(model.FixturesGenerated, true);
            Assert.AreEqual(model.EventName, "Test");
            Assert.AreEqual(model.OwnerId, 4);
            Assert.AreEqual(model.EventTypes, EventTypes.Friendly);
            Assert.AreEqual(model.Date, date);
            Assert.AreEqual(model.EventId, 10);
            Assert.AreEqual(2, model.Users.Count);

        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldThrowNullExceptionIfCurrentUserIsNotTheOwnerOfTheEvent()
        {
            //given
            Setup();

            _repo.Setup(x => x.GetEvent(10))
                .Returns(new EventModel { OwnerId = 10 });
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,"12"), 
            }));

            _appUser.Setup(x => x.CurrentUser).Returns(new UserClaimsPrincipal(claimsPrincipal));

            //when + then
            _eventController.GenerateFixtures(10);
        }
    }
}
