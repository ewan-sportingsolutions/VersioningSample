using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.Interfaces;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IFixtureServiceAsync fixtureService;

        public HomeController(IFixtureServiceAsync fixtureService)
        {
            this.fixtureService = fixtureService;
        }

        public async Task<ActionResult> Index(string id = null)
        {
            FixtureViewModel vm = new FixtureViewModel();
            if (id != null)
            {
                Fixture fixture = await this.fixtureService.GetFixtureAsync(id);
                if (fixture != null)
                {
                    vm.Fixture = fixture;
                }
                else
                {
                    vm.Fixture = fixture = new Fixture()
                    {
                        Id = id,
                        Description = "no fixture found"
                    };
                }
            }
            return View(vm);
        }

        public async Task<ActionResult> Add(string id, string desc)
        {
            Fixture fixture = new Fixture();
            fixture.Id = id;
            fixture.Description = desc;

            await this.fixtureService.GetFixtureAsync(id);

            return RedirectToAction(
                "Index",
                new
                {
                    id = fixture.Id
                });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}