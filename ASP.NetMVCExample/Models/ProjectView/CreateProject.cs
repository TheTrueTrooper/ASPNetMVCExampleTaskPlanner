#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: Sep 1,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To help control the calls relating companies
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources/References: 
//  {
//  Name: ASP.net
//  Writer/Publisher: Microsoft
//  Link: https://www.asp.net/
//  }
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP.NetMVCExample.Models.ProjectView
{
    public class CreateProject
    {
        [DisplayName("Project Name"), Required, MaxLength(70)]
        public string ProjectName { get; set; }
        [DisplayName("Company")]
        public int? CompanyID { get; set; }
        [DisplayName("Manager"), Required]
        public int? ManagerID { get; set; }
        [DisplayName("Address"), Required, MaxLength(30)]
        public string Address { get; set; }
        [DisplayName("PostalCode"), Required]
        public string PostalCode { get; set; }
        [DisplayName("Country"), Required, MaxLength(10)]
        public string Country { get; set; }
        [DisplayName("Province"), Required, MaxLength(10)]
        public string Province { get; set; }
        [DisplayName("City"), Required, MaxLength(10)]
        public string City { get; set; }
        [DisplayName("Description"), Required, MaxLength(250)]
        public string Description { get; set; }
        [DisplayName("Start Date"), Required, DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayName("End Date"), Required, DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EndDate { get; set; }
    }
}