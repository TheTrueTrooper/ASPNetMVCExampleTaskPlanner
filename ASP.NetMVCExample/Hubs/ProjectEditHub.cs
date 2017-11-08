using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ASP.NetMVCExample.Models.ProjectView;
using ASP.NetMVCExample.Models;
using ASP.NetMVCExample.Models.__HubModels;
using System.Web.Helpers;

namespace ASP.NetMVCExample.Hubs
{
    static class QuickGroupNamer
    {
        public static string ToProjectGroupName(this int This)
        {
            return "Project#" + This;
        }
    }

    [HubName("ProjectEdit")]
    public class ProjectEditHub : Hub
    {
        static Dictionary<int, ProjectHubSessions> Sessions = new Dictionary<int, ProjectHubSessions>(); 
        
        

        public Task InIt(dynamic Session, int ProjectID)
        {

            return Task.Run(() =>
            {
                if(!Sessions.ContainsKey(ProjectID))
                    Sessions.Add(ProjectID, new ProjectHubSessions(ProjectID, Session, Context.ConnectionId));
                using ()
                    Sessions[ProjectID].AddSession(Session, Context.ConnectionId);
                Groups.Add(Context.ConnectionId, ProjectID.ToProjectGroupName());
                Clients.Group(ProjectID.ToProjectGroupName(), Context.ConnectionId).onJoinNotify();
                Clients.Caller.onInItDone(Sessions[ProjectID]);
            });
        }

        public override Task OnDisconnected(bool stopCalled)
        {

            var SessionsRemovalList = Sessions.Where(x => x.Value.ContainsConnection(Context.ConnectionId)).ToList();
            for(int i = 0; i < SessionsRemovalList.Count; i++)
            {
                SessionsRemovalList[i].Value.RemoveSession(Context.ConnectionId);
                if(SessionsRemovalList[i].Value.SessionCount == 0)
                {
                    Sessions.Remove(SessionsRemovalList[i].Key);
                    SessionsRemovalList.RemoveAt(i);
                    i--;
                }
            }

            return base.OnDisconnected(stopCalled);
        }


    }
}