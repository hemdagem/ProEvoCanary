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
    public class ResultsRepositoryTests
    {

        [Test]
        public void ShouldGetResults()
        {
            //given
            var dictionary = new Dictionary<string, object>
            {
                {"HomeTeam", "Arsenal"},
                {"AwayTeam", "Villa"},
                {"HomeScore", 3},
                {"AwayScore", 0},
                {"HomeTeamID", 1},
                {"AwayTeamID", 2},
                {"ResultsID", 1},
            };

            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new ResultsRepository(helper.Object);

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
            var dictionary = new Dictionary<string, object>
            {
                {"HomeUser", "Arsenal"},
                {"AwayUser", "Villa"},
                {"HomeScore", 3},
                {"AwayScore", 0},
                {"ResultsID", 1},
            };

            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new ResultsRepository(helper.Object);

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
            var helper = new Mock<IDBHelper>();

            //given
            var dictionary = new Dictionary<string, object>
            {
                {"PlayerOneWins", 2},
                {"PlayerTwoWins", 2},
                {"TotalDraws", 0},
                {"TotalMatches", 4},
            };

            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new ResultsRepository(helper.Object);

            //when
            var resultsModels = repository.GetHeadToHeadRecord(It.IsAny<int>(), It.IsAny<int>());

            //then
            Assert.That(resultsModels.TotalMatches, Is.EqualTo(4));
            Assert.That(resultsModels.TotalDraws, Is.EqualTo(0));
            Assert.That(resultsModels.PlayerOneWins, Is.EqualTo(2));
            Assert.That(resultsModels.PlayerTwoWins, Is.EqualTo(2));
        }


      
    }
}
