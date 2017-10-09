﻿#region WritersSigniture
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

namespace ASP.NetMVCExample.Controllers
{
    [DBFSPAuthorize]
    public class ProjectController : Controller
    {
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();

        [NonAction]
        ProjectPrivages GetProjectPriv(int ItemID)
        {
            int ID = (int)Session["SessionUserID"]; //Simply validate code agenst session
            string Code = (string)Session["SessionCode"];

            ProjectPrivages ProjectPriv = new ProjectPrivages();

            using (MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2())
            using (ObjectResult<ValidateWithProjectViewPriv_Result> Result = DB.ValidateWithProjectViewPriv(ID, Code, ItemID))
                ProjectPriv.In(Result.First());

            return ProjectPriv;
        }

        // GET: Project
        [DBFSPAuthorize]
        public ActionResult Index(int ID)
        {
            ProjectPrivages PP = GetProjectPriv(ID);

            if (PP.CanView)
            {
                ViewBag.Priv = PP;

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
            string ErrorMessage = "";
            ObjectParameter ErrorMessageParameter = new ObjectParameter("ErrorMessage", ErrorMessage);

            int ID = 0;
            ObjectParameter IDParameter = new ObjectParameter("ErrorMessage", ID);

            List<SelectListItem> UserList = new List<SelectListItem>{ new SelectListItem { Text = "Me", Value = ((int)Session["SessionUserID"]).ToString() } };
            ViewBag.UserList = UserList;
            ProjectToCreate.StartDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                DB.CreateProject(ProjectToCreate.ProjectName, ProjectToCreate.CompanyID, 
                    ProjectToCreate.ManagerID, ProjectToCreate.Address, ProjectToCreate.PostalCode,
                    ProjectToCreate.Country, ProjectToCreate.ProjectName, ProjectToCreate.City,
                    ProjectToCreate.Description, ProjectToCreate.StartDate, ProjectToCreate.EndDate, 
                    ErrorMessageParameter, IDParameter);
            }
            return View();
        }
    }
}