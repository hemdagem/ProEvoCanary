using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories;

namespace ProEvoCanary.Tests
{
    [TestFixture]
    public class EventRepositoryTests
    {
        [Test]
        public void Should()
        {

            //given
            var eventModels = new List<EventModel>()
            {
                new EventModel
                {
                    EventID = 0,
                    EventName = "Event",
                    Venue = "Venue",
                    Date = "10/10/2010",
                    Name = "Arsenal",
                    Completed = true
                }
            };

            Mock<IDBHelper> helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                reader(eventModels));

            EventRepository repository = new EventRepository(helper.Object);

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

        private IDataReader reader(List<EventModel> objectsToEmulate)
        {
            var moq = new Mock<IDataReader>();

            // This var stores current position in 'ojectsToEmulate' list
            int count = -1;

            moq.Setup(x => x.Read())
                .Returns(() => count < objectsToEmulate.Count - 1)
                .Callback(() => count++);

            moq.Setup(x => x["TournamentID"])
                .Returns(() => objectsToEmulate[count].EventID);

            moq.Setup(x => x["TournamentName"])
                .Returns(() => objectsToEmulate[count].EventName);

            moq.Setup(x => x["Venue"])
                .Returns(() => objectsToEmulate[count].Venue);

            moq.Setup(x => x["Date"])
                .Returns(() => objectsToEmulate[count].Date);

            moq.Setup(x => x["Name"])
                .Returns(() => objectsToEmulate[count].Name);
            moq.Setup(x => x["Completed"])
                .Returns(() => objectsToEmulate[count].Completed);

            return moq.Object;
        }

    }
}
