using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Controllers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Tests.ControllerTests
{
    [TestFixture]
    public class RecordsControllerTests
    {
        Mock<IPlayerRepository> _playerRepository;
        Mock<IResultRepository> _resultRepository;


        private void Setup()
        {
            _playerRepository = new Mock<IPlayerRepository>();
            _resultRepository = new Mock<IResultRepository>();
            _playerRepository.Setup(x => x.GetAllPlayers()).Returns(new List<PlayerModel>()
            {
                new PlayerModel
                {
                    PlayerName = "Hemang",
                    PlayerId = 1
                }
            });
        }

        [Test]
        public void ShouldGetRightViewForHeadToHeadView()
        {
            //given
            Setup();
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object);

            //when
            var result = recordsController.HeadToHead() as ViewResult;

            //then
            Assert.That(result.ViewName, Is.EqualTo("HeadToHead"));
        }


        [Test]
        public void ShouldSetModelWithRightPropertiesForHeadToHead()
        {
            //given
            Setup();
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object);

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
            Setup();

            _resultRepository.Setup(x => x.GetHeadToHeadRecord(1, 2)).Returns(
                new Domain.RecordsModel
                {
                    TotalMatches = 1,
                    PlayerOneWins = 2,
                    PlayerTwoWins = 3,
                    TotalDraws = 4,
                    Results = new List<Domain.ResultsModel>
                    {
                       new Domain.ResultsModel
                {
                    AwayScore = 0,
                    AwayTeam = "Villa",
                    HomeScore = 3,
                    HomeTeam = "Arsenal",
                    ResultId = 1
                }
                    }
                }
            );
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object);

            //when
            var result = recordsController.HeadToHeadResults(1, 2) as ViewResult;

            //then
            var model = (ResultsListModel)result.Model;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.HeadToHead.Results.First().AwayScore, Is.EqualTo(0));
            Assert.That(model.HeadToHead.Results.First().HomeScore, Is.EqualTo(3));
            Assert.That(model.HeadToHead.Results.First().ResultId, Is.EqualTo(1));
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
