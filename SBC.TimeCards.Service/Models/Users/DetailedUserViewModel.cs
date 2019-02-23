using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Service.Models.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Users
{
    public class DetailedUserViewModel
    {
        [Display(Name = "User ID")]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime CreateDate { get; set; }

        public List<ProjectViewModel> Projects { get; set; }
    }
}
