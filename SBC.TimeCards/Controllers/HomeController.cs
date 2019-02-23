using SBC.TimeCards.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SBC.TimeCards.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController(IUnitOfWork uow)
        {

        }

        public async Task<ActionResult> Index(int page=1)
        {
            //var pageSize = 6;
            //var projects = await _uow.Projects.GetAllAsync();
            //return View(projects.ToPagedList(page,pageSize));
            return View();
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