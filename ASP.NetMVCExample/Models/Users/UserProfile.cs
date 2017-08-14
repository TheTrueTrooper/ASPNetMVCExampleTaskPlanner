#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: August 1,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To Create a model to use in the Dashboard
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: ASP.net
//  Writer/Publisher: Microsoft
//  Link: https://www.asp.net/
//  }
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AngelASPExtentions.ExtraValidationAttributes.Files;
using System.Linq;
using System.Web;
using System.Drawing;

namespace ASP.NetMVCExample.Models.Users
{
    public class UserProfile
    {

        public HttpPostedFileBase ProfilePicture;

        [MaxLength(250)]
        public string Bio { get; set; }

        [Phone(ErrorMessage = "This is not a valid phone number.")]
        public string HomePhone { get; set; }
        [Phone(ErrorMessage = "This is not a valid phone number.")]
        public string CellPhone { get; set; }
        [Phone(ErrorMessage = "This is not a valid phone number.")]
        public string WorkPhone { get; set; }

    }
}