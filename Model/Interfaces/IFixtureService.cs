using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IFixtureService
    {
        /// <summary>
        /// return a fixture by its id
        /// </summary>
        Fixture GetFixture(
            string id);

        Fixture AddFixture(
            Fixture fixture);

        /// <summary>
        /// get fixtures between the start and end date
        /// </summary>
        List<Fixture> GetFixtureByDate(
            DateTime startDate,
            DateTime endDate);
    }
}
