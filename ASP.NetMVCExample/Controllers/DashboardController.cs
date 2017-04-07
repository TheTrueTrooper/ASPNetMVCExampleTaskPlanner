using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NetMVCExample.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            //Per Page styles (css) add here
            @ViewBag.Styles = new List<string>()
            {
                "\"/Content/ProjectBoxCSS.css\""
            };
            //Per Page styles (java) add here
            @ViewBag.Scripts = new List<string>()
            {
            };

            return View();
        }


    }
}