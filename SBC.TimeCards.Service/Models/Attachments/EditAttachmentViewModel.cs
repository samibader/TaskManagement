using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Attachments
{
    public class EditAttachmentViewModel
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
    }
}
