using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace WebUI.Models
{
    public class FixtureViewModel
    {
        public FixtureViewModel()
        {
            this.Fixture = new Fixture();
        }

        public Fixture Fixture { get; set; }
    }
}