using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ProEvoCanary.AcceptanceTests
{
    [TestFixture]
    public class HomePageTests
    {
        private IWebDriver _driver;
        const string URL = "http://www.proevoleague.hem/";

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver(@"C:\");
        }

        [Test]
        public void ShouldHaveEventsOnTheHomePage()
        {
            _driver.Navigate().GoToUrl(URL);
            IWebElement query = _driver.FindElement(By.TagName("h2"));

            Assert.That(query.Text, Is.EqualTo("Events"));
        }  
        
        [Test]
        public void ShouldGoToRecordsPageWhenLinkIsClicked()
        {
            _driver.Navigate().GoToUrl(URL);
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