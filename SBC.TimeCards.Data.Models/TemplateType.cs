using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
    public class TemplateType
    {
        public TemplateType()
        {
            TicketTemplates = new List<TicketTemplate>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<TicketTemplate> TicketTemplates { get; set; }

    }
}
