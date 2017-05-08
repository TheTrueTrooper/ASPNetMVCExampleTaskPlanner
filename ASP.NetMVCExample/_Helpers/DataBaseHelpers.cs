using ASP.NetMVCExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample._Helpers
{
    /// <summary>
    /// this class simply makes a database for use to share
    /// </summary>
    public class DataBaseHelpers
    {
        /// <summary>
        /// the shared database
        /// </summary>
        static MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();

        /// <summary>
        /// Used to get the database
        /// </summary>
        /// <returns>the database</returns>
        internal static MVCTaskMasterAppDataEntities2 GetDataBase()
        {
            return DB;
        }
    }
}