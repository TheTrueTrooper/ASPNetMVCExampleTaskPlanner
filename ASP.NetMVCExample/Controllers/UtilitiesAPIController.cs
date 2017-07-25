using ASP.NetMVCExample._Helpers;
using ASP.NetMVCExample.Models;
using ASP.NetMVCExample.Models.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace ASP.NetMVCExample.Controllers
{
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

    }
}
