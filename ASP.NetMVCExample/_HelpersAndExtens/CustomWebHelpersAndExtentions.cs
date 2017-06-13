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

   

    //useful Extentions that add functionality to classes 
    #region Extentions

    /// <summary>
    /// some Extentions for the class to write less later For the view
    /// </summary>
    public static class CustomWebExtentions
    {

        //Extentions for use with strings 
        #region Strings
        public static bool IsValidEmail(this string ToCheck)
        {
            return ToCheck != null && EmailCheckerTool.RegexEmailChecker.IsMatch(ToCheck);
        }

        public static bool IsValidPassword(this string ToCheck)
        {
            return ToCheck != null && PasswordCheckerTool.RegexPaswordChecker.IsMatch(ToCheck);
        }

        public static bool IsNullEmptyOrWhiteSpace(this string ToCheck)
        {
            return String.IsNullOrWhiteSpace(ToCheck);
        }

        #endregion

        #region HTMLInjectionStyleAndScript
       
        /// <summary>
        /// This Renders To the HTML after the controller and view render Adds styles and script in the head of the DOM. In Debug it marks end and start.
        /// Make Sure is in the head
        /// </summary>
        /// <param name="This"></param>
        /// <returns>the HTML String</returns>
        public static HtmlString RenderBundles(this HtmlHelper This)
        {
            string Return = "";
            //for debug render out the section lable
#if DEBUG
            Return += "<!--Per page added scripts-->\n";
#endif
            //then add The Script Bundles
            if (This.ViewBag.ScriptBundles != null && This.ViewBag.ScriptBundles is string[])
            {
                foreach (string addScript in ((string[])This.ViewBag.ScriptBundles))
                { Return += Scripts.Render(addScript).ToHtmlString(); }
            }
            // repeat for the Styles
#if DEBUG
            Return += "<!--Per page added styles-->\n";
#endif
            if (This.ViewBag.StyleBundles != null && This.ViewBag.StyleBundles is string[])
            {
                foreach (string addStyle in ((string[])This.ViewBag.StyleBundles))
                { Return += Styles.Render(addStyle).ToHtmlString(); }
            }
#if DEBUG
            Return += "<!--EndOfBund-->\n";
#endif
            return new HtmlString(Return);
        }

        /// <summary>
        ///     For Use on view side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For style bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.StyleBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Conroller for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The HtmlHelper to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find StyleBundles</param>
        /// <returns>HtmlHelper for chaining</returns>
        public static HtmlHelper AddToRenderStyleBundles(this HtmlHelper This, string[] StyleBundles)
        {
            if (StyleBundles == null || StyleBundles.Count() < 1)
                return This;
            if (This.ViewBag.StyleBundles != null)
            {
                if (This.ViewBag.StyleBundles is string[])
                    This.ViewBag.StyleBundles = ((string[])This.ViewBag.StyleBundles).Union(StyleBundles);
                else
                    throw new Exception("ViewBag.StyleBundles is NOT Null. Space should be null to allow usage or posible conflict could happen");
            }
            else
                This.ViewBag.StyleBundles = StyleBundles;
            return This;
        }

        /// <summary>
        ///     For Use on view side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For style bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.StyleBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Conroller for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The HtmlHelper to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find StyleBundles</param>
        /// <returns>HtmlHelper for chaining</returns>
        public static HtmlHelper AddToRenderStyleBundles(this HtmlHelper This, List<string> StyleBundles)
        {
            if (StyleBundles == null || StyleBundles.Count() < 1)
                return This;
            return AddToRenderStyleBundles(This, StyleBundles.ToArray());
        }

        /// <summary>
        ///     For Use on view side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For style bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.StyleBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Conroller for easy Chaining that is line seperatatable for easy read
        /// Note
        ///     Does nothing if input is null useful for place holding or templating 
        ///     This also means it ill not throw unless it has input
        /// </summary>
        /// <param name="This">The HtmlHelper to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find StyleBundles</param>
        /// <returns>HtmlHelper for chaining</returns>
        public static HtmlHelper AddToRenderStyleBundles(this HtmlHelper This, string StyleBundles)
        {
            if (StyleBundles == null)
                return This;
            return AddToRenderStyleBundles(This, new string[] { StyleBundles });
        }

        /// <summary>
        ///     For Use on view side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For script bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.ScriptBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Conroller for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The HtmlHelper to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find ScriptBundles</param>
        /// <returns>HtmlHelper for chaining</returns>
        public static HtmlHelper AddToRenderScriptBundles(this HtmlHelper This, string[] ScriptBundles)
        {
            if (ScriptBundles == null || ScriptBundles.Count() < 1)
                return This;
            if (This.ViewBag.ScriptBundles != null)
            {
                if (This.ViewBag.ScriptBundles is string[])
                    This.ViewBag.ScriptBundles = ((string[])This.ViewBag.ScriptBundles).Union(ScriptBundles);
                else
                    throw new Exception("ViewBag.StyleBundles is NOT Null. Space should be null to allow usage or posible conflict could happen");
            }
            else
                This.ViewBag.ScriptBundles = ScriptBundles;
            return This;
        }

        /// <summary>
        ///     For Use on view side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For script bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.ScriptBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Conroller for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The HtmlHelper to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find ScriptBundles</param>
        /// <returns>HtmlHelper for chaining</returns>
        public static HtmlHelper AddToRenderScriptBundles(this HtmlHelper This, List<string> ScriptBundles)
        {
            if (ScriptBundles == null || ScriptBundles.Count() < 1)
                return This;
            return AddToRenderScriptBundles(This, ScriptBundles.ToArray());
        }

        /// <summary>
        ///     For Use on views side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For script bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.ScriptBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the HtmlHelper for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The HtmlHelper to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find ScriptBundles</param>
        /// <returns>HtmlHelper for chaining</returns>
        public static HtmlHelper AddToRenderScriptBundles(this HtmlHelper This, string ScriptBundles = null)
        {
            if (ScriptBundles == null)
                return This;
            return AddToRenderScriptBundles(This, new string[] { ScriptBundles });
        }
        #endregion
    }

    /// <summary>
    /// some Extentions for the class to write less later For the controller
    /// </summary>
    public static class CustomContollerExtentions
    {
        #region ControllerInjectionStyleAndScript
        /// <summary>
        ///     You should consider HtmlHelper.AddToRenderStyleBundles extention first as this is bad practice but might be useful
        ///     For Use on controllers side to create a list for Rendering in view using HtmlHelper.RenderBundlesSylesAndScripts extention
        ///     this one in particular is used For style bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.StyleBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Controller for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The controller to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find StyleBundles</param>
        /// <returns>Controller for chaining</returns>
        public static Controller AddToRenderStyleBundles(this Controller This, string[] StyleBundles)
        {
            if (StyleBundles == null || StyleBundles.Count() < 1)
                return This;
            if (This.ViewBag.StyleBundles != null)
            {
                if (This.ViewBag.StyleBundles is string[])
                    This.ViewBag.StyleBundles = ((string[])This.ViewBag.StyleBundles).Union(StyleBundles);
                else
                    throw new Exception("ViewBag.StyleBundles is NOT Null. Space should be null to allow usage or posible conflict could happen");
            }
            else
                This.ViewBag.StyleBundles = StyleBundles;
            return This;
        }

        /// <summary>
        ///     You should consider HtmlHelper.AddToRenderStyleBundles extention first as this is bad practice but might be useful
        ///     For Use on controllers side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For style bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.StyleBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Controller for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The controller to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find StyleBundles</param>
        /// <returns>Controller for chaining</returns>
        public static Controller CAddToRenderStyleBundles(this Controller This, List<string> StyleBundles)
        {
            if (StyleBundles == null || StyleBundles.Count() < 1)
                return This;
            return AddToRenderStyleBundles(This, StyleBundles.ToArray());
        }

        /// <summary>
        ///     You should consider HtmlHelper.AddToRenderStyleBundles extention first as this is bad practice but might be useful
        ///     For Use on controllers side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For style bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.StyleBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Controller for easy Chaining that is line seperatatable for easy read
        /// Note
        ///     Does nothing if input is null useful for place holding or templating 
        ///     This also means it ill not throw unless it has input
        /// </summary>
        /// <param name="This">The controller to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find StyleBundles</param>
        /// <returns>Controller for chaining</returns>
        public static Controller AddToRenderStyleBundles(this Controller This, string StyleBundles)
        {
            if (StyleBundles == null)
                return This;
            return AddToRenderStyleBundles(This, new string[] { StyleBundles });
        }

        /// <summary>
        ///     You should consider HtmlHelper.AddToRenderScriptBundles extention first as this is bad practice but might be useful
        ///     For Use on controllers side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For script bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.ScriptBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Controller for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The controller to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find ScriptBundles</param>
        /// <returns>Controller for chaining</returns>
        public static Controller AddToRenderScriptBundles(this Controller This, string[] ScriptBundles)
        {
            if (ScriptBundles == null || ScriptBundles.Count() < 1)
                return This;
            if (This.ViewBag.ScriptBundles != null)
            {
                if (This.ViewBag.ScriptBundles is string[])
                    This.ViewBag.ScriptBundles = ((string[])This.ViewBag.ScriptBundles).Union(ScriptBundles);
                else
                    throw new Exception("ViewBag.StyleBundles is NOT Null. Space should be null to allow usage or posible conflict could happen");
            }
            else
                This.ViewBag.ScriptBundles = ScriptBundles;
            return This;
        }

        /// <summary>
        ///     You should consider HtmlHelper.AddToRenderScriptBundles extention first as this is bad practice but might be useful
        ///     For Use on controllers side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For script bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.ScriptBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Controller for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The controller to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find ScriptBundles</param>
        /// <returns>Controller for chaining</returns>
        public static Controller AddToRenderScriptBundles(this Controller This, List<string> ScriptBundles)
        {
            if (ScriptBundles == null || ScriptBundles.Count() < 1)
                return This;
            return AddToRenderScriptBundles(This, ScriptBundles.ToArray());
        }

        /// <summary>
        ///     You should consider HtmlHelper.AddToRenderScriptBundles extention first as this is bad practice but might be useful
        ///     For Use on controllers side to create a list for Rendering in view using Html.VRenderBundlesSylesAndScripts extention
        ///     this one in particular is used For script bundles
        ///     Is useful for perpage bundles so it can be rendered in the head while not Requiring it for all
        /// throws exeption on 
        ///   ViewBag.ScriptBundles not being NULL and is not a Array as presset by earlier call, because it adds your list to ViewBag.StyleBundles
        /// Returns 
        ///     the Controller for easy Chaining that is line seperatatable for easy read
        /// </summary>
        /// <param name="This">The controller to chain off(us this. in method)</param>
        /// <param name="StyleBundles">A list of virtual paths on which to find ScriptBundles</param>
        /// <returns>Controller for chaining</returns>
        public static Controller AddToRenderScriptBundles(this Controller This, string ScriptBundles = null)
        {
            if (ScriptBundles == null)
                return This;
            return AddToRenderScriptBundles(This, new string[] { ScriptBundles });
        }
        #endregion
        
        #region RazorTemplating
        /// <summary>
        /// Gets a String rep. of a view Back after Templateing. This is usefull if you would like to Template an email or something?
        /// </summary>
        /// <param name="This">The Contorller extending off of</param>
        /// <param name="ViewName">The name of the view to use</param>
        /// <returns>The HTML Templated rep. as a string</returns>
        public static string GetRazorTemplateAsString(this Controller This, string ViewName)
        {
            OpenStringResult Result = new OpenStringResult(ViewName);

            ControllerContext Context = new ControllerContext(This.HttpContext, This.RouteData, This);

            Result.ExecuteResult(Context);

            return Result.Result;
        }
        #endregion
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

    #region Other
    /// <summary>
    /// A class to allow us to Template with the existing MVC eng in a Extention class
    /// </summary>
    internal class OpenStringResult : ViewResult
    {
        public string Result { private set; get; }

        /// <summary>
        /// Creates a OpenStringResult From a ViewName
        /// </summary>
        /// <param name="ViewNameIn"></param>
        public OpenStringResult(string ViewNameIn)
        {
            ViewName = ViewNameIn;
        }

        /// <summary>
        /// forces a render and stores the existing HTML text as a string
        /// </summary>
        /// <param name="Context">a Controller Context</param>
        public override void ExecuteResult(ControllerContext Context)
        {
            if (Context == null)
                throw new ArgumentNullException("Context");

            if (String.IsNullOrEmpty(ViewName))
                throw new Exception("ViewName Can't be null");
           
            View = FindView(Context).View;

            StringBuilder Builder = new StringBuilder();
            StringWriter Writer = new StringWriter(Builder);

            ViewContext ViewContext = new ViewContext(Context, View, ViewData, TempData, Writer);

            View.Render(ViewContext, Writer);

            Result = Builder.ToString();
        }
    }
    #endregion
}