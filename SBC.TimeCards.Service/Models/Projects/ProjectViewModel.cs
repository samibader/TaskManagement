using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Projects
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Color { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public string Description { get; set; }
    }
}
