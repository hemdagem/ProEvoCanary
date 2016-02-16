using System.Web.Mvc;
using NUnit.Framework;
using ProEvoCanary.AdminWeb.Controllers;

namespace ProEvoCanary.AdminWeb.UnitTests
{
    [TestFixture]
    public class DefaultControllerTests
    {
        [Test]
        public void ShouldSetDefaultViewName()
        {
            //given
            var defaultController = new DefaultController();
            //when
            var actionResult = defaultController.Index() as ViewResult;
            //then
            Assert.That(actionResult.ViewName,Is.EqualTo("Index"));
        }
    }
}
