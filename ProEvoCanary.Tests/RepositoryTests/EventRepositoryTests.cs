using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProEvoCanary.DataAccess;
using ProEvoCanary.DataAccess.Helpers;
using ProEvoCanary.DataAccess.Repositories;
using ProEvoCanary.Domain.Helpers.Exceptions;
using ProEvoCanary.UnitTests.HelperTests;
using TournamentType = ProEvoCanary.Web.Models.TournamentType;

namespace ProEvoCanary.UnitTests.RepositoryTests
{
    [TestFixture]
    public class EventRepositoryTests
    {
        [Test]
        public void ShouldGetListOfEventsForGetEventsMethod()
        {
            //given
            var dictionary = new Dictionary<string, object>
            {
                {"TournamentId", 0},
                {"TournamentName", "Event"},
                {"Date", "10/10/2010"},
                {"Name", "Arsenal"},
                {"Completed", true},
            };

            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader("up_GetTournamentDetails", null)).Returns(
                DataReaderTestHelper.Reader(dictionary));
            var repository = new EventReadRepository(helper.Object);

            //when
            var resultsModels = repository.GetEvents();

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().TournamentId, Is.EqualTo(0));
            Assert.That(resultsModels.First().TournamentName, Is.EqualTo("Event"));
            Assert.That(resultsModels.First().Date, Is.EqualTo("10/10/2010"));
            Assert.That(resultsModels.First().Name, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().Completed, Is.EqualTo(true));
        }

        [Test]
        public void ShouldGetEventInfoForGetEventMethod()
        {
            //given
            var dictionary = new Dictionary<string, object>
            {
                {"OwnerId", 10},
                {"TournamentName", "Event"},
                {"Date", "10/10/2010"},
                {"Completed", false},
                {"TournamentType", TournamentType.Friendly},
            };

            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader("up_GetTournamentForEdit", It.IsAny<object>())).Returns(
                DataReaderTestHelper.Reader(dictionary));
            var repository = new EventReadRepository(helper.Object);

            //when
            var eventId = Guid.NewGuid();
            var eventModel = repository.GetEvent(eventId);

            //then
            Assert.IsNotNull(eventModel);
            Assert.That(eventModel.TournamentId, Is.EqualTo(eventId));
            Assert.That(eventModel.OwnerId, Is.EqualTo(10));
            Assert.That(eventModel.TournamentName, Is.EqualTo("Event"));
            Assert.That(eventModel.Date, Is.EqualTo("10/10/2010"));
            Assert.That(eventModel.Completed, Is.EqualTo(false));
            Assert.That(eventModel.FixturesGenerated, Is.EqualTo(false));
            Assert.That(eventModel.TournamentType, Is.EqualTo(Domain.Models.TournamentType.Friendly));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionIfTournamentNameIsEmptyOrNullWhenCreatingEvent(string tournamentName)
        {
            //given
            var helper = new Mock<IDbHelper>();
            var xmlGeneratorMock = new Mock<IXmlGenerator>();
            helper.Setup(x => x.ExecuteScalar("up_AddTournament", null)).Returns(1);

            var repository = new EventWriteRepository(helper.Object, xmlGeneratorMock.Object);

            //then
            Assert.Throws<NullReferenceException>(() => repository.CreateEvent(tournamentName, It.IsAny<DateTime>(), It.IsAny<int>()));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        public void ShouldThrowExceptionIfOwnerIdIsLessThanZeroWhenCreatingEvent(int ownerId)
        {
            //given
            var helper = new Mock<IDbHelper>();
            var xmlGeneratorMock = new Mock<IXmlGenerator>();
            helper.Setup(x => x.ExecuteScalar("up_AddTournament", null)).Returns(1);

            var repository = new EventWriteRepository(helper.Object, xmlGeneratorMock.Object);

            //then
            Assert.Throws<LessThanOneException>(() => repository.CreateEvent("Test", It.IsAny<DateTime>(), It.IsAny<int>()));
        }

        [Test]
        public void ShouldCreateNewTournament()
        {
            //given
            var helper = new Mock<IDbHelper>();
            var xmlGeneratorMock = new Mock<IXmlGenerator>();
            helper.Setup(x => x.ExecuteScalar("up_AddTournament", It.IsAny<object>())).Returns(1);

            var repository = new EventWriteRepository(helper.Object, xmlGeneratorMock.Object);

            //when
            var user = repository.CreateEvent("TournamentName", DateTime.UtcNow, (int)TournamentType.Friendly);

            //then
            Assert.That(user, Is.EqualTo(1));
        }
    }
}
