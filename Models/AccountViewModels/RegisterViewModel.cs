using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lastVersionAuthe.Models.AccountViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "EmailErorr")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "NameLength", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "ConfirmPasswordErorr")]
        public string ConfirmPassword { get; set; }
    }
}
