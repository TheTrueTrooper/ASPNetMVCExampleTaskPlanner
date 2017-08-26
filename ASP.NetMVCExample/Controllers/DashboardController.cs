#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To help control the calls relating high level account management (changes and extras)
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources/References: 
//  {
//  Name: ASP.net
//  Writer/Publisher: Microsoft
//  Link: https://www.asp.net/
//  }
#endregion
using AngelASPExtentions.ExtraExtentions;
using ASP.NetMVCExample._Helpers;
using ASP.NetMVCExample.Models;
using ASP.NetMVCExample.Models.DashBoard;
using ASP.NetMVCExample.SecurityValidation;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ASP.NetMVCExample.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [DBFSPAuthorize]
    public class DashboardController : Controller
    {
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();
        // GET: Dashboard
        /// <summary>
        /// returns the dash board page from which we can move around from
        /// </summary>
        /// <returns>the page</returns>
        public ActionResult Index()
        {

            string ErrorMessage = "";
            ObjectParameter ErrorMessageParameter = new ObjectParameter("ErrorMessage", ErrorMessage);

            DashBoardView Dash = new DashBoardView();
            //Get a list of results to use for the quick navagation
            using (ObjectResult<SelectUserProjects_Result> TempResults = DB.SelectUserProjects((int)Session["SessionUserID"], ErrorMessageParameter))
                Dash.ProjectData = TempResults.ToList();

            if (((string)ErrorMessageParameter.Value).Trim() != "")
                return View(@"~\Shared\FriendlyErr.cshtml");

            ErrorMessage = "";
            ErrorMessageParameter = new ObjectParameter("ErrorMessage", ErrorMessage);

            using (ObjectResult<SelectTheUser_Result> TempResults2 = DB.SelectTheUser((int)Session["SessionUserID"], ErrorMessageParameter))
                Dash.UsersProfileData = TempResults2.First();

            if (((string)ErrorMessageParameter.Value).Trim() != "")
                return View(@"~\Shared\FriendlyErr.cshtml");

            if (Dash.UsersProfileData.Picture == null)
                Dash.UsersProfileData.Picture = new Bitmap(HostingEnvironment.MapPath("~/Images/NoProfilePicPic.jpg")).ToByteArray();
            

            //pass to the page
            return View(Dash);
        }


    }
}