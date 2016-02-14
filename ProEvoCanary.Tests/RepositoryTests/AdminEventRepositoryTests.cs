using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Domain.Helpers.Exceptions;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Repositories;
using ProEvoCanary.Tests.HelperTests;
using EventTypes = ProEvoCanary.Domain.Models.EventTypes;

namespace ProEvoCanary.Tests.RepositoryTests
{
    [TestFixture]
    public class AdminEventRepositoryTests
    {

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldThrowExceptionIfTournamentNameIsEmptyOrNullWhenCreatingEvent(string tournamentName)
        {
            //given
            var helper = new Mock<IDBHelper>();
            var xmlGeneratorMock = new Mock<IXmlGenerator>();
            helper.Setup(x => x.ExecuteScalar("up_AddTournament", null)).Returns(1);

            var repository = new AdminEventRepository(helper.Object, xmlGeneratorMock.Object);

            //then
            repository.CreateEvent(tournamentName, It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>());

        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        [ExpectedException(typeof(LessThanOneException))]
        public void ShouldThrowExceptionIfOwnerIdIsLessThanZeroWhenCreatingEvent(int ownerId)
        {
            //given
            var helper = new Mock<IDBHelper>();
            var xmlGeneratorMock = new Mock<IXmlGenerator>();
            helper.Setup(x => x.ExecuteScalar("up_AddTournament", null)).Returns(1);

            var repository = new AdminEventRepository(helper.Object, xmlGeneratorMock.Object);

            //then
            repository.CreateEvent("Test", It.IsAny<DateTime>(), It.IsAny<int>(), ownerId);
        }


        [Test]
        public void ShouldCreateNewTournament()
        {
            //given
            var helper = new Mock<IDBHelper>();
            var xmlGeneratorMock = new Mock<IXmlGenerator>();
            helper.Setup(x => x.ExecuteScalar("up_AddTournament", It.IsAny<IDictionary<string,IConvertible>>())).Returns(1);

            var repository = new AdminEventRepository(helper.Object, xmlGeneratorMock.Object);

            //when
            var user = repository.CreateEvent("TournamentName", DateTime.UtcNow, (int)EventTypes.Friendly, 1);

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
             var xmlGeneratorMock = new Mock<IXmlGenerator>();
            helper.Setup(x => x.ExecuteReader("up_GetTournamentDetails", null)).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new AdminEventRepository(helper.Object, xmlGeneratorMock.Object);

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
