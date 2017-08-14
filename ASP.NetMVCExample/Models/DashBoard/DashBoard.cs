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
using ASP.NetMVCExample.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample.Models.DashBoard
{
    public class DashBoard
    {
        public List<SelectUserProjects_Result> ProjectData;
        public SelectTheUser_Result UsersProfileData;
    }
}