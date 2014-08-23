using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.Tests
{
    [TestFixture]
    public class EventRepositoryTests
    {
        [Test]
        public void ShouldGetListOfEvents()
        {
            //given
            var dictionary = new Dictionary<string, object>
            {
                {"TournamentId", 0},
                {"TournamentName", "Event"},
                {"Venue", "Venue"},
                {"Date", "10/10/2010"},
                {"Name", "Arsenal"},
                {"Completed", true},
            };


            var helper = new Mock<IdBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new EventRepository(helper.Object);

            //when
            var resultsModels = repository.GetEvents();

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().EventId, Is.EqualTo(0));
            Assert.That(resultsModels.First().EventName, Is.EqualTo("Event"));
            Assert.That(resultsModels.First().Venue, Is.EqualTo("Venue"));
            Assert.That(resultsModels.First().Date, Is.EqualTo("10/10/2010"));
            Assert.That(resultsModels.First().Name, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().Completed, Is.EqualTo(true));
        }


       
    }
}
