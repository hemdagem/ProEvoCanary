using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace ProEvoCanary.AcceptanceTests
{
    [TestFixture]
    public class HomePageTests
    {
        private IWebDriver _driver;
        const string Url = "http://www.proevoleague.hem/";

        [SetUp]
        public void Setup()
        {
            _driver = new PhantomJSDriver();
        }

        [Test]
        public void ShouldHaveTheMainFourHeadingsOnTheHomePage()
        {
            _driver.Navigate().GoToUrl(Url);
            var query = _driver.FindElements(By.TagName("h2"));

            Assert.That(query[0].Text, Is.EqualTo("Events"));
            Assert.That(query[1].Text, Is.EqualTo("News"));
            Assert.That(query[2].Text, Is.EqualTo("Top Players"));
            Assert.That(query[3].Text, Is.EqualTo("Results"));
        }  
        
        [Test]
        public void ShouldHaveALinkToRecordsPage()
        {
            IWebElement query = _driver.FindElement(By.CssSelector("body > nav > section > ul > li:last-child > a"));

            Assert.That(query.GetAttribute("href"), Contains.Substring("/Records/HeadToHead"));
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
        }
    }
}