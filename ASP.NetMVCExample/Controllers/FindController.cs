using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NetMVCExample.Models.Search;
using ASP.NetMVCExample.Models;
using ASP.NetMVCExample._Helpers;

/// <summary>
/// not implemented yet
/// </summary>
namespace ASP.NetMVCExample.Controllers
{
    public class FindController : Controller
    {
        static MVCTaskMasterAppDataEntities2 DB = DataBaseHelpers.GetDataBase();

        // GET: Find
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Individual(SearchUsers Params)
        {
            return View();
        }
    }
}