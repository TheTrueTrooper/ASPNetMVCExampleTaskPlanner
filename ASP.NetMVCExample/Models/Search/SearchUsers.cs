using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample.Models.Search
{
    public class SearchUsers
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name or Initial")]
        public string MiddleInitial { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}