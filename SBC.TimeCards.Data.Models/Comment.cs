using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual User User { get; set; }
    }
}
