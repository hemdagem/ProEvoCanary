using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Web.Controllers;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.UnitTests.ControllerTests
{
    [TestFixture]
    public class RecordsControllerTests
    {
        Mock<IPlayerRepository> _playerRepository;
        Mock<IResultRepository> _resultRepository;
        private Mock<IMapper> _mapper;

        public RecordsControllerTests()
        {
            _mapper = new Mock<IMapper>();
            _playerRepository = new Mock<IPlayerRepository>();
            _resultRepository = new Mock<IResultRepository>();
            var playerModels = new List<PlayerModel>()
            {
                new PlayerModel
                {
                    PlayerName = "Hemang",
                    PlayerId = 1
                }
            };

            var domainPlayerModels = new List<DataAccess.Models.PlayerModel>()
            {
                new DataAccess.Models.PlayerModel
                {
                    PlayerName = "Hemang",
                    PlayerId = 1
                }
            };
            _playerRepository.Setup(x => x.GetAllPlayers()).Returns(domainPlayerModels);

            _mapper.Setup(x => x.Map<List<PlayerModel>>(domainPlayerModels)).Returns(playerModels);

            var recordsModel = new RecordsModel
            {
                TotalMatches = 1,
                PlayerOneWins = 2,
                PlayerTwoWins = 3,
                TotalDraws = 4,
                Results = new List<ResultsModel>
                {
                    new ResultsModel
                    {
                        AwayScore = 0,
                        AwayTeam = "Villa",
                        HomeScore = 3,
                        HomeTeam = "Arsenal",
                        ResultId = Guid.NewGuid()
                    }
                }
            };

            var domainRecordsModel = new DataAccess.Models.RecordsModel
            {
                TotalMatches = 1,
                PlayerOneWins = 2,
                PlayerTwoWins = 3,
                TotalDraws = 4,
                Results = new List<DataAccess.Models.ResultsModel>
                {
                    new DataAccess.Models.ResultsModel
                    {
                        AwayScore = 0,
                        AwayTeam = "Villa",
                        HomeScore = 3,
                        HomeTeam = "Arsenal",
                        ResultId = Guid.NewGuid()
                    }
                }
            };
            _resultRepository.Setup(x => x.GetHeadToHeadRecord(1, 2)).Returns(domainRecordsModel);

            _mapper.Setup(x => x.Map<RecordsModel>(domainRecordsModel)).Returns(recordsModel);
        }

        [Test]
        public void ShouldGetRightViewForHeadToHeadView()
        {
            //given
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object, _mapper.Object);

            //when
            var result = recordsController.HeadToHead() as ViewResult;

            //then
            Assert.That(result.ViewName, Is.EqualTo("HeadToHead"));
        }

        [Test]
        public void ShouldSetModelWithRightPropertiesForHeadToHead()
        {
            //given
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object, _mapper.Object);

            //when
            var result = recordsController.HeadToHead() as ViewResult;

            //then
            var model = (ResultsListModel)result.Model;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.PlayerList.First().PlayerName, Is.EqualTo("Hemang"));
            Assert.That(model.PlayerList.First().PlayerId, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetHeadToHeadRecord()
        {
            //given
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object, _mapper.Object);

            //when
            var result = recordsController.HeadToHeadResults(1, 2) as ViewResult;

            //then
            var model = (ResultsListModel)result.Model;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.HeadToHead.Results.First().AwayScore, Is.EqualTo(0));
            Assert.That(model.HeadToHead.Results.First().HomeScore, Is.EqualTo(3));
            Assert.That(model.HeadToHead.Results.First().ResultId, Is.TypeOf<Guid>());
            Assert.That(model.HeadToHead.Results.First().HomeTeam, Is.EqualTo("Arsenal"));
            Assert.That(model.HeadToHead.Results.First().AwayTeam, Is.EqualTo("Villa"));
            Assert.That(model.PlayerOne, Is.EqualTo(1));
            Assert.That(model.PlayerTwo, Is.EqualTo(2));
            Assert.That(model.HeadToHead.TotalMatches, Is.EqualTo(1));
            Assert.That(model.HeadToHead.TotalDraws, Is.EqualTo(4));
            Assert.That(model.HeadToHead.PlayerOneWins, Is.EqualTo(2));
            Assert.That(model.HeadToHead.PlayerTwoWins, Is.EqualTo(3));
        }
    }
}
