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
using ASP.NetMVCExample.SMTPHelpers;
using AngelASPExtentions.ASPMVCControllerExtentions;
using AngelASPExtentions.ExtraExtentions;

namespace ASP.NetMVCExample.Controllers
{
    /// <summary>
    /// this controller is resposible for login, account creation, and account password resset
    /// </summary>
    [DBFSPAuthorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// const for the salt length, picked 20 iff the top of my head. due to array contruction it turn into 24 spaces but thats ok
        /// </summary>
        const int CodeLengths = 20;
        /// <summary>
        /// the Global Shared Database 
        /// </summary>
        MVCTaskMasterAppDataEntities2 DB = SharedStarter.GetDataBase();

        SMTPClient SMTPClient = SharedStarter.GetSMTP();

        [NonAction]
        void SendEmail(string ToEmail, string Subject, string View, object Model = null)
        {
            string EmailTempOut = this.GetRazorTemplateAsString(View, Model);

            SMTPClient.SendMessage(ToEmail, Subject, EmailTempOut);
        }


        //-----------------------------------------------------------------------Pages------------------------------------------------------------------------------

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
                        Session["SessionCode"] = SecurityHelper.GetCode(CodeLengths);
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

                SecurityReturn PasscodeHasher = SecurityHelper.PasswordToSaltedHash(Registy.Password, CodeLengths);
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PasswordResetEmailRedirect(string Email, string Code)
        {
            UserPasswordReset Reset = new UserPasswordReset() { Email = Email, ResetCode = Code };
            return RedirectToAction("PasswordReset", Reset);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordReset(ASP.NetMVCExample.Models.Users.UserPasswordReset Reset)
        {
            const string PasswordResetSub = "Task Manager - Password Reset";

            //if we have a fully valid reset then we are done
            if (ModelState.IsValid)
            {
                //change the password
                SecurityReturn TempReturn = SecurityHelper.PasswordToSaltedHash(Reset.NewConfirmPassword, CodeLengths);
                DB.DoPasswordResset(Reset.Email, Reset.ResetCode, TempReturn.SaltedHashedPassword, TempReturn.Salt);
                return RedirectToAction("", "Home");
            }
            //if from the email you have gotten the reset code move to next step
            if(ModelState["ResetCode"].Errors.Count < 1 && ModelState["Email"].Errors.Count < 1)
            {
                return View("PasswordResetStep2", Reset);
            }
            //if we provided a email to work with move to step 2
            if (ModelState["Email"].Errors.Count < 1)
            {
                //make a code and attempt bind it
                string Code = SecurityHelper.GetCode(20);
                if (DB.CreateThePasswordResset(Reset.Email, Code).First().Value)
                {
                    ViewBag.URL = Url.Action("PasswordResetEmailRedirect", "Account", new { Email = Reset.Email, Code = Code });
                    // if bound send an email to the email
                    SendEmail(Reset.Email, PasswordResetSub, "PasswordResetEmailTemplate", Reset);
                }
                //Display to check the email or resend
                return View("PasswordResetCheckYourEmail", Reset);
            }
            return View("PasswordResetStep1", Reset);
        }

        /// <summary>
        /// this will work as a getter to validate form an email string
        /// </summary>
        /// <param name="User">The user to validate</param>
        /// <param name="Code">The Code for the user</param>
        /// <returns>A View to congraduate them</returns>
        [AllowAnonymous]
        public ActionResult ValidateAccount(string User, string Code)
        {
            if(!User.IsNullEmptyOrWhiteSpace() || !Code.IsNullEmptyOrWhiteSpace() )//|| DB.ValidateUser)
            {
                //come back to this....
                return View("FriendlyErr");
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}