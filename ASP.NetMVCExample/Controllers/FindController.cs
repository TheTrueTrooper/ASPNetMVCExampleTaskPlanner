﻿#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To help control the calls relating public profile searches
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources/References: 
//  {
//  Name: ASP.net
//  Writer/Publisher: Microsoft
//  Link: https://www.asp.net/
//  }
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NetMVCExample.Models.SearchView;
using ASP.NetMVCExample.Models;
using ASP.NetMVCExample._Helpers;

/// <summary>
/// not implemented yet
/// </summary>
namespace ASP.NetMVCExample.Controllers
{
    public class FindController : Controller
    {
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();

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