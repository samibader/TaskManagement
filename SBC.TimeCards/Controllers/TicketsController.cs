using SBC.TimeCards.Service.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBC.TimeCards.Controllers
{
    public class TicketsController : BaseController
    {
        public TicketsController(ApplicationUserManager userManager) : base(userManager)
        {

        }
        // GET: Tickets
        public ActionResult Index()
        {
            return View();
        }
    }
}