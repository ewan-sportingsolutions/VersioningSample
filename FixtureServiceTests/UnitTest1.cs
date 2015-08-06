using System;
using System.Configuration;
using System.Threading.Tasks;
using FixtureServiceRestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Interfaces;

namespace FixtureServiceTests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// integration test
        /// runs against the WebHost specifed in AppSettings
        /// todo:: use dependency injection (from file config) rather than these test methods
        /// </summary>
        [TestMethod]
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
        [TestMethod]
        public void BusinessLogicServiceTest()
        {
            IFixtureService fixtureService = new FixtureService.FixtureService();

            ServiceTest(
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

            this.AddFixtureTest(
                fixtureService,
                f);

            var result = this.GetFixtureTest(
                fixtureService,
                f.Id);

            Assert.AreEqual(f.Id, result.Id);
        }

        public void AddFixtureTest(IFixtureService fixtureService, Fixture f)
        {
            fixtureService.AddFixture(
                f);
        }

        public Fixture GetFixtureTest(IFixtureService fixtureService, string id)
        {
            return fixtureService.GetFixture(
                id);
        }
    }
}
