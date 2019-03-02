using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Service.Models.Attachments;
using System.IO;
using SBC.TimeCards.Service.Services;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Common;

namespace SBC.TimeCards.Controllers
{
    public class AttachmentsController : BaseController
    {
        private readonly IAttachmentsService _attachmentsService;
        public AttachmentsController(AttachmentsService attachmentService, ApplicationUserManager userManager) : base(userManager)
        {
            _attachmentsService = attachmentService;
        }

        // GET: Attachments
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateAttachmentViewModel model, HttpPostedFileBase attachment)
        {
            if (attachment.ContentLength > 0 && ModelState.IsValid)
            {
                var fileName = UploadFile(attachment);
                model.FileName = fileName;
                _attachmentsService.Create(model);
                return new RedirectResult(Url.Action("Manage", "Projects", new { id = model.Id }) + "#attachments");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var attachment = _attachmentsService.GetAttachmentForEdit(id);
            return View(attachment);
        }

        [HttpPost]
        public ActionResult Edit(EditAttachmentViewModel model, HttpPostedFileBase attachment)
        {
            if (ModelState.IsValid) {

                if (attachment!=null && attachment.ContentLength >0)
                {
                    var fileName = UploadFile(attachment);
                    model.FileName = fileName;
                }
                _attachmentsService.Edit(model);
                return new RedirectResult(Url.Action("Manage", "Projects", new { id = model.Id }) + "#attachments");
            }
            return View(model);
        }
        public ActionResult List(int projectId)
        {
            var attachemnts = _attachmentsService.GetProjectAttachments(projectId);
            return View(attachemnts);

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _attachmentsService.Delete(id);
            return Json(new { success = true});
        }
    }
}