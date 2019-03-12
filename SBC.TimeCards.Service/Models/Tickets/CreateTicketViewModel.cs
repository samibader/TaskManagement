using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Tickets
{
    public class CreateTicketViewModel
    {  
        public int? OwnerId { get; set; }
        public int? ProjectId { get; set; }
        public int? ParentTicketId { get; set; }
    }
}
