using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NetMVCExample.Controllers
{
    /// <summary>
    /// this controller is for testing purpo on routing
    /// </summary>
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public ActionResult Index()
        {
            ViewBag.Test = "dsadfa";

            return View();
        }

        /// <summary>
        /// just playing around
        /// really souldnt make a view here but this was a request test
        /// </summary>
        /// <param name="name">a persons name</param>
        /// <returns>html that is just a string to display</returns>
        public string WhereIsChuck(string name = "mister")
        {
            return HttpUtility.HtmlEncode("He is right out outside YOUR window " + name);
        }

    }
}