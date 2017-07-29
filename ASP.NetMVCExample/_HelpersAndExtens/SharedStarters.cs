#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To add an encapsolated 255 salt and hash encoder
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: NA
//  Writer/Publisher: NA
//  Link: NA
//  }
#endregion
using ASP.NetMVCExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP.NetMVCExample.SMTPHelpers;

namespace ASP.NetMVCExample._Helpers
{
    /// <summary>
    /// this class simply makes a database for use to share
    /// </summary>
    public class SharedStarter
    {
        /// <summary>
        /// the client to use
        /// </summary>
        static SMTPClient Client = new SMTPClient(HttpContext.Current.Server.MapPath("~/PrivateServerOptions.Config"));

        /// <summary>
        /// used to get a shared SMTPClient that is shared between classes and types
        /// </summary>
        /// <returns>the SMTPClient</returns>
        internal static SMTPClient GetSMTP()
        {
            return Client;
        }
    }
}