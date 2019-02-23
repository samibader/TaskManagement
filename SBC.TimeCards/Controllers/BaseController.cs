using System;
using System.Collections.Generic;
using System.Data;
using SBC.TimeCards.Common;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Service.Identity;
using System.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace SBC.TimeCards.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ApplicationUserManager _userManager;
        public BaseController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        protected string GetCurrentUserRole()
        {
            if (User.IsInRole(AppRoles.Administrator))
                return AppRoles.Administrator;
            else
                return AppRoles.User;
        }
        protected async Task<int> GetCurrentUserId()
        {
            return (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
        }

    }
}