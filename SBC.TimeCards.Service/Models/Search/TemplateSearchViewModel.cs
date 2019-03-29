using SBC.TimeCards.Service.Models.Templates;
using SBC.TimeCards.Service.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Search
{
    public class TemplateSearchViewModel
    {
        public TemplateViewModel Template { get; set; }
        public TicketViewModel Ticket { get; set; }
    }
}
