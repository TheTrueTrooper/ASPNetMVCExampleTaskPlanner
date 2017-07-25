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