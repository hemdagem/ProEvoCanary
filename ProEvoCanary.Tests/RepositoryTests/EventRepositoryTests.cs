using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Repositories;
using ProEvoCanary.Tests.HelperTests;

namespace ProEvoCanary.Tests.RepositoryTests
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
                {"Id", 0},
                {"TournamentName", "Event"},
                {"Date", "10/10/2010"},
                {"Name", "Arsenal"},
                {"Completed", true},
            };


            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader("sp_GetTournamentDetails")).Returns(
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
