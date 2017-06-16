using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASP.NetMVCExample;
using ASP.NetMVCExample.Controllers;
using System.Threading;
using System.Diagnostics;
using ASP.NetMVCExample.Tests.TestHelpers;

namespace ASP.NetMVCExample.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        /// <summary>
        /// going to leave this as an example of basics
        /// </summary>
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller__Topic_Default = new HomeController();
            HomeController controller__Topic_Cloud = new HomeController();
            HomeController controller__Topic_Tools = new HomeController();
            HomeController controller__Topic_Testimonial = new HomeController();
            HomeController controller__Topic_Jiberish = new HomeController();
            HomeController controller__Topic_Cloud__CaseInsensitive = new HomeController();

            // Act
            //Each posible value
            ViewResult Result__Topic_Default = controller__Topic_Default.About("") as ViewResult;
            ViewResult Result__Topic_Cloud = controller__Topic_Cloud.About("Cloud") as ViewResult;
            ViewResult Result__Topic_Tools = controller__Topic_Tools.About("Tools") as ViewResult;
            ViewResult Result__Topic_Testimonial = controller__Topic_Testimonial.About("Testimonial") as ViewResult;

            // jib should just revert to default
            ViewResult Result__Topic_Jiberish = controller__Topic_Jiberish.About("sdfsaf") as ViewResult;

            //cases should not matter
            ViewResult Result__Topic_Cloud__CaseInsensitive = controller__Topic_Cloud__CaseInsensitive.About("clOud") as ViewResult;

            // Assert
            //Test that all should load
            TestHelper.IsNotNull(Result__Topic_Default, "Check To see if About(Default) Makes a view properly", "Faild to generate a view ", nameof(Result__Topic_Default));
            TestHelper.IsNotNull(Result__Topic_Cloud, "Check To see if About(Cloud) Makes a view properly", "Faild to generate a view ", nameof(Result__Topic_Default));
            TestHelper.IsNotNull(Result__Topic_Tools, "Check To see if About(Tools) Makes a view properly", "Faild to generate a view ", nameof(Result__Topic_Default));
            TestHelper.IsNotNull(Result__Topic_Testimonial, "Check To see if About(Testimonial) Makes a view properly", "Faild to generate a view ", nameof(Result__Topic_Default));
            TestHelper.IsNotNull(Result__Topic_Jiberish, "Check To see if About(Jiberish) Makes a view properly", "Faild to generate a view ", nameof(Result__Topic_Default));
            TestHelper.IsNotNull(Result__Topic_Cloud__CaseInsensitive, "Check To see if About(Cloud_CaseInsensitive) Makes a view properly", "Faild to generate a view ", nameof(Result__Topic_Default));

            //all sould have different depths and Proper Values
            TestHelper.AreEqual("", Result__Topic_Default.ViewBag.ScrollId, "Target Scroll Value was wrong on Result__Topic_Default");
            TestHelper.AreEqual("Cloud", Result__Topic_Cloud.ViewBag.ScrollId, "Target Scroll Value was wrong on Result__Topic_Cloud");
            TestHelper.AreEqual("Tools", Result__Topic_Tools.ViewBag.ScrollId, "Target Scroll Value was wrong on Result__Topic_Tools");
            TestHelper.AreEqual("Testimonial", Result__Topic_Testimonial.ViewBag.ScrollId, "Target Scroll Value was wrong on Result__Topic_Testimonial");

            //Jiberish should generate the same result as Default
            TestHelper.AreEqual(Result__Topic_Default.ViewBag.ScrollId, Result__Topic_Jiberish.ViewBag.ScrollId, "Entering Jiberish should the same as Default and doesn't match. Success: Result__Topic_Jiberish == Result__Topic_Default");

            //casing should not matter
            TestHelper.AreEqual(Result__Topic_Cloud__CaseInsensitive.ViewBag.ScrollId, Result__Topic_Cloud.ViewBag.ScrollId, "Entry not be case sensitive. Success: Result__Topic_Cloud__CaseInsensitive == Result__Topic_Cloud");
        }


        /// <summary>
        /// going to leave this as an example of basics
        /// </summary>
        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Help()
        {
            // Arrange
            HomeController controller__Subject_Default = new HomeController();
            HomeController controller__Subject_Jiberish = new HomeController();

            // Act
            //Posible Values
            ViewResult Result__Subject_Default = controller__Subject_Default.Help("") as ViewResult;

            // jib should just revert to default
            ViewResult Result__Subject_Jiberish = controller__Subject_Jiberish.Help("sdfsaf") as ViewResult;

            // Assert
            TestHelper.IsNotNull(Result__Subject_Default, "Check To see if About(Default) Makes a view properly", "Faild to generate a view ", nameof(Result__Subject_Default));
            TestHelper.IsNotNull(Result__Subject_Jiberish, "Check To see if About(Cloud) Makes a view properly", "Faild to generate a view ", nameof(Result__Subject_Jiberish));

            //Jiberish should generate the same result as Default
            TestHelper.AreEqual(Result__Subject_Default.ViewBag.ScrollId, Result__Subject_Jiberish.ViewBag.ScrollId, "Entering Jiberish should the same as Default and doesn't match. Success: Result__Topic_Jiberish == Result__Topic_Default");

            //casing should not matter
            //TestHelper.AreEqual(Result__Topic_Cloud__CaseInsensitive.ViewBag.ScrollId, Result__Topic_Cloud.ViewBag.ScrollId, "Entry not be case sensitive. Success: Result__Topic_Cloud__CaseInsensitive == Result__Topic_Cloud");
        }
    }
}
