//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace ASP.NetMVCExample.Models
{
    using System;
    using System.Collections.Generic;

    public partial class TaskLinker
    {
        public int linkerID { get; set; }
        public int TaskID { get; set; }
        public int NextTaskID { get; set; }

        public virtual Task Task { get; set; }
        public virtual Task Task1 { get; set; }
    }
}