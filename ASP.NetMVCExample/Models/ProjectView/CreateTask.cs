using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample.Models.ProjectView
{
    public class CreateTask
    {
        [Required]
        public int ProjectID { get; set; }
        public int TaskID { get; set; }
        public Nullable<int> SubContractorID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TaskTypeID { get; set; }
        public System.TimeSpan ExpectedDuration { get; set; }
        public Nullable<System.DateTime> ActualStartDate { get; set; }
        public Nullable<System.DateTime> ActualEndDate { get; set; }
        [Required]
        public string ActualDuration { get; set; }
        public long ActualDurationAsTicks
        {
            get
            {
                //If we have a start date start using it to calculate agenst the end date. If not well we have not even started so the actual durration must be 0.
                return TimeSpan.ParseExact(ActualDuration, "ddd\\:hh\\:mm\\:ss", CultureInfo.InvariantCulture).Ticks;
            }
        }
        [Required]
        public List<int> NextTask { get; set; }
        [Required]
        public List<int> PrevTask { get; set; }
    }
}