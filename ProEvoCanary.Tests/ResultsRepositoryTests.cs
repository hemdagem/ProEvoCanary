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
    public class ResultsRepositoryTests
    {

        [Test]
        public void ShouldGetResults()
        {

            //given
            var resultsModel = new List<ResultsModel>()
            {
                new ResultsModel
                {
                    AwayScore = 0,
                    AwayTeam = "Villa",
                    AwayTeamID = 2,
                    HomeScore = 3,
                    HomeTeam = "Arsenal",
                    HomeTeamID = 1,
                    ResultID = 1
                }
            };

            Mock<IDBHelper> helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                reader(resultsModel));

            ResultsRepository repository = new ResultsRepository(helper.Object);

            //when
            var resultsModels = repository.GetResults();

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().AwayScore, Is.EqualTo(0));
            Assert.That(resultsModels.First().AwayTeam, Is.EqualTo("Villa"));
            Assert.That(resultsModels.First().AwayTeamID, Is.EqualTo(2));
            Assert.That(resultsModels.First().HomeScore, Is.EqualTo(3));
            Assert.That(resultsModels.First().HomeTeam, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().HomeTeamID, Is.EqualTo(1));
            Assert.That(resultsModels.First().ResultID, Is.EqualTo(1));

        }

        [Test]
        public void ShouldGetHeadToHeadResults()
        {

            //given
            var resultsModel = new List<ResultsModel>()
            {
                new ResultsModel
                {
                    AwayScore = 0,
                    AwayTeam = "Villa",
                    HomeScore = 3,
                    HomeTeam = "Arsenal",
                    ResultID = 1
                }
            };

            Mock<IDBHelper> helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                readerForResults(resultsModel));

            ResultsRepository repository = new ResultsRepository(helper.Object);

            //when
            var resultsModels = repository.GetHeadToHeadResults(It.IsAny<int>(), It.IsAny<int>());

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().AwayScore, Is.EqualTo(0));
            Assert.That(resultsModels.First().AwayTeam, Is.EqualTo("Villa"));
            Assert.That(resultsModels.First().HomeScore, Is.EqualTo(3));
            Assert.That(resultsModels.First().HomeTeam, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().ResultID, Is.EqualTo(1));

        }

        [Test]
        public void ShouldGetHeadToHeadRecord()
        {
            Mock<IDBHelper> helper = new Mock<IDBHelper>();

            //given
            var resultsModel = new List<RecordsModel>()
            {
                new RecordsModel
                {
                    TotalMatches =1,
                    PlayerOneWins = 2,
                    PlayerTwoWins = 2,
                    TotalDraws = 4

                }
            };
            
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                readerForHeadToHeadResults(resultsModel));

            ResultsRepository repository = new ResultsRepository(helper.Object);

            //when
            var resultsModels = repository.GetHeadToHeadRecord(It.IsAny<int>(), It.IsAny<int>());

            //then
            Assert.That(resultsModels.TotalMatches, Is.EqualTo(1));
            Assert.That(resultsModels.TotalDraws, Is.EqualTo(4));
            Assert.That(resultsModels.PlayerOneWins, Is.EqualTo(2));
            Assert.That(resultsModels.PlayerTwoWins, Is.EqualTo(2));


        }

        private IDataReader readerForHeadToHeadResults(List<RecordsModel> objectsToEmulate)
        {
            var moq = new Mock<IDataReader>();

            int count = -1;

            moq.Setup(x => x.Read())
                .Returns(() => count < objectsToEmulate.Count - 1)
                .Callback(() => count++);

            moq.Setup(x => x["PlayerOneWins"])
                .Returns(() => objectsToEmulate[count].PlayerOneWins);

            moq.Setup(x => x["PlayerTwoWins"])
                .Returns(() => objectsToEmulate[count].PlayerTwoWins);

            moq.Setup(x => x["TotalDraws"])
                .Returns(() => objectsToEmulate[count].TotalDraws);

            moq.Setup(x => x["TotalMatches"])
                .Returns(() => objectsToEmulate[count].TotalMatches);

            return moq.Object;
        }

        private IDataReader reader(List<ResultsModel> objectsToEmulate)
        {
            var moq = new Mock<IDataReader>();

            int count = -1;

            moq.Setup(x => x.Read())
                .Returns(() => count < objectsToEmulate.Count - 1)
                .Callback(() => count++);

            moq.Setup(x => x["HomeTeam"])
                .Returns(() => objectsToEmulate[count].HomeTeam);

            moq.Setup(x => x["AwayTeam"])
                .Returns(() => objectsToEmulate[count].AwayTeam);

            moq.Setup(x => x["HomeScore"])
                .Returns(() => objectsToEmulate[count].HomeScore);

            moq.Setup(x => x["AwayScore"])
                .Returns(() => objectsToEmulate[count].AwayScore);

            moq.Setup(x => x["HomeTeamID"])
                .Returns(() => objectsToEmulate[count].HomeTeamID);

            moq.Setup(x => x["AwayTeamID"])
                .Returns(() => objectsToEmulate[count].AwayTeamID);

            moq.Setup(x => x["ResultsID"])
                .Returns(() => objectsToEmulate[count].ResultID);

            return moq.Object;
        }  
        
        private IDataReader readerForResults(List<ResultsModel> objectsToEmulate)
        {
            var moq = new Mock<IDataReader>();

            int count = -1;

            moq.Setup(x => x.Read())
                .Returns(() => count < objectsToEmulate.Count - 1)
                .Callback(() => count++);

            moq.Setup(x => x["HomeUser"])
                .Returns(() => objectsToEmulate[count].HomeTeam);

            moq.Setup(x => x["AwayUser"])
                .Returns(() => objectsToEmulate[count].AwayTeam);

            moq.Setup(x => x["HomeScore"])
                .Returns(() => objectsToEmulate[count].HomeScore);

            moq.Setup(x => x["AwayScore"])
                .Returns(() => objectsToEmulate[count].AwayScore);

            moq.Setup(x => x["ResultsID"])
                .Returns(() => objectsToEmulate[count].ResultID);

            return moq.Object;
        }

    }


   
}
