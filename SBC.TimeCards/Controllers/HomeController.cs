using SBC.TimeCards.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Models;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Services;

namespace SBC.TimeCards.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IProjectService _projectService;
        public HomeController(ProjectService projectService, ApplicationUserManager userManager):base(userManager)
        {
            _projectService = projectService;
        }

        public async Task<ActionResult> Index()
        {
            DashBoardProjectsViewModel vm = new DashBoardProjectsViewModel();
            vm.ArchivedProjects = _projectService.GetArchivedProjects(GetCurrentUserId());
            vm.ActiveProjects = _projectService.GetAllActiveProjects(GetCurrentUserId());
            return View(vm);
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