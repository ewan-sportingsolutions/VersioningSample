using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IFixtureServiceAsync
    {
        /// <summary>
        /// return a fixture by its id
        /// </summary>
        Task<Fixture> GetFixtureAsync(
            string id);

        Task<Fixture> AddFixtureAsync(
            Fixture fixture);

        /// <summary>
        /// get fixtures between the start and end date
        /// </summary>
        Task<List<Fixture>> GetFixtureByDateAsync(
            DateTime startDate, 
            DateTime endDate);
    }
}
