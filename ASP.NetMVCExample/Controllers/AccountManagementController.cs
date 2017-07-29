#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To help control the calls relating high level account management (changes and extras)
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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NetMVCExample.Models.Users;
using ASP.NetMVCExample.SecurityValidation;
using ASP.NetMVCExample.Models;
using System.Data.Entity.Core.Objects;
using ASP.NetMVCExample._Helpers;

namespace ASP.NetMVCExample.Controllers
{
    /// <summary>
    /// Changes user accounts -- views not implmented
    /// </summary>
    [DBFSPAuthorize]
    public class AccountManagementController : Controller
    {
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();
        // GET: AccountManagement
        //will change preferences here not edit Data, so bits extra, but it will link to data edit 
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// gets the page to change the profile
        /// </summary>
        /// <returns>the page</returns>
        public ActionResult ChangeProfile()
        {
            return View();
        }

        /// <summary>
        /// Changes the Profile information
        /// </summary>
        /// <param name="Edits">the new profile information</param>
        /// <returns>the page or dash board</returns>
        public ActionResult ChangeProfile(UserEditProfile Edits)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter ErrorMessage = new ObjectParameter("ErrorMessage", typeof(string));
                int MyError = DB.UpdateTheUserInfo(int.Parse(Session["SessionUserID"] as string), null, null, null, Edits.HomePhone, Edits.CellPhone, Edits.WorkPhone, ErrorMessage);
                ViewBag.ErrorMessage = ErrorMessage.Value as string;
                if (MyError > 0)
                    return RedirectToAction("Index", "Dashboard");
            }
            return View(Edits);
        }

        /// <summary>
        /// gets the page to change password
        /// </summary>
        /// <returns>the page</returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// changes password
        /// </summary>
        /// <param name="Edits">the Model to use</param>
        /// <returns>The page to change or steos out</returns>
        public ActionResult ChangePassword(UserChangePassword Edits)
        {
            //if the model is not valid just fall though
            if(ModelState.IsValid)
            {
                //error message output for debug
                ObjectParameter ErrorMessage = new ObjectParameter("ErrorMessage", typeof(string));
                //change the password
                int MyError = DB.UpdateTheUserChangePassword(int.Parse(Session["SessionUserID"] as string), Edits.Password, Edits.NewPassword, ErrorMessage);
                ViewBag.ErrorMessage = ErrorMessage.Value as string;
                //if all is ok return back to the dashboard
                if (MyError > -1)
                    return RedirectToAction("Index", "Dashboard");
            }
            return View(Edits);
        }

        /// <summary>
        /// will edit your linked companies
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyConnection()
        {
            return View();
        }



    }
}