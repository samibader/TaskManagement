using SBC.TimeCards.Common;
using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Models.Templates;
using SBC.TimeCards.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBC.TimeCards.Controllers
{
    public class TemplatesController : BaseController
    {
        private readonly ITemplatesService _templatesService;
        public TemplatesController(TemplatesService templateService, ApplicationUserManager userManager) : base(userManager)
        {
            _templatesService = templateService;
        }
        // GET: Templates
        public ActionResult UpdateTemplate(TemplateViewModel model)
        {
            _templatesService.UpdateTemplate(model);
            return Json(new { success = true });
        }
        public ActionResult Init(TemplateViewModel model)
        {
            var res = _templatesService.Init(model);
            return RenderTemplate(res);
        }
        public ActionResult GetTemplates(int ticketId)
        {
            var res = _templatesService.GetTemplates(ticketId);
            return PartialView("_Templates", res);
        }
        public ActionResult RenderTemplate(TemplateViewModel model)
        {
            if (model.TemplateTypeId == (int)TemplateTypes.NetworkTemplate)
            {
                return PartialView("_NetworkTemplate", model);
            }
            else if (model.TemplateTypeId == (int)TemplateTypes.ServerTemplate)
            {
                return PartialView("_ServerTemplate", model);
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            _templatesService.Delete(id);
            return Json(new { success=true});
        }
        public ActionResult InitDisk(int id)
        {
            var res = _templatesService.InitDisk(id);
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Disks[{0}]", _templatesService.GetDisksCount(id)-1);
            return PartialView("_ServerDiskTemplate",res);
        }
        public ActionResult InitServerNetwork(int id)
        {
            var res = _templatesService.InitNetwork(id);
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Networks[{0}]", _templatesService.GetNetworkCount(id)-1);
            return PartialView("_ServerNetworkTemplate",res);
        }
        public ActionResult DeleteDisk(int id)
        {
            _templatesService.DeleteDisk(id);
            return Json(new { success = true });
        }
        public ActionResult DeleteNetwork(int id)
        {
            _templatesService.DeleteNetwork(id);
            return Json(new { success = true });
        }
    }
}