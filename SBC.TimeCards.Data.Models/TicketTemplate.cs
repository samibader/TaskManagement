using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
    public class TicketTemplate
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int TemplateTypeId { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual TemplateType TemplateType { get; set; }
        public virtual ServerTemplate ServerTemplate { get; set; }
        public virtual DeviceTemplate DeviceTemplate { get; set; }
        public virtual NetworkTemplate NetworkTemplate { get; set; }
        public virtual UserTemplate UserTemplate { get; set; }
    }
}
