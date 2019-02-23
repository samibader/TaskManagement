﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBC.TimeCards.Service.Models.Users
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        [Remote("CheckUniqueEmail", "Users",ErrorMessage ="Email Already Taken !",HttpMethod ="Post")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Remote("CheckUniqueUsername", "Users", ErrorMessage = "Username Already Taken !", HttpMethod = "Post")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
