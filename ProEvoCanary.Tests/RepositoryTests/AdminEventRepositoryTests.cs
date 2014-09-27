using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Areas.Admin.Models;
using ProEvoCanary.Helpers.Exceptions;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.Tests.RepositoryTests
{
    [TestFixture]
    public class AdminEventRepositoryTests
    {


        [Test]
        public void ShouldHaveRightParametersWhenCreatingEvent()
        {
            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(1);

            var repository = new AdminEventRepository(helper.Object);

            //then
            repository.CreateEvent("test", It.IsAny<DateTime>(), It.IsAny<EventTypes>(), 1);

            helper.Verify(x => x.AddParameter("@TournamentName", "test"), Times.Once);
            helper.Verify(x => x.AddParameter("@TournamentType", (int)It.IsAny<EventTypes>()), Times.Once);
            helper.Verify(x => x.AddParameter("@Date", It.IsAny<DateTime>()), Times.Once);
            helper.Verify(x => x.AddParameter("@OwnerId", 1), Times.Once);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldThrowExceptionIfTournamentNameIsEmptyOrNullWhenCreatingEvent(string tournamentName)
        {
            //given
            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(1);

            var repository = new AdminEventRepository(helper.Object);

            //then
            repository.CreateEvent(tournamentName, It.IsAny<DateTime>(), It.IsAny<EventTypes>(), It.IsAny<int>());

        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        [ExpectedException(typeof(LessThanOneException))]
        public void ShouldThrowExceptionIfOwnerIdIsLessThanZeroWhenCreatingEvent(int ownerId)
        {
            //given
            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(1);

            var repository = new AdminEventRepository(helper.Object);

            //then
            repository.CreateEvent("Test", It.IsAny<DateTime>(), It.IsAny<EventTypes>(), ownerId);
        }


        [Test]
        public void ShouldCreateNewTournament()
        {
            //given
            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteScalar(It.IsAny<string>())).Returns(1);

            var repository = new AdminEventRepository(helper.Object);

            //when
            var user = repository.CreateEvent("TournamentName", DateTime.UtcNow, EventTypes.Friendly, 1);

            //then
            Assert.That(user, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetListOfEvents()
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


            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new EventRepository(helper.Object);

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
    }
}
