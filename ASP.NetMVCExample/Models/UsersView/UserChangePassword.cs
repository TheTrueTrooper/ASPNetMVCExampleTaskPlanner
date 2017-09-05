using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample.Models.UsersView
{
    public class UserChangePassword
    {
        [DisplayName("Password"), PasswordPropertyText, Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set; }
        [DisplayName("New Password"), PasswordPropertyText, Required(ErrorMessage = "The New Password field is required")]
        public string NewPassword { get; set; }
        [DisplayName("Confirm Password"), Compare("NewPassword", ErrorMessage = "The Passwords do not match."), PasswordPropertyText, Required(ErrorMessage = "This field is required")]
        public string NewConfirmPassword { get; set; }
    }
}