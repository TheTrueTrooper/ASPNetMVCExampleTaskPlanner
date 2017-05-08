using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample.Models.Users
{
    public class UserLoginTry
    {
        [DisplayName("Email"), Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
        [DisplayName("Password"), PasswordPropertyText, Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        [StringLength(50)]
        public string Salt { get; set; }
    }
}