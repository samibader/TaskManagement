using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Attachments
{
    public class AttachmentViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public DateTime UploadDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public string SizeAsString { get; set; }

    }
}
