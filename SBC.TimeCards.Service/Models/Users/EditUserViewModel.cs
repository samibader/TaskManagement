using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBC.TimeCards.Service.Models.Users
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Remote("CheckUniqueEmail", "Users", ErrorMessage = "Email Already Taken !", HttpMethod = "Post", AdditionalFields ="Id")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Remote("CheckUniqueUsername", "Users", ErrorMessage = "Username Already Taken !", HttpMethod = "Post", AdditionalFields = "Id")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display (Name="Role")]
        public int RoleId { get; set; }
    }
}
