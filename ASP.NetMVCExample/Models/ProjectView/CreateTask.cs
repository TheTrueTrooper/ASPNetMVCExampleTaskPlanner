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
        /*
        @SubContractorID int,
	    @TaskTypeID int,
	    @ProjectID int,
	    @Description NVARCHAR(250),
	    @DurationTicks bigint,
        @OutID int output,
        @ErrorMessage char (100) output
        */
        public Nullable<int> SubContractorID { get; set; }
        [Required]
        public List<int> NextTask { get; set; }
        [Required]
        public List<int> PrevTask { get; set; }
        [Required]
        public int ProjectID { get; set; }
        public int TaskID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TaskTypeID { get; set; }
        [Required]
        public string ExpectedDuration { get; set; }
        public long ExpectedDurationAsTicks
        {
            get
            {
                //If we have a start date start using it to calculate agenst the end date. If not well we have not even started so the actual durration must be 0.
                return TimeSpan.ParseExact(ExpectedDuration, "ddd\\:hh\\:mm\\:ss", CultureInfo.InvariantCulture).Ticks;
            }
        }


        
    }
}