using ASP.NetMVCExample.Models;
using ASP.NetMVCExample.Models.ProjectView;
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
    public class ProjectAPIController : Controller
    {
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();

        ///// <summary>
        ///// A severside Getter for a user's Projects that they are working on
        ///// </summary>
        ///// <param name="email"></param>
        ///// <returns></returns>
        //[AcceptVerbs("Get", "Post")]
        //public JsonResult ProjectData(int ID)
        //{
        //    if (Session["SessionUserID"] == null)
        //        return Json(@"Access Denied", JsonRequestBehavior.AllowGet);

        //    string ErrorMessage = "";
        //    ObjectParameter ErrorMessageParameter = new ObjectParameter("ErrorMessage", ErrorMessage);

        //    SelectProjectByID_Result ProjectData;
        //    using (ObjectResult<SelectProjectByID_Result> TempResults = DB.SelectProjectByID(ID))
        //        ProjectData = TempResults.First();

        //    if (ProjectData.ManagerPicture == null)
        //        ProjectData.ManagerPicture = new Bitmap(HostingEnvironment.MapPath("~/Images/NoProfilePicPic.jpg")).ToByteArray();

        //    Project_IndexView_ViewOverViewAsSubView Return = new Project_IndexView_ViewOverViewAsSubView(ProjectData);

        //    return Json(Return, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// A severside Getter for a Project's tasks
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AcceptVerbs("Get")]
        public JsonResult ProjectsTaskData(int ID)
        {
            Project_Tasks_And_Links Return = new Project_Tasks_And_Links(DB, ID);
            return Json(Return, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// A severside Getter for a Project's tasks
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [AcceptVerbs("Post")]
        public JsonResult ProjectsTaskData(CreateTask Task)
        {

            string ErrorMessage = "";
            ObjectParameter ErrorMessageParameter = new ObjectParameter("ErrorMessage", ErrorMessage);

            int ID = 0;
            ObjectParameter IDParameter = new ObjectParameter("OutID", ID);

            if (ModelState.IsValid)
            {
                DB.CreateTask(Task.SubContractorID, Task.TaskTypeID, Task.ProjectID, Task.Description, Task.ActualDurationAsTicks, IDParameter, ErrorMessageParameter);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }
    }
}