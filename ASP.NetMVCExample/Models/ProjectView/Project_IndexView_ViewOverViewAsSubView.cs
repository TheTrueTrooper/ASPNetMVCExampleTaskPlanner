#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: Sept 18,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To crate a view that can hold the picture as a string
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
using AngelASPExtentions.ExtraExtentions.ImageExtentions;

namespace ASP.NetMVCExample.Models.ProjectView
{
    public class Project_IndexView_ViewOverViewAsSubView
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectAddress { get; set; }
        public string ProjectCity { get; set; }
        public string ProjectProvince { get; set; }
        public string ProjectCountry { get; set; }
        public string ProjectPostalCode { get; set; }
        public System.DateTime ProjectStartDate { get; set; }
        public System.DateTime ProjectEndDate { get; set; }
        public Nullable<System.DateTime> ProjectActualStartDate { get; set; }
        public Nullable<System.DateTime> ProjectActualEndDate { get; set; }
        public string ProjectDescription { get; set; }
        public System.DateTime ProjectCreationDate { get; set; }
        public Nullable<int> ManagerID { get; set; }
        public string ManagerPicture { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerMiddleInitial { get; set; }
        public string ManagerLastName { get; set; }
        public string ManagerHomePhone { get; set; }
        public string ManagerWorkPhone { get; set; }
        public string ManagerCellPhone { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanySite { get; set; }

        public Project_IndexView_ViewOverViewAsSubView(SelectProjectByID_Result DivFrom)
        {
            ProjectID = DivFrom.ProjectID;
            ProjectName = DivFrom.ProjectName;
            ProjectAddress = DivFrom.ProjectAddress;
            ProjectCity = DivFrom.ProjectCity;
            ProjectProvince = DivFrom.ProjectProvince;
            ProjectCountry = DivFrom.ProjectCountry;
            ProjectPostalCode = DivFrom.ProjectPostalCode;
            ProjectStartDate = DivFrom.ProjectStartDate;
            ProjectEndDate = DivFrom.ProjectEndDate;
            ProjectActualStartDate = DivFrom.ProjectActualStartDate;
            ProjectActualEndDate = DivFrom.ProjectActualEndDate;
            ProjectDescription = DivFrom.ProjectDescription;
            ProjectCreationDate = DivFrom.ProjectCreationDate;
            ManagerID = DivFrom.ManagerID;
            ManagerPicture = DivFrom.ManagerPicture.ToBase64ImageString();
            ManagerFirstName = DivFrom.ManagerFirstName;
            ManagerMiddleInitial = DivFrom.ManagerMiddleInitial;
            ManagerLastName = DivFrom.ManagerLastName;
            ManagerHomePhone = DivFrom.ManagerHomePhone;
            ManagerWorkPhone = DivFrom.ManagerWorkPhone;
            ManagerCellPhone = DivFrom.ManagerCellPhone;
            CompanyID = DivFrom.CompanyID;
            CompanyName = DivFrom.CompanyName;
            CompanySite = DivFrom.CompanySite;
    }


    }
}