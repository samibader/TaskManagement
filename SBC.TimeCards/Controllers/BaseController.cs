using System;
using System.Collections.Generic;
using System.Data;
using SBC.TimeCards.Common;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Service.Identity;
using System.Security;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SBC.TimeCards.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using SBC.TimeCards.Service.Models;
using SBC.TimeCards.Service.Services;
using Hangfire;
using SBC.TimeCards.Models.Emails;
using SBC.TimeCards.Service.Models.Notificatoion;

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
            if (String.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
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

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
        protected void sendNoti(object sender, EventArgs e)
        {
            var model = e as NotificationArgs;
            var url = "";
            if (!model.TicketId.HasValue)
            {
                url = Url.Action("Manage", "Projects", new { id = model.ProjcetId });
            }
            else
            {
                url = Url.Action("Manage", "Projects", new { id = model.ProjcetId,tid=model.TicketId });
            }
            model.Body = String.Format("{0}<br>{1}</br>", model.Body, url);
            BackgroundJob.Enqueue(() => EmailNotificatioService.SendNotificationEmail(model.Subject, model.To, model.Body));

        }
    }
}