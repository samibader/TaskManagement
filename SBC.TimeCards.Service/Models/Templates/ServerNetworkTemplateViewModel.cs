using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Templates
{
   public class ServerNetworkTemplateViewModel
    {
        public int Id { get; set; }
        public int ServerTemplateId { get; set; }
        public string Ip { get; set; }
        public string DefaultGateway { get; set; }
        public string Zone { get; set; }
        public string Subnet { get; set; }

    }
}
