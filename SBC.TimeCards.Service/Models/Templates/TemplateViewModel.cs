using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Templates
{
    public class TemplateViewModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int TemplateTypeId { get; set; }
        public string Ip { get; set; }
        public List<string> Disks { get; set; }
        public List<ServerNetworkTemplate> Networks { get; set; }
        public string ServerName { get; set; }


    }
}
