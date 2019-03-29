using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Notificatoion
{
    public class NotificationArgs: EventArgs
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string ProjectName { get; set; }
        public int ProjcetId { get; set; }
        public string TicketTitle { get; set; }
        public int? TicketId { get; set; }
        public string Body { get; set; }

    }
}
