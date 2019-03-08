using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Models
{
    public class TicketState
    {
        public TicketState()
        {
            Tickets = new List<Ticket>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
    }
}
