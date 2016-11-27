﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.Web.Controllers;
using ProEvoCanary.Web.Models;
using EventModel = ProEvoCanary.Domain.Models.EventModel;
using PlayerModel = ProEvoCanary.Domain.Models.PlayerModel;
using ResultsModel = ProEvoCanary.Domain.Models.ResultsModel;
using RssFeedModel = ProEvoCanary.Domain.Models.RssFeedModel;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class DefaultControllerTests
    {
        private Mock<ICachePlayerRepository> _playerRepository;
        private Mock<IRssFeedRepository> _rssFeedRepository;
        private Mock<IEventRepository> _eventsRepository;
        private Mock<IResultRepository> _resultsRepository;
        private Mock<IMapper> _mapper;
        private DefaultController _defaultController;
        private ViewResult _result;

        public DefaultControllerTests()
        {
            _mapper = new Mock<IMapper>();
            _playerRepository = new Mock<ICachePlayerRepository>();
            var domainPlayerModels = new List<PlayerModel>
            {
                new PlayerModel
                {
                    PlayerId = 1,
                    PlayerName = "Hemang",
                    GoalsPerGame = 2,
                    MatchesPlayed = 3,
                    PointsPerGame = 3.2f
                }
            };

            var playerModels = new List<Web.Models.PlayerModel>
            {
                new Web.Models.PlayerModel
                {
                    PlayerId = 1,
                    PlayerName = "Hemang",
                    GoalsPerGame = 2,
                    MatchesPlayed = 3,
                    PointsPerGame = 3.2f
                }
            };
            _playerRepository.Setup(x => x.GetTopPlayers()).Returns(domainPlayerModels);


            _rssFeedRepository = new Mock<IRssFeedRepository>();
            var domainRssFeedModels = new List<RssFeedModel>
            {
                new RssFeedModel
                {
                    LinkTitle = "hemang",
                    LinkDescription = "ha"
                }
            };

            var rssFeedModels = new List<Web.Models.RssFeedModel>
            {
                new Web.Models.RssFeedModel
                {
                    LinkTitle = "hemang",
                    LinkDescription = "ha"
                }
            };
            _rssFeedRepository.Setup(x => x.GetFeed(It.IsAny<string>())).Returns(domainRssFeedModels);

            _eventsRepository = new Mock<IEventRepository>();

            var domainEventModels = new List<EventModel>
            {
                new EventModel
                {
                    EventId = 1,
                    EventName = "Hemang",
                    Date = "10/10/2014",
                    Name = "Hemang",
                    Completed = true
                }
            };
            var eventModels = new List<Web.Models.EventModel>
            {
                new Web.Models.EventModel
                {
                    EventId = 1,
                    EventName = "Hemang",
                    Date = "10/10/2014",
                    Name = "Hemang",
                    Completed = true
                }
            };
            _eventsRepository.Setup(x => x.GetEvents()).Returns(domainEventModels);

            _resultsRepository = new Mock<IResultRepository>();
            var domainResultsModels = new List<ResultsModel>
            {
                new ResultsModel
                {
                    ResultId = 1,
                    HomeTeamId = 1,
                    HomeTeam = "Arsenal",
                    HomeScore = 5,
                    AwayTeamId = 2,
                    AwayTeam = "Aston Villa",
                    AwayScore = 2,
                }
            };

            var resultsModels = new List<Web.Models.ResultsModel>
            {
                new Web.Models.ResultsModel
                {
                    ResultId = 1,
                    HomeTeamId = 1,
                    HomeTeam = "Arsenal",
                    HomeScore = 5,
                    AwayTeamId = 2,
                    AwayTeam = "Aston Villa",
                    AwayScore = 2,
                }
            };
            _resultsRepository.Setup(x => x.GetResults()).Returns(domainResultsModels);

            _mapper.Setup(x => x.Map<List<Web.Models.EventModel>>(domainEventModels)).Returns(eventModels);
            _mapper.Setup(x => x.Map<List<Web.Models.PlayerModel>>(domainPlayerModels)).Returns(playerModels);
            _mapper.Setup(x => x.Map<List<Web.Models.RssFeedModel>>(domainRssFeedModels)).Returns(rssFeedModels);
            _mapper.Setup(x => x.Map<List<Web.Models.ResultsModel>>(domainResultsModels)).Returns(resultsModels);

            _defaultController = new DefaultController(_playerRepository.Object, _rssFeedRepository.Object,
                _eventsRepository.Object, _resultsRepository.Object, _mapper.Object);
            _result = _defaultController.Index() as ViewResult;
        }

        [Test]
        public void ShouldSetEventsModel()
        {
            //given + when
            var model = _result.Model as HomeModel;

            //then
            Assert.That(model.Events.Count(), Is.EqualTo(1));
            Assert.That(model.Events.First().EventId, Is.EqualTo(1));
            Assert.That(model.Events.First().EventName, Is.EqualTo("Hemang"));
            Assert.That(model.Events.First().Date, Is.EqualTo("10/10/2014"));
            Assert.That(model.Events.First().Name, Is.EqualTo("Hemang"));
            Assert.That(model.Events.First().Completed, Is.EqualTo(true));
        }

        [Test]
        public void ShouldSetPlayerModel()
        {
            //given + when
            var model = _result.Model as HomeModel;

            //then
            Assert.That(model.Players.Count, Is.EqualTo(1));
            Assert.That(model.Players[0].PlayerName, Is.EqualTo("Hemang"));
            Assert.That(model.Players[0].PlayerId, Is.EqualTo(1));
            Assert.That(model.Players[0].GoalsPerGame, Is.EqualTo(2));
            Assert.That(model.Players[0].MatchesPlayed, Is.EqualTo(3));
            Assert.That(model.Players[0].PointsPerGame, Is.EqualTo(3.2f));
        }

        [Test]
        public void ShouldSetResultsModel()
        {
            //given + when
            var model = _result.Model as HomeModel;

            //then
            Assert.That(model.Results.Count(), Is.EqualTo(1));
            Assert.That(model.Results.First().ResultId, Is.EqualTo(1));
            Assert.That(model.Results.First().HomeTeamId, Is.EqualTo(1));
            Assert.That(model.Results.First().HomeTeam, Is.EqualTo("Arsenal"));
            Assert.That(model.Results.First().HomeScore, Is.EqualTo(5));
            Assert.That(model.Results.First().AwayTeamId, Is.EqualTo(2));
            Assert.That(model.Results.First().AwayTeam, Is.EqualTo("Aston Villa"));
            Assert.That(model.Results.First().AwayScore, Is.EqualTo(2));
        }

        [Test]
        public void ShouldSetRssFeedModel()
        {
            //given + when
            var model = _result.Model as HomeModel;

            //then
            Assert.That(model.News.Count(), Is.EqualTo(1));
            Assert.That(model.News.First().LinkTitle, Is.EqualTo("hemang"));
            Assert.That(model.News.First().LinkDescription, Is.EqualTo("ha"));
        }

        [Test]
        public void ShouldSetViewToDefault()
        {
            //given + when + then
            Assert.That(_result.ViewName, Is.EqualTo("Index"));
        }
    }
}