using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NetMVCExample.WebHelpers;

namespace ASP.NetMVCExample.Controllers
{
    /// <summary>
    /// this is our basic uncontrolled class and pages for a landing
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(string Topic = "")
        {
            ViewBag.ScrollId = Topic;

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Help(string Subject)
        {
            ViewBag.HelpSubject = Subject;
            return View();
        }
    }
}