using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Interfaces;

namespace FixtureServiceMock
{
    public class FixtureService_Mock : IFixtureService
    { 
        private List<Fixture> fixtures;

        public FixtureService_Mock()
        {
            this.fixtures = new List<Fixture>();
            this.AddFixtures();
        }

        public void AddFixtures()
        {
            this.fixtures.Add(new Fixture() {Id ="test", Description = "test fixture"});
        }

        public Fixture GetFixture(string id)
        {
            return this.fixtures.FirstOrDefault(f => f.Id == id);
        }

        public Fixture AddFixture(Fixture fixture)
        {
            this.fixtures.Add(fixture);
            return fixture;
        }
    }
}
