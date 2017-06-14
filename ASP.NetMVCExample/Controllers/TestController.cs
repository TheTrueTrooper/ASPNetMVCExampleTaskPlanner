using ASP.NetMVCExample.Models;
using ASP.NetMVCExample.WebHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NetMVCExample.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            ViewBag.Test = new TestModel();
            ViewData.Model = new TestModel() { Success= "false" };
            ViewBag.TestResult = this.GetRazorTemplateAsString("Test", new TestModel());
            return View();
        }
    }
}