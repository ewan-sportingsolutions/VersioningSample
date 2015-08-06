using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interfaces;

namespace FixtureServiceRestSharp
{
    public class FixtureService_RestSharp : IFixtureService
    {
        private FixtureServiceAsync_RestSharp restServiceAsync;

        public FixtureService_RestSharp(
            Uri endpoint,
            string authkey,
            int timeout = 10000)
        {
            this.restServiceAsync = new FixtureServiceAsync_RestSharp(
                endpoint,
                authkey,
                timeout);
        }

        public Model.Fixture GetFixture(string id)
        {
            return this.restServiceAsync.GetFixtureAsync(id)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public Model.Fixture AddFixture(Model.Fixture fixture)
        {
            return this.restServiceAsync.AddFixtureAsync(fixture)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }


        public List<Model.Fixture> GetFixtureByDate(DateTime startDate, DateTime endDate)
        {
            return this.restServiceAsync.GetFixtureByDateAsync(startDate, endDate)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}
