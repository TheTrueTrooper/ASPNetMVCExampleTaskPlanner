using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample.Models.Users
{

    public class UserPasswordReset
    {
        [DisplayName("Email"), Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
        public string Salt { get; set; }
        [DisplayName("Resset Code"), PasswordPropertyText, Required(ErrorMessage = "Resset Code")]
        public string ResetCode { get; set; }
        [DisplayName("New Password"), PasswordPropertyText, Required(ErrorMessage = "The New Password field is required")]
        public string NewPassword { get; set; }
        [DisplayName("Confirm Password"), Compare("NewPassword", ErrorMessage = "The Passwords do not match."), PasswordPropertyText, Required(ErrorMessage = "This field is required")]
        public string NewConfirmPassword { get; set; }
    }
}