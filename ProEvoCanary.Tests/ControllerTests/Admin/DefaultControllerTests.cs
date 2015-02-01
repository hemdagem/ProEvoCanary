﻿using System.Web.Mvc;
using NUnit.Framework;
using ProEvoCanary.Areas.Admin.Controllers;

namespace ProEvoCanary.Tests.ControllerTests.Admin
{
    [TestFixture]
    public class DefaultControllerTests
    {
        [Test]
        public void Should()
        {
            //given
            var defaultController = new DefaultController();
            //when
            var actionResult = defaultController.Index() as ViewResult;
            //then
            Assert.That(actionResult.View,Is.EqualTo("Index"));
        }
    }
}
