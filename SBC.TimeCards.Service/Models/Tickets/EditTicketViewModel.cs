using SBC.TimeCards.Service.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Tickets
{
    public class EditTicketViewModel
    {
        public int Id { get; set; }
        public int? AssigneeId { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public bool IsSubTask { get; set; }
        public TicketViewModel ParentTicketInfo { get; set; }
        public ProjectViewModel ProjectInfo { get; set; }
        public int StateId { get; set; }
    }
}
