using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SBC.TimeCards.Data.Models
{
    public class Ticket
    {
        public Ticket()
        {
            SubTickets = new List<Ticket>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatoionDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int OwnerId { get; set; }
        public int? AssigneeId { get; set; }
        public int StateId { get; set; }
        public int? ProjectId { get; set; }
        public int? ParentTicketId { get; set; }
        public virtual User Owner { get; set; }
        public virtual User Assignee { get; set; }
        public virtual TicketState TicketState { get; set; }
        public virtual Ticket Parent { get; set; }
        public virtual Project Project { get; set; }
        public virtual IList<Ticket> SubTickets { get; set; }
        public virtual IList<Comment> Comments { get; set; }

    }
}
