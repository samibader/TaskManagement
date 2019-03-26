using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
    public class ServerTemplate
    {
        public ServerTemplate()
        {
            ServerNetworkTemplates = new List<ServerNetworkTemplate>();
            ServerDiskTemplates = new List<ServerDiskTemplate>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ram { get; set; }
        public string Cpu { get; set; }
        public virtual TicketTemplate TicketTemplate { get; set; }
        public virtual IList<ServerNetworkTemplate> ServerNetworkTemplates { get; set; }
        public virtual IList<ServerDiskTemplate> ServerDiskTemplates { get; set; }

    }
}
