using System;
using System.Configuration;
using System.Threading.Tasks;
using FixtureServiceRestSharp;
using Model;
using Model.Interfaces;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace FixtureServiceTests
{
    public class UnitTest1
    {
        /// <summary>
        /// integration test
        /// runs against the WebHost specifed in AppSettings
        /// todo:: use dependency injection (from file config) rather than these test methods
        /// </summary>
        [Test]
        public void RestServiceTest()
        {
            Uri serviceEndPoint = new Uri(ConfigurationManager.AppSettings["serviceEndPoint"]);
            string authKey = ConfigurationManager.AppSettings["authKey"];

            IFixtureService fixtureService = new FixtureService_RestSharp(
                serviceEndPoint,
                authKey);

            ServiceTest(
                fixtureService);
        }

        /// <summary>
        /// unit test, runs against the business logic
        /// todo:: use dependency injection (from file config) rather than these test methods
        /// </summary>
        [Test]
        public void BusinessLogicServiceTest()
        {
            IFixtureService fixtureService = new FixtureService.FixtureService();

            ServiceTest(
                fixtureService);
        }


        /// <summary>
        /// integration test
        /// runs against the WebHost specifed in AppSettings
        /// todo:: use dependency injection (from file config) rather than these test methods
        /// </summary>
        [Test]
        public void RestServiceByDateTest()
        {
            Uri serviceEndPoint = new Uri(ConfigurationManager.AppSettings["serviceEndPoint"]);
            string authKey = ConfigurationManager.AppSettings["authKey"];

            IFixtureService fixtureService = new FixtureService_RestSharp(
                serviceEndPoint,
                authKey);

            ServiceTestByDate(
                fixtureService);
        }

        /// <summary>
        /// unit test, runs against the business logic
        /// todo:: use dependency injection (from file config) rather than these test methods
        /// </summary>
        [Test]
        public void BusinessLogicGetByDateTest()
        {
            IFixtureService fixtureService = new FixtureService.FixtureService();

            ServiceTestByDate(
                fixtureService);
        }

        /// <summary>
        /// the actual test
        /// </summary>
        /// <param name="fixtureService"></param>
        public void ServiceTest(IFixtureService fixtureService)
        {
            Fixture f = new Fixture();
            f.Id = "Test_" + Guid.NewGuid()
                .ToString();
            f.Description = "test fixture";
            f.Date = DateTime.Now;

            fixtureService.AddFixture(f);

            var result = fixtureService.GetFixture(f.Id);

            Assert.AreEqual(f.Id, result.Id);
            Assert.AreEqual(f.Date, result.Date);
        }

        public void ServiceTestByDate(IFixtureService fixtureService)
        {
            Fixture f = new Fixture();
            f.Id = "Test_" + Guid.NewGuid()
                .ToString();
            f.Description = "test fixture";
            f.Date = DateTime.Now;


            fixtureService.AddFixture(f);
            DateTime startDate = f.Date.AddDays(-1);
            DateTime endDate = f.Date.AddDays(+1);
            var result = fixtureService.GetFixtureByDate(startDate,endDate);

            Assert.AreEqual(f.Id, result[0].Id);
            Assert.AreEqual(f.Date, result[0].Date);
        }
    }
}
