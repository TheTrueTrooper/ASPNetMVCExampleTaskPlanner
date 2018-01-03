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
using System.Web.Mvc;
using ASP.NetMVCExample.SecurityValidation;
using ASP.NetMVCExample.Models.ProjectView;
using ASP.NetMVCExample.Models._SecurityOptions;
using ASP.NetMVCExample.Models;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using AngelASPExtentions.ASPMVCControllerExtentions;
using System.Globalization;
using AngelASPExtentions.ExtraExtentions.Strings;
using System.Threading;

namespace ASP.NetMVCExample.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [DBFSPAuthorize]
    public class ProjectController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();

        /// <summary>
        /// Gets the proPrivs
        /// </summary>
        /// <param name="ItemID">The Project you are tring to access</param>
        /// <returns></returns>
        [NonAction]
        ProjectPrivages GetProjectPriv(int ItemID)
        {
            int ID = (int)Session["SessionUserID"]; //Simply validate code agenst session
            string Code = (string)Session["SessionCode"];

            ProjectPrivages ProjectPriv = new ProjectPrivages();

            using (ObjectResult<ValidateWithProjectViewPriv_Result> Result = DB.ValidateWithProjectViewPriv(ID, Code, ItemID))
                ProjectPriv.In(Result.First());

            return ProjectPriv;
        }

        // GET: Project
        /// <summary>
        /// Gets a Project interface page
        /// </summary>
        /// <param name="ID">THe Projects ID</param>
        /// <returns>the Page if the Project if you have Privs</returns>
        [DBFSPAuthorize]
        public ActionResult Index(int ID)
        {
            ProjectPrivages PP = GetProjectPriv(ID);

            if (PP.CanView)
            {
                ViewBag.Priv = this.GetJsonAsString(PP);

                SelectProjectByID_Result Project = null;
                using (ObjectResult<SelectProjectByID_Result> Result = DB.SelectProjectByID(ID))
                    Project = Result.First();
                return View(Project);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult IndexSubPage(string AngularView)
        {
            switch(AngularView)
            {
                case "OverView":
                    return View("_AJSV_Index_OverView");
                case "TaskCellView":
                    return View("_AJSV_Index_TaskCellView");
                case "GanttChartView":
                    return View("_AJSV_Index_GanttChartView");
                case "PerkChartView":
                    return View("_AJSV_Index_PerkChartView");
                default:
                    return View("FriendlyErr");
            }
        }


        public ActionResult CreateProject(CreateProject ProjectToCreate)
        {
            string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
                    "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy"};

            string ErrorMessage = "";
            ObjectParameter ErrorMessageParameter = new ObjectParameter("ErrorMessage", ErrorMessage);

            int ID = 0;
            ObjectParameter IDParameter = new ObjectParameter("OutID", ID);

            List<SelectListItem> UserList = new List<SelectListItem>{ new SelectListItem { Text = "Me", Value = ((int)Session["SessionUserID"]).ToString() } };
            ViewBag.UserList = UserList;

            bool EndSuccess = false, StartSuccess = false;

            if (!ProjectToCreate.StartDateIn.IsNullEmptyOrWhiteSpace())
            {
                DateTime StartDate;
                StartSuccess = DateTime.TryParseExact(ProjectToCreate.StartDateIn, formats, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out StartDate);
                ViewBag.StartSuccess = StartSuccess;
                ProjectToCreate.StartDate = StartDate;
            }

            if (!ProjectToCreate.StartDateIn.IsNullEmptyOrWhiteSpace())
            {
                DateTime EndDate;
                EndSuccess = DateTime.TryParseExact(ProjectToCreate.EndDateIn, formats, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out EndDate);
                ViewBag.EndSuccess = EndSuccess;
                ProjectToCreate.EndDate = EndDate;
            }


            if (ModelState.IsValid && StartSuccess && EndSuccess)
            {
                 DB.CreateProject(ProjectToCreate.ProjectName, ProjectToCreate.CompanyID, 
                    ProjectToCreate.ManagerID, ProjectToCreate.Address, ProjectToCreate.PostalCode,
                    ProjectToCreate.Country, ProjectToCreate.Province, ProjectToCreate.City,
                    ProjectToCreate.Description, ProjectToCreate.StartDate, ProjectToCreate.EndDate, 
                    ErrorMessageParameter, IDParameter);

                if(ErrorMessage.IsNullEmptyOrWhiteSpace())
                    return RedirectToAction("Index", "Project", new { ID = IDParameter.Value});
            }
            return View();
        }
    }
}
