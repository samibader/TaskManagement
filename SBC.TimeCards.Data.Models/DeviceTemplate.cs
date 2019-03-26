using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
    
    public class DeviceTemplate
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public virtual TicketTemplate TicketTemplate { get; set; }

    }
}
