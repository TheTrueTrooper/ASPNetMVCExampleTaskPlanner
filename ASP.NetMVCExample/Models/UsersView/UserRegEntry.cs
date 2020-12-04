using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NetMVCExample.Models.UsersView
{
    public class UserRegEntry
    {
        [DisplayName("First Name"), StringLength(50), Required(ErrorMessage = "First Name field is required")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name or Initial"), StringLength(50)]
        public string MiddleInitial { get; set; }
        [DisplayName("Last Name"), StringLength(50), Required(ErrorMessage = "Last Name field is required")]
        public string LastName { get; set; }
        [DisplayName("Email"), Remote("VerifyFreeEmail", "UtilitiesAPI"), EmailAddress(ErrorMessage = "Entered email value is not a valid email."), Required(ErrorMessage = "Email field is required")]
        public string Email { get; set; }
        [DisplayName("Password"), PasswordPropertyText, Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
        [DisplayName("Confirm Password"), System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The Passwords do not match."), PasswordPropertyText, Required(ErrorMessage = "Password field is required")]
        public string ConfirmPassword { get; set; }
        [StringLength(50)]
        public string Salt { get; set; }
        [DisplayName("Primary Phone Number"), StringLength(14), Phone(ErrorMessage = "Enter phone number value is not a valid phone number."), Required(ErrorMessage = "Home phone field is required")]
        public string PrimaryPhoneNumber { get; set; }
    }
}