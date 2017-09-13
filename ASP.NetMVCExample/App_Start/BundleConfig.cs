#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To allow the extend the current bundles
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: ASP.net
//  Writer/Publisher: Microsoft
//  Link: https://www.asp.net/
//  }
#endregion
using System.Web;
using System.Web.Optimization;

namespace ASP.NetMVCExample
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

//#if DEBUG
////BundleTable.EnableOptimizations = false;
////            bundles.Add(new ScriptBundle("~/bundles/mainJs")
////      .Include("~/Scripts/mainSite.js")
////      .Include("~/Scripts/helperStuff.js"));
////after a push up date all
//#endif


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // my scriptBundles
            // libs
            bundles.Add(new ScriptBundle("~/bundles/Lib")
                .Include("~/Scripts/Moment.js")
                .Include("~/Scripts/angular.js"));
            //per page 
            bundles.Add(new ScriptBundle("~/bundles/About").Include("~/Scripts/PageScripts/About/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/DashBoard").Include("~/Scripts/PageScripts/DashBoard/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/Projects").Include("~/Scripts/PageScripts/Projects/*.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            // my styleBundles
            //libs

            //per page
            bundles.Add(new StyleBundle("~/Content/All").Include("~/Content/PageCss/*.css",
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
        }
    }
}
