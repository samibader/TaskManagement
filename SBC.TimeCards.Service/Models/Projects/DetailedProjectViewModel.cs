using SBC.TimeCards.Service.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Projects
{
    public class DetailedProjectViewModel
    {
        [Display(Name ="Project ID")]
        public int Id { get; set; }
        [Display(Name = "Project Name")]
        public string Name { get; set; }
        [Display(Name = "Assigned Users")]
        public List<UserViewModel> Users { get; set; }
    }
}
