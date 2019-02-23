using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBC.TimeCards.Service.Models.Projects
{
    public class CreateProjectViewModel
    {
        [Required]
        [Display(Name = "Project Name")]
        [Remote("CheckUniqueProjectName", "Projects", ErrorMessage = "Duplicate Project Name !", HttpMethod = "Post")]
        public string Name { get; set; }

        [Display(Name = "Project Users")]
        public List<SelectListItem> Users { get; set; }
        public List<string> SelectedUsers { get; set; }


        public CreateProjectViewModel()
        {
            Users = new List<SelectListItem>();
            SelectedUsers = new List<string>();
        }
        
    }
}
