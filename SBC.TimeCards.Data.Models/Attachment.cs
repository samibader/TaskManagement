using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
   public class Attachment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int Size { get; set; }
    }
}
