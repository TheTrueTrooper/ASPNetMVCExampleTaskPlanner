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
    
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.Tasks1 = new HashSet<Task>();
            this.Tasks11 = new HashSet<Task>();
        }
    
        public int TaskID { get; set; }
        public int SubContractorID { get; set; }
        public int TaskTypeID { get; set; }
        public int ProjectID { get; set; }
        public Nullable<int> PrevTask { get; set; }
        public Nullable<int> NextTask { get; set; }
        public string Description { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public System.DateTime CreationDate { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Project Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks1 { get; set; }
        public virtual Task Task1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks11 { get; set; }
        public virtual Task Task2 { get; set; }
        public virtual TaskType TaskType { get; set; }
    }
}