﻿using System.Collections.Generic;
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

            var helper = new Mock<IdBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
                DataReaderTestHelper.Reader(dictionary));

            //when
            var repository = new PlayerRepository(helper.Object);
            var resultsModels = repository.GetAllPlayers();

            //then
            Assert.That(resultsModels,Is.Not.Null);
            Assert.That(resultsModels.ListItems.Count(), Is.EqualTo(1));
            Assert.That(resultsModels.ListItems.First().Text, Is.EqualTo("Arsenal"));
            Assert.That(resultsModels.ListItems.First().Value, Is.EqualTo("1"));

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

            var helper = new Mock<IdBHelper>();
            helper.Setup(x => x.ExecuteReader(It.IsAny<string>())).Returns(
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
        
      
    }
}

