using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using ASP.NetMVCExample.Models.ProjectView;
using ASP.NetMVCExample.Models;

namespace ASP.NetMVCExample.Hubs
{
    public class GanttEdit : Hub
    {
        MVCTaskMasterAppDataEntities2 DB = new MVCTaskMasterAppDataEntities2();

        public override Task OnConnected()
        {
            // do the super first but store for future use
            Task Return = base.OnConnected();
            Clients.Client(Context.ConnectionId).GetTaskData();
            return Return;
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        public ProjectTasks GetTaskData(int ProjectID)
        {
            return new Project_IndexView_ViewGanttPerkViewAsSubView(DB, ProjectID);
        }
    }
}