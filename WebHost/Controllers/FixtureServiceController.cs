using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using Model;
using Model.Interfaces;

namespace WebHost.Controllers
{
    public class FixtureServiceController : ApiController
    {
        private IFixtureService fixtureService;
        private ILog logger;
        public FixtureServiceController(
            IFixtureService fixtureService,
            ILog logger)
        {
            this.fixtureService = fixtureService;
            this.logger = logger;
        }

        [HttpGet]
        [HttpPost]
        public Fixture GetFixture(string id)
        {
            //todo::check the auth key!
            //log the version number
            logger.Info(
                "received message from version : " + GetVersion() +
                " from authKey " + GetAuthKey() + 
                " requesting fixture id : " + id);

            return this.fixtureService.GetFixture(id);
        }
        [HttpGet]
        [HttpPost]
        public Fixture AddFixture([FromBody]Fixture fixture)
        {
            //todo::check the auth key!
            //log the version number
            logger.Info(
                "received message from version : " + GetVersion() +
                " from authKey " + GetAuthKey() + 
                " adding fixture id : " + fixture.Id);

            return this.fixtureService.AddFixture(fixture);
        }

        [HttpGet]
        [HttpPost]
        public List<Fixture> GetFixtureByDate(DateTime startDate, DateTime endDate)
        {
            //todo::check the auth key!
            //log the version number
            logger.Info(
                "received message from version : " + GetVersion() +
                " from authKey " + GetAuthKey() +
                " GetFixtureByDate : " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString());

            return this.fixtureService.GetFixtureByDate(startDate, endDate);
        }

        private string GetVersion()
        {
            string versionString = "not specified";
            if (Request.Headers.Contains(
                "Version"))
            {
                IEnumerable<string> version = Request.Headers.GetValues("Version");

                if (version != null)
                {
                    versionString = version.FirstOrDefault() ?? "not specified";
                }
            }

            return versionString;
        }

        private string GetAuthKey()
        {
            string versionString = "not specified";
            if (Request.Headers.Contains(
               "X-Auth-Token"))
            {
                IEnumerable<string> version = Request.Headers.GetValues("X-Auth-Token");

                if (version != null)
                {
                    versionString = version.FirstOrDefault() ?? "not specified";
                }
            }

            return versionString;
        }
    }
}