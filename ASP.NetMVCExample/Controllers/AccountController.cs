using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NetMVCExample.WebHelpers;
using ASP.NetMVCExample.SecurityHelpers;
using System.Data.Entity.Core.Objects;
using ASP.NetMVCExample.Models.Users;
using ASP.NetMVCExample.Models;
using ASP.NetMVCExample.SecurityValidation;
using ASP.NetMVCExample._Helpers;

namespace ASP.NetMVCExample.Controllers
{
    /// <summary>
    /// this controller is resposible for login, account creation, and account password resset
    /// </summary>
    [DBFSPAuthorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// the Global Shared Database 
        /// </summary>
        MVCTaskMasterAppDataEntities2 DB = DataBaseHelpers.GetDataBase();

        /// <summary>
        /// creates a page for us to login at
        /// </summary>
        /// <param name="ReturnURL">the get url to redirect on successful login</param>
        /// <returns>a view to make or a redirect view</returns>
        [AllowAnonymous]
        // GET: Account
        public ActionResult Login(string ReturnURL = null)
        {
            ViewBag.ReturnUrl = ReturnURL;
            @ViewBag.Error = "";
            return View();
        }

        /// <summary>
        /// Trys to log in and create a session for us with info in the model
        /// </summary>
        /// <param name="LoginAttempt">our post model info</param>
        /// <param name="ReturnURL">the get url to redirect on successful login</param>
        /// <returns>a view to make or a redirect view</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        // GET: Account
        public ActionResult Login(UserLoginTry LoginAttempt, string ReturnURL = null)
        {
            ViewBag.ReturnUrl = ReturnURL;
            //at any point we fail we will fall though to the bottom as par my style and return the original page with data filled out
            if (ModelState.IsValid)
            {
                //out params on stored procedures
                var ChecksOutParameter = new ObjectParameter("ChecksOut", typeof(bool));
                var IDOutParameter = new ObjectParameter("IDOut", typeof(int));

                // try and get the salt from the database
                using (ObjectResult<string> Result = DB.GetTheSalt(LoginAttempt.Email))
                {
                    string[] Results = Result.ToArray();
                    if (Results.Count() > 0)
                        LoginAttempt.Salt = Results.First();
                }

                //salt hash and check the password agenst the database
                LoginAttempt.Password = SecurityHelper.PasswordToSaltedHash(LoginAttempt.Password, LoginAttempt.Salt);
                DB.PasswordCheck(LoginAttempt.Email, LoginAttempt.Password, IDOutParameter, ChecksOutParameter);
                
                //cast the out to a bool for use
                bool Success = (bool)ChecksOutParameter.Value;
              
                if(Success)
                {
                    //make a session on both the server and the client for validation later
                        int UserID = (int)IDOutParameter.Value;
                        SecurityHelper.GetCode();
                        Session["SessionUserID"] = UserID;
                        Session["Email"] = LoginAttempt.Email;
                        Session["SessionCode"] = SecurityHelper.GetCode(20);
                        DB.CreateTheSession((int)Session["SessionUserID"], (string)Session["SessionCode"]);
                    if (ReturnURL == null) //if we dont have a redirect go to dashboard
                        return RedirectToAction("Index", "DashBoard");
                    else //else go to the redirect
                        return Redirect(ReturnURL);
                }

                // if we failed to get to the inner most loop the password or account was wrong
                //note let them know vagly so the cant use the information to attack with
                @ViewBag.Error = "Sorry, but either your email account or password are incorrect.";
            }
            else // if early you havent entered a password so give some feed back on the matter
                @ViewBag.Error = "Sorry, but you must enter email and password to sign in.";

            //return to the view with he errors and inforamtion on failier
            return View(LoginAttempt);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegEntry Registy)
        {
            if (ModelState.IsValid && !DB.IsEmailUsed(Registy.Email).First().Value)
            {
                string ErrorMessage = "";
                var ErrorMessageParameter = ErrorMessage != null ?
                new ObjectParameter("ErrorMessage", ErrorMessage) :
                new ObjectParameter("ErrorMessage", typeof(string));

                SecurityReturn PasscodeHasher = SecurityHelper.PasswordToSaltedHash(Registy.Password, 20);
                Registy.Salt = PasscodeHasher.Salt;
                Registy.Password = PasscodeHasher.SaltedHashedPassword;

                int Error = DB.InsertNewUser(Registy.FirstName, Registy.MiddleInitial, Registy.LastName, Registy.Email,
                    Registy.Password, Registy.Salt, Registy.HomePhone, Registy.CellPhone, Registy.WorkPhone, ErrorMessageParameter);
                ViewBag.ErrorMessage = ErrorMessageParameter.Value as string;

                if (Error > 0)
                    return RedirectToAction("Login");

                
            }
            return View(Registy);

        }

        [AllowAnonymous]
        public ActionResult PasswordReset()
        {
            return View("PasswordResetStep1");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordResset(UserPasswordRessetStep1 RessetStep1)
        {
            if (ModelState.IsValid)
            {
                UserPasswordRessetStep2 RessetStep2 = new UserPasswordRessetStep2();
                RessetStep2.Email = RessetStep1.Email;
                return View("PasswordResetStep2", RessetStep2.Email);
            }
            return View("PasswordResetStep1", RessetStep1);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordResset(UserPasswordRessetStep2 Resset)
        {
            if (ModelState.IsValid)
            {
            }
            return View("PasswordResetStep2", Resset);
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}