using NUnit.Framework;
using ProEvoCanary.Web.Models;

namespace ProEvoCanary.UnitTests.ModelTests
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
