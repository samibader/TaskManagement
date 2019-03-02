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
using SBC.TimeCards.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace SBC.TimeCards.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = LanguageManager.GetDefaultLanguage();
                }
            }
             LanguageManager.SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }
        public ActionResult ChangeLanguage(string lang, string returnUrl)
        {
            LanguageManager.SetLanguage(lang);
            return Redirect(returnUrl);
        }
        protected readonly ApplicationUserManager _userManager;
        public BaseController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        protected string GetCurrentUserRole()
        {
            if (User.IsInRole(AppRoles.Administrator))
                return AppRoles.Administrator;
            return "";
        }
        //protected async Task<int> GetCurrentUserId()
        //{
        //    return (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
        //}
        protected string UploadFile(HttpPostedFileBase file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(Server.MapPath(GlobalSettings.UPLOADS_PATH), fileName);
            file.SaveAs(path);
            return fileName;
        }
        protected int GetCurrentUserId()
        {
            return Int32.Parse(User.Identity.GetUserId());
        }
    }
}