using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProEvoCanary.DataAccess.Helpers;

namespace ProEvoCanary.UnitTests.HelperTests
{
    [TestFixture]
    public class FixtureGenaratorTests
    {
        private static List<int> _teamIds = new List<int>();

        [Test]
        [TestCase(null)]
        [TestCaseSource("_teamIds")]
        public void ShouldThrowAnExceptionIfTeamsAreMissingOrNull(List<int> teamIds)
        {
            //given
            var fixtureGenerator = new FixtureGenerator();
            //then
            
            Assert.Throws<ArgumentNullException>(() => fixtureGenerator.Generate(teamIds));
        }

        [Test]
        public void ShouldThrowExceptionIfTeamIdsAreNotUnique()
        {
            //given
            var fixtureGenerator = new FixtureGenerator();
            var teamIds = new List<int>
            {
                1,
                1,
            };
            //then
            Assert.Throws<Exception>(() => fixtureGenerator.Generate(teamIds));
        }

        [Test]
        public void EachTeamShouldPlayEachOtherOnce()
        {

            //given
            var teamIdsOne = new List<int> { 1, 2 };
            var teamIdstwo = new List<int> { 1, 2, 3 };
            var teamIdsThree = new List<int> { 1, 2, 3, 4 };
            var fixtureGenerator = new FixtureGenerator();

            //then
            var values = fixtureGenerator.Generate(teamIdsOne);
            var values2 = fixtureGenerator.Generate(teamIdstwo);
            var value3 = fixtureGenerator.Generate(teamIdsThree);

            //then
            Assert.That(values.Count, Is.EqualTo(1));
            Assert.That(values[0].TeamOne,Is.EqualTo(1));
            Assert.That(values[0].TeamTwo,Is.EqualTo(2));

            Assert.That(values2.Count, Is.EqualTo(3));
            Assert.That(values2[0].TeamOne, Is.EqualTo(1));
            Assert.That(values2[0].TeamTwo, Is.EqualTo(2));
            Assert.That(values2[1].TeamOne, Is.EqualTo(1));
            Assert.That(values2[1].TeamTwo, Is.EqualTo(3));
            Assert.That(values2[2].TeamOne, Is.EqualTo(2));
            Assert.That(values2[2].TeamTwo, Is.EqualTo(3));

            Assert.That(value3.Count, Is.EqualTo(6));
            Assert.That(value3[0].TeamOne, Is.EqualTo(1));
            Assert.That(value3[0].TeamTwo, Is.EqualTo(2));
            Assert.That(value3[1].TeamOne, Is.EqualTo(1));
            Assert.That(value3[1].TeamTwo, Is.EqualTo(3));
            Assert.That(value3[2].TeamOne, Is.EqualTo(1));
            Assert.That(value3[2].TeamTwo, Is.EqualTo(4));
            Assert.That(value3[3].TeamOne, Is.EqualTo(2));
            Assert.That(value3[3].TeamTwo, Is.EqualTo(3));
            Assert.That(value3[4].TeamOne, Is.EqualTo(2));
            Assert.That(value3[4].TeamTwo, Is.EqualTo(4));
            Assert.That(value3[5].TeamOne, Is.EqualTo(3));
            Assert.That(value3[5].TeamTwo, Is.EqualTo(4));

        }
    }
}
