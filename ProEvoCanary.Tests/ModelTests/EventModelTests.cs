using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProEvoCanary.Models;

namespace ProEvoCanary.Tests.ModelTests
{
    [TestFixture]
    public class EventModelTests
    {
        [Test]
        public void UsersListShouldNotBeNullWhenModelIsInitialised()
        {
            //given + when
            var eventModel = new EventModel();

            //then
            Assert.IsNotNull(eventModel.Users);

        }
    }
}
