using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
    public class NetworkTemplate
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Subnet { get; set; }
        public string DefaultGateway { get; set; }
        public string Zone { get; set; }
        public virtual TicketTemplate TicketTemplate { get; set; }

    }
}
