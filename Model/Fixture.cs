using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// a sporting event
    /// </summary>
    public class Fixture
    {
        public string Id { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// scheduled start time of the game
        /// </summary>
        public DateTime Date { get; set; }
    }
}
