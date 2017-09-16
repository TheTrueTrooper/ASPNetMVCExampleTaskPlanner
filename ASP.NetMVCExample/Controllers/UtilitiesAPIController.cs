#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To help control the calls relating API utilities
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources/References: 
//  {
//  Name: ASP.net
//  Writer/Publisher: Microsoft
//  Link: https://www.asp.net/
//  }
#endregion
using ASP.NetMVCExample._Helpers;
using ASP.NetMVCExample.Models;
using ASP.NetMVCExample.SecurityValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace ASP.NetMVCExample.Controllers
{
    [DBFSPAuthorize]
    public class UtilitiesAPIController : Controller
    {
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();

        /// <summary>
        /// A severside validation sub page API so that we can get a post back as to vailidship
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public JsonResult VerifyFreeEmail(string email)
        {
            
            if (DB.IsEmailUsed(email).First().Value)
            {
                return Json("The email has been used already", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// A severside Getter for a user's Projects that they are working on
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Post")]
        public JsonResult GetUsersProjectData()
        {
            if (Session["SessionUserID"] == null)
                return Json(@"Access Denied", JsonRequestBehavior.AllowGet);

            string ErrorMessage = "";
            ObjectParameter ErrorMessageParameter = new ObjectParameter("ErrorMessage", ErrorMessage);

            List<SelectUserProjects_Result> UsersProjectData;
            using (ObjectResult<SelectUserProjects_Result> TempResults = DB.SelectUserProjects((int)Session["SessionUserID"], ErrorMessageParameter))
                UsersProjectData = TempResults.ToList();

            if (((string)ErrorMessageParameter.Value).Trim() != "")
                return Json(@"Error", JsonRequestBehavior.AllowGet);

            return Json(UsersProjectData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// A severside Getter for a user's Projects that they are working on
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Post")]
        public JsonResult ProjectData(int ID)
        {
            if (Session["SessionUserID"] == null)
                return Json(@"Access Denied", JsonRequestBehavior.AllowGet);

            string ErrorMessage = "";
            ObjectParameter ErrorMessageParameter = new ObjectParameter("ErrorMessage", ErrorMessage);

            SelectProjectByID_Result ProjectData;
            using (ObjectResult<SelectProjectByID_Result> TempResults = DB.SelectProjectByID(ID))
                ProjectData = TempResults.First();

            return Json(ProjectData, JsonRequestBehavior.AllowGet);
        }

    }
}
