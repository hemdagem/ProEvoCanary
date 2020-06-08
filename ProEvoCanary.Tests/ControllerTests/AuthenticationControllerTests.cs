using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Web.Controllers;

namespace ProEvoCanary.UnitTests.ControllerTests
{
    [TestFixture]
    public class AuthenticationControllerTests
    {
        private Mock<IUserRepository> _repo;

        public AuthenticationControllerTests()
        {
            _repo = new Mock<IUserRepository>();
        }

        [Test]
        public void ShouldSetDefaultViewNameToCreateForCreatePage()
        {
            //given
            var authenticationController = new AuthenticationController(_repo.Object);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then
            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
        }

    }
}
