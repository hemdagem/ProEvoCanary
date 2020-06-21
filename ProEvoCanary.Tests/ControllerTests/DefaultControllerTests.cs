using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Application.EventHandlers.Events.Queries;
using ProEvoCanary.Application.EventHandlers.Players.GetPlayers;
using ProEvoCanary.Application.EventHandlers.Results.GetResults;
using ProEvoCanary.Application.EventHandlers.RssFeeds.GetFeed;
using ProEvoCanary.Web.Controllers;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.UnitTests.ControllerTests
{
    [TestFixture]
    public class DefaultControllerTests
    {
        private readonly Mock<IGetPlayersQueryHandler> _getPlayersQueryHandler;
        private readonly Mock<IGetRssFeedQueryHandler> _getRssFeedQueryHandler;
        private readonly Mock<IGetResultsQueryHandler> _getResultsQueryHandler;
        private readonly DefaultController _defaultController;
        private readonly ViewResult _result;
        private readonly Mock<IEventsQueryHandler> _eventQueryBaseMock;
        public DefaultControllerTests()
        {
	       
            _getPlayersQueryHandler = new Mock<IGetPlayersQueryHandler>();
            var domainPlayerModels = new List<PlayerModelDto>
            {
                new PlayerModelDto
                {
                    PlayerId = 1,
                    PlayerName = "Hemang",
                    GoalsPerGame = 2,
                    MatchesPlayed = 3,
                    PointsPerGame = 3.2f
                }
            };

            _getPlayersQueryHandler.Setup(x => x.Handle()).Returns(domainPlayerModels);


            _getRssFeedQueryHandler = new Mock<IGetRssFeedQueryHandler>();
            var domainRssFeedModels = new List<RssFeedModelDto>
            {
                new RssFeedModelDto
                {
                    LinkTitle = "hemang",
                    LinkDescription = "ha"
                }
            };

            _getRssFeedQueryHandler.Setup(x => x.Handle(It.IsAny<RssFeedQuery>())).Returns(domainRssFeedModels);


            var domainEventModels = new List<EventModelDto>
            {
                new EventModelDto
                {
                    TournamentId = Guid.NewGuid(),
                    TournamentName = "Hemang",
                    Date = "10/10/2014",
                    Name = "Hemang",
                    Completed = true
                }
            };

            _getResultsQueryHandler = new Mock<IGetResultsQueryHandler>();
            var domainResultsModels = new List<GetResultsModelDto>
            {
                new GetResultsModelDto
                {
                    ResultId = Guid.NewGuid(),
                    HomeTeamId = 1,
                    HomeTeam = "Arsenal",
                    HomeScore = 5,
                    AwayTeamId = 2,
                    AwayTeam = "Aston Villa",
                    AwayScore = 2,
                }
            };

            _getResultsQueryHandler.Setup(x => x.Handle()).Returns(domainResultsModels);

            _eventQueryBaseMock = new Mock<IEventsQueryHandler>();

            _eventQueryBaseMock.Setup(x => x.Handle()).Returns(domainEventModels);

          
            _defaultController = new DefaultController(_getPlayersQueryHandler.Object, _getRssFeedQueryHandler.Object, _eventQueryBaseMock.Object, _getResultsQueryHandler.Object);
            _result = _defaultController.Index() as ViewResult;
        }

        [Test]
        public void ShouldSetEventsModel()
        {
            //given + when
            var model = _result.Model as HomeModel;

            //then
            Assert.That(model.Events.Count(), Is.EqualTo(1));
            Assert.That(model.Events.First().TournamentId, Is.TypeOf<Guid>());
            Assert.That(model.Events.First().TournamentName, Is.EqualTo("Hemang"));
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
            Assert.That(model.Results.First().ResultId, Is.TypeOf<Guid>());
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