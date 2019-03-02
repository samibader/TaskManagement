using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Attachments
{
    public class CreateAttachmentViewModel
    {
        public string Title { get; set; }
       
        /// <summary>
        /// The project Id
        /// </summary>
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
    }
}
