using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
    public class UserTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ExpirationDate { get; set; }
        public string OU { get; set; }

        public virtual TicketTemplate TicketTemplate { get; set; }
    }
}
