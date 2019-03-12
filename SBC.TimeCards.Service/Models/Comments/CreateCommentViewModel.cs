using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Comments
{
    public class CreateCommentViewModel
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
