using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
    public class ServerDiskTemplate
    {
        public int Id { get; set; }
        public int ServerTemplateId { get; set; }
        public string Value { get; set; }
        public virtual ServerTemplate ServerTemplate { get; set; }

    }
}
