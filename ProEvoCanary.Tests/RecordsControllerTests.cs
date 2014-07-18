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
            Assert.That(result.ViewName,Is.EqualTo("HeadToHead"));
        }


        [Test]
        public void ShouldSetModelWithRightProperties()
        {
            //given
            Setup();
            _playerRepository.Setup(x => x.GetPlayerList()).Returns(new SelectListModel
            {
                ListItems = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Hemang", Value ="1"
                    }
                    
                }, SelectedItem = "0"
               
            });
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object);

            //when
            var result = recordsController.HeadToHead() as ViewResult;

            //then
            var model = (ResultsListModel)result.Model;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.Items.ListItems.First().Text, Is.EqualTo("Hemang"));
            Assert.That(model.Items.ListItems.First().Value, Is.EqualTo("1"));
  
        }

        [Test]
        public void ShouldGetHeadToHeadRecord()
        {
            //given
            Setup();
            _playerRepository.Setup(x => x.GetPlayerList()).Returns(new SelectListModel
            {
                ListItems = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Hemang", Value ="1"
                    },
                    new SelectListItem
                    {
                        Text = "Hemdagem", Value = "2"
                    }
                    
                },
                SelectedItem = "0"

            });

            _resultRepository.Setup(x => x.GetHeadToHeadResults(It.IsAny<int>(),It.IsAny<int>())).Returns(new List<ResultsModel>
            {
                new ResultsModel
                {
                    AwayScore = 0,
                    AwayTeam = "Villa",
                    HomeScore = 3,
                    HomeTeam = "Arsenal",
                    ResultID = 1
                }
            });
            var recordsController = new RecordsController(_playerRepository.Object, _resultRepository.Object);

            //when
            var result = recordsController.HeadToHeadResults(It.IsAny<int>(), It.IsAny<int>()) as ViewResult;

            //then
            var model = (ResultsListModel)result.Model;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.Results.First().AwayScore, Is.EqualTo(0));
            Assert.That(model.Results.First().HomeScore, Is.EqualTo(3));
            Assert.That(model.Results.First().ResultID, Is.EqualTo(1));
            Assert.That(model.Results.First().HomeTeam, Is.EqualTo("Arsenal"));
            Assert.That(model.Results.First().AwayTeam, Is.EqualTo("Villa"));
  
        }
    }
}
