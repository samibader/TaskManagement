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
        #region Server
        public string ServerName { get; set; }
        public string Ram { get; set; }
        public string Cpu { get; set; }

        public List<ServerDiskTemplateViewModel> Disks { get; set; }
        public List<ServerNetworkTemplateViewModel> Networks { get; set; }
        #endregion
        #region User
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ExpirationDate { get; set; }
        public string OU { get; set; }
        #endregion
        #region Network
        public string NetworkIp { get; set; }
        public string Subnet { get; set; }
        public string DefaultGateway { get; set; }
        public string Zone { get; set; }
        #endregion
        #region Device
        public string Type { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string DeviceIp { get; set; }
        #endregion
    }
}
