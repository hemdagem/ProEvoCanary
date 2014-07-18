using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Helpers;
using ProEvoCanary.Repositories;
using ProEvoCanary.Models;

namespace ProEvoTests
{
    [TestFixture]
    public class PlayerRepositoryTests
    {


        [Test]
        public void ShouldGetPlayerList()
        {
            //given
            var resultsModel = new List<PlayerModel>()
            {
                new PlayerModel
                {
                    PlayerName = "Arsenal",
                    PlayerId = 1
                }
            };

            Mock<IDBHelper> helper = new Mock<IDBHelper>();
            PlayerRepository repository = new PlayerRepository(helper.Object);

            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                reader(resultsModel));

            //when
            var resultsModels = repository.GetPlayerList();

            //then
            Assert.That(resultsModels,Is.Not.Null);
            Assert.That(resultsModels.ListItems.Count(), Is.EqualTo(1));
            Assert.That(resultsModels.ListItems.First().Text, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.ListItems.First().Value, Is.EqualTo("1"));


        }

        [Test]
        public void ShouldGetPlayers()
        {

            //given
            var resultsModel = new List<PlayerModel>()
            {
                new PlayerModel
                {
                    
                    PointsPerGame = 4.2f,
                    GoalsPerGame = 3.2f,
                    PlayerName = "Arsenal",
                    MatchesPlayed = 1,
                    PlayerId = 1
                }
            };

            Mock<IDBHelper> helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                reader(resultsModel));

            PlayerRepository repository = new PlayerRepository(helper.Object);

            //when
            var resultsModels = repository.GetPlayers();

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().PointsPerGame, Is.EqualTo(4.2f));
            Assert.That(resultsModels.First().GoalsPerGame, Is.EqualTo(3.2f));
            Assert.That(resultsModels.First().PlayerName, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().MatchesPlayed, Is.EqualTo(1));
            Assert.That(resultsModels.First().PlayerId, Is.EqualTo(1));

        }

        private IDataReader reader(List<PlayerModel> objectsToEmulate)
        {
            var moq = new Mock<IDataReader>();

            // This var stores current position in 'ojectsToEmulate' list
            int count = -1;

            moq.Setup(x => x.Read())
                .Returns(() => count < objectsToEmulate.Count - 1)
                .Callback(() => count++);

            moq.Setup(x => x["LoginID"])
                .Returns(() => objectsToEmulate[count].PlayerId);

            moq.Setup(x => x["Name"])
                .Returns(() => objectsToEmulate[count].PlayerName);

            moq.Setup(x => x["GoalsPerGame"])
                .Returns(() => objectsToEmulate[count].GoalsPerGame);

            moq.Setup(x => x["PointsPerGame"])
                .Returns(() => objectsToEmulate[count].PointsPerGame);

            moq.Setup(x => x["MatchesPlayed"])
                .Returns(() => objectsToEmulate[count].MatchesPlayed);

            return moq.Object;
        }

    }
}

