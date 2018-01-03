using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

// this will probably need optmization later;
namespace ASP.NetMVCExample.Models.ProjectView
{
    /// <summary>
    /// this will probably need optmization later;
    /// A container for tasks that will add linkers to it;
    /// </summary>
    public class ProjectTasks
    {

        public int ChainNumber { get; set; }
        public int NumberInChain { get; set; }
        [Required]
        public int TaskID { get; set; }
        public Nullable<int> SubContractorID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TaskTypeID { get; set; }
        //public System.DateTime ExpectedStartDate
        //{
        //    get
        //    {

        //    }
        //}
        //public System.DateTime ExpectedEndDate
        //{
        //    get
        //    {

        //    }
        //}
        public System.TimeSpan ExpectedDuration { get; set; }
        public Nullable<System.DateTime> ActualStartDate { get; set; }
        public Nullable<System.DateTime> ActualEndDate { get; set; }
        public System.TimeSpan ActualDuration
        {
            get
            {
                //If we have a start date start using it to calculate agenst the end date. If not well we have not even started so the actual durration must be 0.
                return new TimeSpan(ActualStartDate == null || ActualStartDate.HasValue 
                    //If the end date has been used apply it agnst start. if not assume now. 
                    ? (ActualEndDate == null || ActualEndDate.HasValue 
                        ? ActualEndDate.Value.Ticks - ActualStartDate.Value.Ticks 
                        : DateTime.UtcNow.Ticks - ActualStartDate.Value.Ticks) 
                    : 0);
            }
        }
        [Required]
        public System.DateTime CreationDate { get; set; }
        [Required]
        public List<int> NextTask { get; set; }
        [Required]
        public List<int> PrevTask { get; set; }

        /// <summary>
        /// basic constructor
        /// </summary>
        public ProjectTasks()
        {

        }

        /// <summary>
        /// we build a data set from the object
        /// </summary>
        /// <param name="Result"></param>
        public ProjectTasks(SelectTasksByProjectID_Result Result)
        {
            TaskID = Result.TaskID;
            SubContractorID = Result.SubContractorID;
            Description = Result.Description;
            TaskTypeID = Result.TaskTypeID;
            ExpectedDuration = new System.TimeSpan(Result.DurationTicks);
            CreationDate = Result.CreationDate;
            NextTask = new List<int>();
            PrevTask = new List<int>();
        }

        /// <summary>
        /// Adds a next task to the list of next tasks will be used when prosessing linkers
        /// </summary>
        /// <param name="ID"></param>
        internal void AddNextTask(int ID)
        {
            NextTask.Add(ID);
        }

        /// <summary>
        /// Adds a next task to the list of next tasks will be used when prosessing linkers
        /// </summary>
        /// <param name="ID"></param>
        internal void AddPreTask(int ID)
        {
            PrevTask.Add(ID);
        }
    }


    /// <summary>
    /// Used to get the view for the project
    /// this will probably be slow <---------------------------------------------- may want to fix later
    /// </summary>
    public class Project_Tasks_And_Links
    {
        //list of tasks
        public List<ProjectTasks> Tasks { get; set; }

        /// <summary>
        /// will build the list of tasks from the data base
        /// note: should wrap in try catch
        /// </summary>
        /// <param name="DB">The Database</param>
        /// <param name="ProjectID">The ProjectID to select from</param>
        public Project_Tasks_And_Links(MVCTaskMasterAppDataEntities2 DB, int ProjectID)
        {
            //grab the projects and start adding them to the list
            using (ObjectResult<SelectTasksByProjectID_Result> OJ = DB.SelectTasksByProjectID(ProjectID))
            {
                List<SelectTasksByProjectID_Result> TasksIn = OJ.ToList();
                Tasks = new List<ProjectTasks>(TasksIn.Count);
                foreach (SelectTasksByProjectID_Result R in TasksIn)
                {
                    Tasks.Add(new ProjectTasks(R));
                }
            }

            //after we have all the projects start to grab the linkers
            foreach (ProjectTasks T in Tasks)
            {
                using (ObjectResult<SelectLinkerByTaskID_Result> OJ = DB.SelectLinkerByTaskID(T.TaskID))
                {
                    List<SelectLinkerByTaskID_Result> Linkers = OJ.ToList();
                    // foreach linker grab that task and set its next task 
                    foreach (SelectLinkerByTaskID_Result R in OJ)
                    {
                        T.AddNextTask(R.NextTaskID);
                    }
                    //get all the next and set them accordingly
                    //.. foreach next task set the data to the prev one
                    foreach (ProjectTasks TNext in Tasks.Where(X => T.NextTask.Contains(X.TaskID)))
                    {
                        TNext.AddPreTask(T.TaskID);
                    }
                }
            }

            ProjectTasks[] TempTasks = Tasks.Where(x => { return x.PrevTask.Count() == 0; }).ToArray();
            for(int CN = 0; CN < TempTasks.Count(); CN++)
            {
                int NInC = 0;
                TempTasks[CN].NumberInChain = CN;
                TempTasks[CN].NumberInChain = NInC;
                NInC++;
                TaskChainBuilder(TempTasks[CN], ref NInC);
            }
        }

        /// <summary>
        /// recursivly builds a chain (well sets the number in the chain so we can sort on the client side)
        /// probably the fastest way we can do this
        /// </summary>
        /// <param name="T">For non recusive calls the start point| inside the current link in the chain</param>
        /// <param name="NumberInChain">For non recusive calls the start point| inside the current link number in the chain</param>
        void TaskChainBuilder(ProjectTasks T, ref int NumberInChain)
        {
            if (T.NextTask.Count() > 0)
            {
                foreach (int i in T.NextTask)
                {
                    ProjectTasks TNext = Tasks.Find(x => { return x.TaskID == i; });
                    TNext.NumberInChain = NumberInChain;
                    NumberInChain++;
                    TaskChainBuilder(TNext, ref NumberInChain);
                }
            }
            NumberInChain--;
        }
    }
}