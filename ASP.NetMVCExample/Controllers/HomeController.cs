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
            Topic = Topic.ToLower();
            switch (Topic)
            {
                case "cloud":
                    ViewBag.ScrollId = "Cloud";
                    break;
                case "tools":
                    ViewBag.ScrollId = "Tools";
                    break;
                case "testimonial":
                    ViewBag.ScrollId = "Testimonial";
                    break;
                default:
                    ViewBag.ScrollId = "";
                    break;
            }

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Help(string Subject = "")
        {
            Subject = Subject.ToLower();
            switch (Subject)
            {
                default:
                    ViewBag.ScrollId = "";
                    break;
            }

            ViewBag.HelpSubject = Subject;
            return View();
        }
    }
}