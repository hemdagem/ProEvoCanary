using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Repositories;
using ProEvoCanary.Tests.HelperTests;

namespace ProEvoCanary.Tests.RepositoryTests
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
                {"HomeTeamId", 1},
                {"AwayTeamId", 2},
                {"Id", 1},
            };

            var helper = new Mock<IDBHelper>();
            helper.Setup(x => x.ExecuteReader("up_RecentResults",null)).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new ResultsRepository(helper.Object);

            //when
            var resultsModels = repository.GetResults();

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().AwayScore, Is.EqualTo(0));
            Assert.That(resultsModels.First().AwayTeam, Is.EqualTo("Villa"));
            Assert.That(resultsModels.First().AwayTeamId, Is.EqualTo(2));
            Assert.That(resultsModels.First().HomeScore, Is.EqualTo(3));
            Assert.That(resultsModels.First().HomeTeam, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().HomeTeamId, Is.EqualTo(1));
            Assert.That(resultsModels.First().ResultId, Is.EqualTo(1));
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
                {"HomeUser", "Arsenal"},
                {"AwayUser", "Villa"},
                {"HomeScore", 3},
                {"AwayScore", 0},
                {"Id", 1},
            };

            helper.Setup(x => x.ExecuteReader("up_HeadToHeadRecord", It.IsAny<IDictionary<string, IConvertible>>())).Returns(
                DataReaderTestHelper.MultipleResultsReader(dictionary,new Queue<bool>(new[] { true, false, true, false })));

            var repository = new ResultsRepository(helper.Object);

            //when
            var resultsModels = repository.GetHeadToHeadRecord(It.IsAny<int>(), It.IsAny<int>());

            //then
            Assert.That(resultsModels.TotalMatches, Is.EqualTo(4));
            Assert.That(resultsModels.TotalDraws, Is.EqualTo(0));
            Assert.That(resultsModels.PlayerOneWins, Is.EqualTo(2));
            Assert.That(resultsModels.PlayerTwoWins, Is.EqualTo(2));
            Assert.That(resultsModels.Results.Count, Is.EqualTo(1));
            Assert.That(resultsModels.Results.First().AwayScore, Is.EqualTo(0));
            Assert.That(resultsModels.Results.First().AwayTeam, Is.EqualTo("Villa"));
            Assert.That(resultsModels.Results.First().HomeScore, Is.EqualTo(3));
            Assert.That(resultsModels.Results.First().HomeTeam, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.Results.First().ResultId, Is.EqualTo(1));
        }


      
    }
}
