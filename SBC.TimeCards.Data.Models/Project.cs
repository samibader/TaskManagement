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
            Users = new List<User>();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual IList<User> Users { get; set; }
    }
}
