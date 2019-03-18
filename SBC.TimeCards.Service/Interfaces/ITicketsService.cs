using SBC.TimeCards.Service.Models.Comments;
using SBC.TimeCards.Service.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Interfaces
{
    public interface ITicketsService
    {
        /// <summary>
        /// initalize an empty ticket to be filled later
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="parentTaskId"></param>
        /// <returns>the id of the task</returns>
        int Init(CreateTicketViewModel model);
        EditTicketViewModel GetTicketForEdit(int id);
        void UpdateTitle(int id, string title);
        void UpdateDescription(int id, string desc);
        void UpdateAssignee(int id, int assigneeId);
        void UpdateDueDate(int id, DateTime dueDate);
        void MarkDone(int id);
        void MarkUnDone(int id);
        TicketKanabanViewModel GetKanabanByProjectId(int projectId);
        TicketKanabanViewModel GetKanabanByTicketId(int ticketId);
        void AddComment(CreateCommentViewModel model);
        List<CommentViewModel> GetComments(int id);
        void Delete(int id);
    }
}
