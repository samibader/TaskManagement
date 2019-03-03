using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Models
{
    public class Project
    {
        public Project()
        {
            Attachments = new List<Attachment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }

        public bool IsArchived { get; set; }
        public string Color { get; set; }

        public DateTime? ArchiveDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual IList<Attachment> Attachments { get; set; }
        public virtual IList<User> UserFavorites { get; set; }

    }
}
