using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Controllers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Tests
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
            _playerRepository.Setup(x => x.GetAllPlayers()).Returns(new SelectListModel
            {
                ListItems = new List<SelectListItem>
                {
                    new SelectListItem{ Text = "Hemang", Value ="1" }
                    
                },
                SelectedItem = "0"

            });
        }

        [Test]
        public void ShouldGetRightView()
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
        public void ShouldSetModelWithRightProperties()
        {
            //given
            Setup();
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object);

            //when
            var result = recordsController.HeadToHead() as ViewResult;

            //then
            var model = (ResultsListModel)result.Model;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.PlayerOneList.ListItems.First().Text, Is.EqualTo("Hemang"));
            Assert.That(model.PlayerOneList.ListItems.First().Value, Is.EqualTo("1"));
            Assert.That(model.PlayerTwoList.ListItems.First().Text, Is.EqualTo("Hemang"));
            Assert.That(model.PlayerTwoList.ListItems.First().Value, Is.EqualTo("1"));

        }

        [Test]
        public void ShouldGetHeadToHeadRecord()
        {
            //given
            Setup();

            _resultRepository.Setup(x => x.GetHeadToHeadResults(1, 2)).Returns(new List<ResultsModel>
            {
                new ResultsModel
                {
                    AwayScore = 0,
                    AwayTeam = "Villa",
                    HomeScore = 3,
                    HomeTeam = "Arsenal",
                    ResultId = 1
                }
            });
            _resultRepository.Setup(x => x.GetHeadToHeadRecord(1, 2)).Returns(
                new RecordsModel
                {
                    TotalMatches = 1,
                    PlayerOneWins = 2,
                    PlayerTwoWins = 3,
                    TotalDraws = 4
                }
            );
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object);

            //when
            var result = recordsController.HeadToHeadResults(1, 2) as ViewResult;

            //then
            var model = (ResultsListModel)result.Model;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.Results.First().AwayScore, Is.EqualTo(0));
            Assert.That(model.Results.First().HomeScore, Is.EqualTo(3));
            Assert.That(model.Results.First().ResultId, Is.EqualTo(1));
            Assert.That(model.Results.First().HomeTeam, Is.EqualTo("Arsenal"));
            Assert.That(model.Results.First().AwayTeam, Is.EqualTo("Villa"));
            Assert.That(model.PlayerOneList.SelectedItem, Is.EqualTo("1"));
            Assert.That(model.PlayerTwoList.SelectedItem, Is.EqualTo("2"));
            Assert.That(model.HeadToHead.TotalMatches, Is.EqualTo(1));
            Assert.That(model.HeadToHead.TotalDraws, Is.EqualTo(4));
            Assert.That(model.HeadToHead.PlayerOneWins, Is.EqualTo(2));
            Assert.That(model.HeadToHead.PlayerTwoWins, Is.EqualTo(3));

        }

    }
}
