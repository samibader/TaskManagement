using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Users
{
    public class UserViewModel
    {
        [Display(Name ="User ID")]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime CreateDate { get; set; }
    }
}
