#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 16,2017
//Project Goal: Encapsulate code for reuse and make more readable.
//File Goal: To add some custom helpers 
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: ASP.NetMVC-razor-Example-TaskPlanner 
//  Writer/Publisher:Angelo Sanches (BitSan)
//  Link: https://github.com/TheTrueTrooper/ASP.NetMVC-razor-Example-TaskPlanner
//
//  Name: ASP.net
//  Writer/Publisher: Microsoft
//  Link: https://www.asp.net/
//  }
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Optimization;
using System.Web.Routing;

namespace ASP.NetMVCExample.WebHelpers
{
    //I plan to export some of this to a class lib
    //Useful classes and tools that could be used out side
    #region Tools
    /// <summary>
    /// A tool that has a static email checker to use under the [RegexEmailChecker] Property
    /// </summary>
    public static class EmailCheckerTool
    {
        /// <summary>
        /// the email pattern. It is based on microsofts check
        /// </summary>
        const string _EmailPattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        /// <summary>
        /// the options for the check
        /// </summary>
        const RegexOptions _RegexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        /// <summary>
        /// the max time till a time out
        /// </summary>
        static readonly TimeSpan _MatchTimeout = TimeSpan.FromSeconds(2);

        /// <summary>
        /// read only pattern checker
        /// </summary>
        static readonly Regex _RegexEmailChecker = new Regex(_EmailPattern, _RegexOptions, _MatchTimeout);

        internal static Regex RegexEmailChecker { get { return _RegexEmailChecker; } }

        /// <summary>
        /// Depreceated in MVC use data ano in place
        /// checks if a string is a valid email using the email password checker tool
        /// </summary>
        /// <param name="ToCheck">the string to check</param>
        /// <returns>if it is an email</returns>
        static bool IsValidEmail(string ToCheck)
        {
            return ToCheck != null && EmailCheckerTool.RegexEmailChecker.IsMatch(ToCheck);
        }
    }

    /// <summary>
    /// A tool that has a static email checker to use under the [RegexEmailChecker] Property
    /// </summary>
    public static class PasswordCheckerTool
    {
        static string _PasswordPattern = "";
        static RegexOptions _RegexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        /// <summary>
        /// the max time to allow the check
        /// </summary>
        static readonly TimeSpan _MatchTimeout = TimeSpan.FromSeconds(2);
        static Regex _RegexPaswordChecker = new Regex(_PasswordPattern, _RegexOptions, _MatchTimeout);

        /// <summary>
        /// gets the tool
        /// </summary>
        internal static Regex RegexPaswordChecker
        { get { return _RegexPaswordChecker; } }

        /// <summary>
        /// sets the pattern to use
        /// </summary>
        public static string PasswordPattern
        {
            set
            {
                if (_PasswordPattern != null)
                {
                    _PasswordPattern = value;
                    _RegexPaswordChecker = new Regex(_PasswordPattern, _RegexOptions, _MatchTimeout);
                }
            }
        }
        /// <summary>
        /// sets the options to use
        /// </summary>
        public static RegexOptions RegexOptions
        {
            set
            {
                _RegexOptions = value;
                _RegexPaswordChecker = new Regex(_PasswordPattern, _RegexOptions, _MatchTimeout);
            }
        }

        /// <summary>
        /// checks if a password is good based on password checker tool
        /// to use set the Static PasswordCheckerTool class
        /// </summary>
        /// <param name="ToCheck">the string password to check</param>
        /// <returns>if the password is acceptable</returns>
        static bool IsValidPassword(string ToCheck)
        {
            return ToCheck != null && RegexPaswordChecker.IsMatch(ToCheck);
        }
    }
    #endregion

    //useful helperclasses that leverage the tools
    #region Helpers
    public class CustomWebHelpers
    {

        /// <summary>
        /// Depreceated in MVC use data ano in place
        /// checks if a string is a valid email using the email password checker tool
        /// </summary>
        /// <param name="ToCheck">the string to check</param>
        /// <returns>if it is an email</returns>
        static bool IsValidEmail(string ToCheck)
        {
            return ToCheck != null && EmailCheckerTool.RegexEmailChecker.IsMatch(ToCheck);
        }

        /// <summary>
        /// checks if a password is good based on password checker tool
        /// to use set the Static PasswordCheckerTool class
        /// </summary>
        /// <param name="ToCheck">the string password to check</param>
        /// <returns>if the password is acceptable</returns>
        static bool IsValidPassword(string ToCheck)
        {
            return ToCheck != null && PasswordCheckerTool.RegexPaswordChecker.IsMatch(ToCheck);
        }



    }
    #endregion

}