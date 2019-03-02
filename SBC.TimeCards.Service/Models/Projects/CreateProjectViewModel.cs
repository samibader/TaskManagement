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
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
