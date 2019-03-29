using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBC.TimeCards.Service.Models.Tickets;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Common;
using AutoMapper;
using SBC.TimeCards.Service.Models.Comments;
using SBC.TimeCards.Service.Models.Notificatoion;

namespace SBC.TimeCards.Service.Services
{
    public class TicketsService : ITicketsService
    {

        private readonly IUnitOfWork _unitOfWork;

        public event EventHandler TicketUpdated;
        protected virtual void AssineeChanged(NotificationArgs e)
        {
            var t = Mapper.Map<Ticket, TicketViewModel>(_unitOfWork.Tickets.GetById(e.TicketId.Value));
            e.Body = "You Have been Assigned Ticket " + t.Title;
            e.Subject = "Progress on Project " + t.ProjectInfo.Name;
            e.ProjcetId = t.ProjectInfo.Id;
            TicketUpdated?.Invoke(this, e);
        }
        protected virtual void TicketDone(NotificationArgs e)
        {
            var t = Mapper.Map<Ticket, TicketViewModel>(_unitOfWork.Tickets.GetById(e.TicketId.Value));
            e.Body = "Ticket" + t.Title +"of User "+t.Assignee.Name+" Marked As done ";
            e.Subject = "Progress on Project " + t.ProjectInfo.Name;
            e.ProjcetId = t.ProjectInfo.Id;
            TicketUpdated?.Invoke(this, e);
        }
        protected virtual void TicketUnDone(NotificationArgs e)
        {
            var t = Mapper.Map<Ticket, TicketViewModel>(_unitOfWork.Tickets.GetById(e.TicketId.Value));
            e.Body = "Ticket" + t.Title + "of User " + t.Assignee.Name + " Marked As In Progress ";
            e.Subject = "Progress on Project " + t.ProjectInfo.Name;
            e.ProjcetId = t.ProjectInfo.Id;
            TicketUpdated?.Invoke(this, e);
        }
        public TicketsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EditTicketViewModel GetTicketForEdit(int id)
        {
            return Mapper.Map<Ticket, EditTicketViewModel>(_unitOfWork.Tickets.GetById(id));
        }

        public int Init(CreateTicketViewModel model)
        {
            if (model.ProjectId.HasValue && model.ParentTicketId.HasValue)
            {
                throw new InvalidOperationException("Ambigus");
            }
            var ticket = new Ticket
            {
                ParentTicketId = model.ParentTicketId,
                ProjectId = model.ProjectId,
                OwnerId = model.OwnerId.Value,
                CreatoionDate = GlobalSettings.CURRENT_DATETIME.Date,
                StateId = (int)TicketStates.Active
            };
            _unitOfWork.Tickets.Add(ticket);
            _unitOfWork.SaveChanges();
            return ticket.Id;
        }

