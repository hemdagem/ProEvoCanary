using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers;
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
                {"TournamentID", 0},
                {"TournamentName", "Event"},
                {"Venue", "Venue"},
                {"Date", "10/10/2010"},
                {"Name", "Arsenal"},
                {"Completed", true},
            };


            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new EventRepository(helper.Object,MemoryCache.Default);

            //when
            var resultsModels = repository.GetEvents();

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().EventID, Is.EqualTo(0));
            Assert.That(resultsModels.First().EventName, Is.EqualTo("Event"));
            Assert.That(resultsModels.First().Venue, Is.EqualTo("Venue"));
            Assert.That(resultsModels.First().Date, Is.EqualTo("10/10/2010"));
            Assert.That(resultsModels.First().Name, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().Completed, Is.EqualTo(true));
        }


        [Test]
        public void ShouldGetCachedListOfEvents()
        {
            //given
            var dictionary = new Dictionary<string, object>
            {
                {"TournamentID", 0},
                {"TournamentName", "Event"},
                {"Venue", "Venue"},
                {"Date", "10/10/2010"},
                {"Name", "Arsenal"},
                {"Completed", true},
            };


            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new EventRepository(helper.Object,MemoryCache.Default);

            //when
            repository.GetEvents();
            repository.GetEvents();

            //then
            helper.Verify(x=>x.ExecuteReader(It.IsAny<string>()),Times.Once());

        }
    }
}
