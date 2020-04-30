using ASP.NetMVCExample.Models.ProjectView;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample.Models.__HubModels
{
    public class ProjectHubSessions : HubSessions
    {
        public int ProjectID { get; private set; }
        public Project_Tasks_And_Links ProjectTasks;
        public SelectProjectByID_Result ProjectOverView;
        public Dictionary<int, ProjectTasks> UnSavedTasks = new Dictionary<int, ProjectView.ProjectTasks>();

        public ProjectHubSessions(int ProjectID, dynamic Session, string ConnectionID)
        {
            this.ProjectID = ProjectID;
            using (MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2())
            {
                using (ObjectResult<SelectProjectByID_Result> oj = DB.SelectProjectByID(ProjectID))
                {
                    ProjectOverView = oj.First();
                }

                ProjectTasks = new Project_Tasks_And_Links(DB, ProjectID);


                AddSession(Session, ConnectionID);
                UsersConnected[ConnectionID]["Permission"] = DB.ValidateWithProjectViewPriv((int)UsersConnected[ConnectionID]["SessionUserID"], (string)UsersConnected[ConnectionID]["SessionCode"], ProjectID);
            }
        }

        public override void AddSession(dynamic Session, string ConnectionID)
        {
            AddSessionSuper(Session, ConnectionID);
            using (MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2())
            {
                AddSession(Session, ConnectionID);
                UsersConnected[ConnectionID]["Permission"] = DB.ValidateWithProjectViewPriv((int)UsersConnected[ConnectionID]["SessionUserID"], (string)UsersConnected[ConnectionID]["SessionCode"], ProjectID);
            }
        }

    }
}