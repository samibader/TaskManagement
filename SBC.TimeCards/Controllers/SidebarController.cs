using SBC.TimeCards.Common;
using SBC.TimeCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBC.TimeCards.Controllers
{
    [Authorize]
    public class SidebarController : Controller
    {
        // GET: Sidebar
        public ActionResult Index()
        {
            if(User.IsInRole(AppRoles.Administrator))
                return PartialView("_Sidebar", Sidebar.AdminItems);
            return PartialView("_Sidebar", Sidebar.UserItems);
        }
    }
}