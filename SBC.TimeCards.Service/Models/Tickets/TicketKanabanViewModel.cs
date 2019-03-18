using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Tickets
{
    public class TicketKanabanViewModel
    {
        public List<TicketViewModel> ActiveTickets { get; set; }
        public List<TicketViewModel> DoneTickets { get; set; }
        public List<TicketViewModel> DelayedTickets { get; set; }
        public List<TicketViewModel> AllTickets { get; set; }
    }
}
