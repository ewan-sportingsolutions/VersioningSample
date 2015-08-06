using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interfaces;

namespace FixtureService
{
    /// <summary>
    /// wraps the synchronous service with async calls
    /// </summary>
    public class FixtureServiceAsync : IFixtureServiceAsync
    {
        private FixtureService fixtureService;
        public FixtureServiceAsync(FixtureService fixtureService)
        {
            this.fixtureService = fixtureService;
        }

        public async Task<Model.Fixture> GetFixtureAsync(string id)
        {
            return await Task.Run(() => fixtureService.GetFixture(id));
            
        }

        public async Task<Model.Fixture> AddFixtureAsync(Model.Fixture fixture)
        {
            return await Task.Run(() => fixtureService.AddFixture(fixture));
        }


        public async Task<List<Model.Fixture>> GetFixtureByDateAsync(DateTime startDate, DateTime endDate)
        {
            return await Task.Run(() => fixtureService.GetFixtureByDate(startDate, endDate));
        }
    }
}
