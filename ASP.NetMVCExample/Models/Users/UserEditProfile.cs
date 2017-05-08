using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample.Models.Users
{
    public class UserEditProfile
    {
        [Phone(ErrorMessage = "This is not a valid phone number.")]
        public string HomePhone { get; set; }
        [Phone(ErrorMessage = "This is not a valid phone number.")]
        public string CellPhone { get; set; }
        [Phone(ErrorMessage = "This is not a valid phone number.")]
        public string WorkPhone { get; set; }

    }
}