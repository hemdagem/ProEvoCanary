using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Domain.Helpers.Exceptions;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Repositories;
using ProEvoCanary.Tests.HelperTests;

namespace ProEvoCanary.Tests.RepositoryTests
{
    [TestFixture]
    public class PlayerRepositoryTests
    {
        [Test]
        public void ShouldGetPlayerList()
        {
            //given
            var dictionary = new Dictionary<string, object>
            {
                {"Id", 1},
                {"Name", "Arsenal"},
                {"Surname", "Rajyaguru"},
                {"Username", "hemdagem"},
                {"UserType", 2}
            };

            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader("up_GetUsers",null)).Returns(
                DataReaderTestHelper.Reader(dictionary));

            //when
            var repository = new PlayerRepository(helper.Object);
            var resultsModels = repository.GetAllPlayers();

            //then
            Assert.That(resultsModels, Is.Not.Null);
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().PlayerName, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().PlayerId, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetPlayers()
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Id", 1},
                {"Name", "Arsenal"},
                {"GoalsPerGame", 3.2f},
                {"PointsPerGame", 4.2f},
                {"MatchesPlayed", 1}
            };

            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader("up_GetTopPlayers", null)).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new PlayerRepository(helper.Object);

            //when
            var resultsModels = repository.GetTopPlayers();

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().PointsPerGame, Is.EqualTo(4.2f));
            Assert.That(resultsModels.First().GoalsPerGame, Is.EqualTo(3.2f));
            Assert.That(resultsModels.First().PlayerName, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().MatchesPlayed, Is.EqualTo(1));
            Assert.That(resultsModels.First().PlayerId, Is.EqualTo(1));
        }
        
        [Test]
        public void ShouldThrowExceptionIfPageNumberIsLessThanOne()
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Id", 1},
                {"Name", "Arsenal"},
                {"GoalsPerGame", 3.2f},
                {"PointsPerGame", 4.2f},
                {"MatchesPlayed", 1}
            };

            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader("up_GetTopPlayers", null)).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new PlayerRepository(helper.Object);

            //when
           

            Assert.Throws<LessThanOneException>(() => repository.GetTopPlayersRange(0));
        }

        [Test]
        public void ShouldThrowExceptionIfRowsPerPageIsLessThanOne()
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Id", 1},
                {"Name", "Arsenal"},
                {"GoalsPerGame", 3.2f},
                {"PointsPerGame", 4.2f},
                {"MatchesPlayed", 1}
            };

            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>(), null)).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new PlayerRepository(helper.Object);

            //when
            Assert.Throws<LessThanOneException>(() => repository.GetTopPlayersRange(1,0));
        }

        [Test]
        public void ShouldGetTopPlayersRange()
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Id", 1},
                {"Name", "Arsenal"},
                {"GoalsPerGame", 3.2f},
                {"PointsPerGame", 4.2f},
                {"MatchesPlayed", 1}
            };

            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader("up_GetTopPlayers", It.IsAny<IDictionary<string,IConvertible>>())).Returns(
                DataReaderTestHelper.Reader(dictionary));

            var repository = new PlayerRepository(helper.Object);

            //when
            var resultsModels = repository.GetTopPlayersRange();

            //then
            Assert.That(resultsModels.Count, Is.EqualTo(1));
            Assert.That(resultsModels.First().PointsPerGame, Is.EqualTo(4.2f));
            Assert.That(resultsModels.First().GoalsPerGame, Is.EqualTo(3.2f));
            Assert.That(resultsModels.First().PlayerName, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.First().MatchesPlayed, Is.EqualTo(1));
            Assert.That(resultsModels.First().PlayerId, Is.EqualTo(1));
        }
        
        [Test]
        public void ShouldThrowExceptionIfMorePlayersThenSpecifiedIsReturned()
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Id", 1},
                {"Name", "Arsenal"},
                {"GoalsPerGame", 3.2f},
                {"PointsPerGame", 4.2f},
                {"MatchesPlayed", 1}
            };

            var helper = new Mock<IDbHelper>();
            helper.Setup(x => x.ExecuteReader("up_GetTopPlayers", It.IsAny<object>())).Returns(
                DataReaderTestHelper.MultipleResultsReader(dictionary, new Queue<bool>(new[] { true, true, true, false })));

            var repository = new PlayerRepository(helper.Object);

            //when
            Assert.Throws<TooManyPlayersReturnedException>(() => repository.GetTopPlayersRange(1, 2));
        }            
    }
}