        public void UpdateTitle(int id, string title)
        {
            var ticket = _unitOfWork.Tickets.GetById(id);
            ticket.Title = title;
            _unitOfWork.Tickets.Update(ticket);
            _unitOfWork.SaveChanges();
        }
        public void UpdateDescription(int id, string desc)
        {
            var ticket = _unitOfWork.Tickets.GetById(id);
            ticket.Description = desc;
            _unitOfWork.Tickets.Update(ticket);
            _unitOfWork.SaveChanges();
        }
        public void UpdateAssignee(int id, int? assigneeId)
        {
            var ticket = _unitOfWork.Tickets.GetById(id);
            ticket.AssigneeId = assigneeId;
            _unitOfWork.Tickets.Update(ticket);
            _unitOfWork.SaveChanges();
            AssineeChanged(new NotificationArgs { To = _unitOfWork.Users.GetById(assigneeId.Value).Email, TicketId = ticket.Id });
        }
        public void UpdateDueDate(int id, DateTime? dueDate)
        {
            var ticket = _unitOfWork.Tickets.GetById(id);
            ticket.DueDate = dueDate;
            if (ticket.DueDate.HasValue)
            {
                if (ticket.DueDate.Value.Date <= GlobalSettings.CURRENT_DATETIME.Date)
                    ticket.StateId = (int)TicketStates.Delayed;
                else
                    ticket.StateId = (int)TicketStates.Active;
            }
            _unitOfWork.Tickets.Update(ticket);
            _unitOfWork.SaveChanges();
        }
        public void MarkDone(int id)
        {
            var ticket = _unitOfWork.Tickets.GetById(id);
            ticket.StateId = (int)TicketStates.Done;
            _unitOfWork.Tickets.Update(ticket);
            _unitOfWork.SaveChanges();
            TicketDone(new NotificationArgs { TicketId = id, To = _unitOfWork.Users.GetById(ticket.Owner.Id).Email });
        }
        public void MarkUnDone(int id)
        {
            var ticket = _unitOfWork.Tickets.GetById(id);
            ticket.StateId = (int)TicketStates.Active;
            _unitOfWork.Tickets.Update(ticket);
            _unitOfWork.SaveChanges();
            TicketUnDone(new NotificationArgs { TicketId = id, To = _unitOfWork.Users.GetById(ticket.Owner.Id).Email });

        }
        public TicketKanabanViewModel GetKanabanByProjectId(int projectId)
        {
            var project = _unitOfWork.Projects.GetById(projectId);
            return GetKanaban(project.Tickets.ToList());

        }
        private TicketKanabanViewModel GetKanaban(List<Ticket> tickets)
        {
            var res = new TicketKanabanViewModel
            {
                ActiveTickets = Mapper.Map<List<Ticket>, List<TicketViewModel>>(tickets.Where(x => x.StateId == (int)TicketStates.Active).ToList()),
                DoneTickets = Mapper.Map<List<Ticket>, List<TicketViewModel>>(tickets.Where(x => x.StateId == (int)TicketStates.Done).ToList()),
                DelayedTickets = Mapper.Map<List<Ticket>, List<TicketViewModel>>(tickets.Where(x => x.StateId == (int)TicketStates.Delayed).ToList())
            };
            res.AllTickets = new List<TicketViewModel>();
            res.AllTickets.AddRange(res.ActiveTickets);
            res.AllTickets.AddRange(res.DelayedTickets);
            res.AllTickets.AddRange(res.DoneTickets);
            res.AllTickets.OrderBy(x => x.DueDate);
            return res;
        }
        public TicketKanabanViewModel GetKanabanByTicketId(int ticketId)
        {
            var ticket = _unitOfWork.Tickets.GetById(ticketId);
            return GetKanaban(ticket.SubTickets.ToList());
        }
        public void AddComment(CreateCommentViewModel model)
        {
            var comment = new Comment
            {
                CreationDate = GlobalSettings.CURRENT_DATETIME,
                UserId = model.UserId,
                Text = model.Text,
                TicketId = model.TicketId
            };
            _unitOfWork.Comments.Add(comment);
            _unitOfWork.SaveChanges();
        }
        public List<CommentViewModel> GetComments(int id)
        {
            return Mapper.Map<List<Comment>, List<CommentViewModel>>(_unitOfWork.Tickets.GetById(id).Comments.ToList());
        }
        public void Delete(int id)
        {
            var ticket = _unitOfWork.Tickets.GetById(id);
            var subtickets = ticket.SubTickets.ToList();
            foreach (var item in subtickets)
            {
                _unitOfWork.Tickets.Remove(item);
            }
            _unitOfWork.Tickets.Remove(ticket);
            _unitOfWork.SaveChanges();
        }
        public List<TicketViewModel> GetUserTickets(int userId)
        {
            return Mapper.Map<List<Ticket>, List<TicketViewModel>>(_unitOfWork.Tickets.GetBy(x => x.AssigneeId == userId).ToList());
        }

        public bool ValidateTemplates(int ticketId)
        {
            var ticket = _unitOfWork.Tickets.GetById(ticketId);
            var valid = false;
            valid |= ticket.TicketTemplates.Any(x => x.DeviceTemplate != null && (String.IsNullOrEmpty(x.DeviceTemplate.Ip) || String.IsNullOrEmpty(x.DeviceTemplate.Location) || String.IsNullOrEmpty(x.DeviceTemplate.Name) || String.IsNullOrEmpty(x.DeviceTemplate.Type)));
            valid |= ticket.TicketTemplates.Any(x => x.NetworkTemplate != null && (String.IsNullOrEmpty(x.NetworkTemplate.Ip) || String.IsNullOrEmpty(x.NetworkTemplate.Subnet) || String.IsNullOrEmpty(x.NetworkTemplate.Zone) || String.IsNullOrEmpty(x.NetworkTemplate.DefaultGateway)));
            valid |= ticket.TicketTemplates.Any(x => x.UserTemplate != null && (String.IsNullOrEmpty(x.UserTemplate.Name) || String.IsNullOrEmpty(x.UserTemplate.ExpirationDate) || String.IsNullOrEmpty(x.UserTemplate.OU) || String.IsNullOrEmpty(x.UserTemplate.Password) || String.IsNullOrEmpty(x.UserTemplate.Role)));
            valid |= ticket.TicketTemplates.
                Any(x => x.ServerTemplate != null && (String.IsNullOrEmpty(x.ServerTemplate.Name) || String.IsNullOrEmpty(x.ServerTemplate.Ram) || String.IsNullOrEmpty(x.ServerTemplate.Cpu)
                || x.ServerTemplate.ServerDiskTemplates.Any(y => String.IsNullOrEmpty(y.Value))
                || x.ServerTemplate.ServerNetworkTemplates
                .Any(y => String.IsNullOrEmpty(y.Ip) || String.IsNullOrEmpty(y.Subnet) || String.IsNullOrEmpty(y.Zone) || String.IsNullOrEmpty(y.DefaultGateway))));
            return !valid;
        }
    }
}
