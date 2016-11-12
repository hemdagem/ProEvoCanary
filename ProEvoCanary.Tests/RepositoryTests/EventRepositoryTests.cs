using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Domain.Helpers.Exceptions;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Repositories;
using ProEvoCanary.Tests.HelperTests;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.Tests.RepositoryTests
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
                {"Id", 0},
                {"TournamentName", "Event"},
                {"Date", "10/10/2010"},
                {"Name", "Arsenal"},
                {"Completed", true},
            };
            
            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader("up_GetTournamentDetails", null)).Returns(
                DataReaderTestHelper.Reader(dictionary));
            var xmlGeneratorMock = new Mock<IXmlGenerator>();
            var repository = new EventRepository(helper.Object, xmlGeneratorMock.Object);

            //when
            var resultsModels = repository.GetEvents();

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().EventId, Is.EqualTo(0));
            Assert.That(resultsModels.First().EventName, Is.EqualTo("Event"));
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
                {"TournamentType", EventTypes.Friendly},
            };

            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader("up_GetTournamentForEdit", It.IsAny<IDictionary<string, IConvertible>>())).Returns(
                DataReaderTestHelper.Reader(dictionary));
            var xmlGeneratorMock = new Mock<IXmlGenerator>();
            var repository = new EventRepository(helper.Object,xmlGeneratorMock.Object);

            //when
            var eventModel = repository.GetEvent(15);

            //then
            Assert.IsNotNull(eventModel);
            Assert.That(eventModel.EventId, Is.EqualTo(15));
            Assert.That(eventModel.OwnerId, Is.EqualTo(10));
            Assert.That(eventModel.EventName, Is.EqualTo("Event"));
            Assert.That(eventModel.Date, Is.EqualTo("10/10/2010"));
            Assert.That(eventModel.Completed, Is.EqualTo(false));
            Assert.That(eventModel.FixturesGenerated, Is.EqualTo(false));
            Assert.That(eventModel.EventTypes, Is.EqualTo(Domain.Models.EventTypes.Friendly));
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

            var repository = new EventRepository(helper.Object, xmlGeneratorMock.Object);

            //then
            Assert.Throws<NullReferenceException>(() => repository.CreateEvent(tournamentName, It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>()));
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

            var repository = new EventRepository(helper.Object, xmlGeneratorMock.Object);

            //then
            Assert.Throws<LessThanOneException>(() => repository.CreateEvent("Test", It.IsAny<DateTime>(), It.IsAny<int>(), ownerId));
        }

        [Test]
        public void ShouldCreateNewTournament()
        {
            //given
            var helper = new Mock<IDbHelper>();
            var xmlGeneratorMock = new Mock<IXmlGenerator>();
            helper.Setup(x => x.ExecuteScalar("up_AddTournament", It.IsAny<IDictionary<string, IConvertible>>())).Returns(1);

            var repository = new EventRepository(helper.Object, xmlGeneratorMock.Object);

            //when
            var user = repository.CreateEvent("TournamentName", DateTime.UtcNow, (int)EventTypes.Friendly, 1);

            //then
            Assert.That(user, Is.EqualTo(1));
        }
    }
}
