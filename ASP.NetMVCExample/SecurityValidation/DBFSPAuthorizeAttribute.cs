using ASP.NetMVCExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASP.NetMVCExample.SecurityValidation
{
    /// <summary>
    /// this is a Data Base First Stored Procedure work around to the traditional Athorization
    /// basicly using the Session like tradtional Systems it uses session to log on agenst the database
    /// </summary>
    public class DBFSPAuthorizeAttribute : AuthorizeAttribute
    {
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();
        
        /// <summary>
        /// Set If we can eddit the company In question
        /// </summary>
        public bool CompanyEdit { get; set; } = false;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // if the session is null we MUST be in the wrong place
            if (httpContext.Session["SessionUserID"] == null || httpContext.Session["SessionCode"] == null)
                return false;
            // basic check aganst session data base ID and Code.
            int ID = (int)httpContext.Session["SessionUserID"]; //Simply validate code agenst session
            string Code = (string)httpContext.Session["SessionCode"];
            bool success = (bool)DB.ValidateSession(ID, Code).First();

            //To Do Add a redirect for email verifacation if not validated here

            //if property not on (defualt) or we faild at the last test skip over this one as no need
            if(CompanyEdit && success)
            {
                //success = httpContext.Request.QueryString["CompanyID"];
            }

            return success;
        }

        /// <summary>
        /// if we are not supposed to be there set us back to a log on with a redirect back waiting
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            Uri OldTarget = filterContext.HttpContext.Request.Url;
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Account" }, {"Action", "Login"}, {"ReturnURL", OldTarget} });
        }
    }
}