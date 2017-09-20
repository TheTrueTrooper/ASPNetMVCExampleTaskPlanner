using ASP.NetMVCExample.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ASP.NetMVCExample.Models;
using ASP.NetMVCExample.Models._SecurityOptions;

namespace ASP.NetMVCExample.SecurityValidation
{
    /// <summary>
    /// this is a Data Base First Stored Procedure work around to the traditional Athorization
    /// basicly using the Session like tradtional Systems it uses session to log on agenst the database
    /// </summary>
    public class DBFSPAuthorizeAttribute : AuthorizeAttribute
    {
        public enum SecurityType
        {
            Basic,
            Project,
            Company
        }

        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();

        const string ProjectAccessErrorMes = "You do not permission to view this project.;This is not the the Project you are looking for.";

        /// <summary>
        /// Set If we can eddit the company In question
        /// </summary>
        int? ItemID { get; set; } = null;

        //SecurityType Type { get; set; } = SecurityType.Basic; 

        bool Valid = false;
        bool Viewable = false;

        SecurityType Type;

        public DBFSPAuthorizeAttribute()
        { Type = SecurityType.Basic; }

        public DBFSPAuthorizeAttribute(SecurityType TypeIn)
        {
            Type = TypeIn;
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            
            // if the session is null we MUST be in the wrong place
            if (httpContext.Session["SessionUserID"] == null || httpContext.Session["SessionCode"] == null)
                return false;

            int ID = (int)httpContext.Session["SessionUserID"]; //Simply validate code agenst session
            string Code = (string)httpContext.Session["SessionCode"];

            switch(Type)
            {
                case SecurityType.Project:
                    string[] Ans = httpContext.Request.Url.PathAndQuery.Split('/');
                    ItemID = int.Parse(Ans.Last());
                    ProjectPrivages Privsl = new ProjectPrivages();
                    using (ObjectResult<ValidateWithProjectViewPriv_Result> Result = DB.ValidateWithProjectViewPriv(ID, Code, ItemID))
                        Privsl.In(Result.First());

                    Valid = Privsl.Valid;
                    return Privsl.CanViewProject;
            }

            //if (CompanyID != null)
            //{
            //    ProjectPrivages Privage = new ProjectPrivages();
            //    using (ObjectResult<ValidateWithProjectViewPriv_Result> Result = DB.ValidateWithProjectViewPriv(ID, Code, ProjectID))
            //        Privage = new ProjectPrivages(Result.First());

            //    Valid = Privage.Valid;
            //    return Privage.CanViewProject;
            //}

            bool success = (bool)DB.ValidateSession(ID, Code).First();

            return success;
        }

        /// <summary>
        /// if we are not supposed to be there set us back to a log on with a redirect back waiting
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (Valid)
            {   filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "DashBoard" }, { "Action", "Index" }, { "Opps", ProjectAccessErrorMes } });  }

            Uri OldTarget = filterContext.HttpContext.Request.Url;
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Account" }, {"Action", "Login"}, {"ReturnURL", OldTarget} });
        }
    }
}