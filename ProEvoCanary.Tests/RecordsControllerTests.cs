using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvo45.Controllers;
using ProEvo45.Models;
using ProEvo45.Repositories.Interfaces;

namespace ProEvoTests
{
    [TestFixture]
    public class RecordsControllerTests
    {
        Mock<IPlayerRepository> playerRepository;


        private void Setup()
        {
            playerRepository = new Mock<IPlayerRepository>();
        }

        [Test]
        public void ShouldGetRightView()
        {
            //given
            Setup();
            var recordsController = new RecordsController(playerRepository.Object);

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
            playerRepository.Setup(x => x.GetPlayerList()).Returns(new SelectListModel
            {
                ListItems = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Hemang", Value ="1"
                    }
                    
                }, SelectedItem = "0"
               
            });
            var recordsController = new RecordsController(playerRepository.Object);

            //when
            var result = recordsController.HeadToHead() as ViewResult;

            //then
            var model = (SelectListModel)result.Model;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.ListItems.First().Text, Is.EqualTo("Hemang"));
            Assert.That(model.ListItems.First().Value, Is.EqualTo("1"));
  
        }
    }
}
