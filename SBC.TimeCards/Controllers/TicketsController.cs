using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Models.Comments;
using SBC.TimeCards.Service.Models.Tickets;
using SBC.TimeCards.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBC.TimeCards.Controllers
{
    public class TicketsController : BaseController
    {
        private readonly ITicketsService _ticketService;
        private readonly IUserService _userService;
        public TicketsController(ApplicationUserManager userManager, TicketsService ticketService,UserService userService) : base(userManager)
        {
            _ticketService = ticketService;
            _userService = userService;
        }
        // GET: Tickets
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateTicketViewModel model)
        {
            model.OwnerId = GetCurrentUserId();
            var id = _ticketService.Init(model);
            return RedirectToAction("Edit", new { id = id });
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ticket = _ticketService.GetTicketForEdit(id);
            if (ticket.AssigneeId.HasValue)
            {
                ViewBag.AssigneeId = _userService.GetAllAsSelectList(ticket.AssigneeId.Value);
            }
            else
            {
                ViewBag.AssigneeId = _userService.GetAllAsSelectList();
            }
            return View("_EditTicketModal",ticket);
        }
        [HttpPost]
        public ActionResult UpdateTitle(int id,string data)
        {
            _ticketService.UpdateTitle(id, data);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult UpdateDescription(int id, string data)
        {
            _ticketService.UpdateDescription(id, data);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult UpdateAssigneeId(int id, int? data)
        {
            _ticketService.UpdateAssignee(id, data);
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult UpdateDueDate(int id, DateTime data)
        {
            _ticketService.UpdateDueDate(id, data);
            return Json(new { success = true });
        }
        public ActionResult MarkDone(int id)
        {
            _ticketService.MarkDone(id);
            return Json(new { success = true });
        }
        public ActionResult MarkUnDone(int id)
        {
            _ticketService.MarkUnDone(id);
            return Json(new { success = true });
        }
        public ActionResult KanabanByTicket(int id)
        {
            var kanaban = _ticketService.GetKanabanByTicketId(id);
            return PartialView("_TicketsKanban", kanaban);
        }
        public ActionResult TicketsListByTicket(int id)
        {
            var kanaban = _ticketService.GetKanabanByTicketId(id);
            return PartialView("_TicketsList", kanaban);
        }
        public ActionResult KanabanByProject(int id)
        {
            var kanaban = _ticketService.GetKanabanByProjectId(id);
            return PartialView("_TicketsKanban", kanaban);
        }
        [HttpPost]
        public ActionResult AddComment(CreateCommentViewModel model)
        {
            model.UserId = GetCurrentUserId();
            _ticketService.AddComment(model);
            return Json(new { success = true });
        }
        [HttpGet]
        public ActionResult GetComments(int id)
        {
            var comments = _ticketService.GetComments(id);
            return PartialView("_Comments", comments);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _ticketService.Delete(id);
            return Json(new { success = true });
        }
        public ActionResult MyTickets()
        {
            var model = _ticketService.GetUserTickets(GetCurrentUserId());

            return View(model);
        }
    }
}