#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
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
using ASP.NetMVCExample.Models;
using System.Data.Entity.Core.Objects;

namespace ASP.NetMVCExample.Controllers
{
    [DBFSPAuthorize]
    public class ProjectController : Controller
    {
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();
        // GET: Project
        public ActionResult Index(int ID)
        {
            SelectProjectByID_Result Project = null;
            using (ObjectResult<SelectProjectByID_Result> Result = DB.SelectProjectByID(ID))
                Project = Result.First();
            return View(Project);
        }

        
        public ActionResult CreateProject(CreateProject ProjectToCreate)
        {
            List<SelectListItem> UserList = new List<SelectListItem>{ new SelectListItem { Text = "Me", Value = ((int)Session["SessionUserID"]).ToString() } };
            ViewBag.UserList = UserList;
            ProjectToCreate.StartDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                 
            }
            return View();
        }
    }
}