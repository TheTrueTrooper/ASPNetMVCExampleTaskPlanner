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
using System.ComponentModel.DataAnnotations;

namespace ASP.NetMVCExample.Hubs
{
    static internal class QuickhubHelper
    {
        /// <summary>
        /// quickly converts a Int value to a group name
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static string ToProjectGroupName(this int This)
        {
            return "Project#" + This;
        }


        /// <summary>
        /// Quickly Checks an obj as a model for correctness
        /// </summary>
        /// <param name="Object"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public static bool TryValidate(this object Object, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(Object, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                Object, context, results,
                validateAllProperties: true
            );
        }
    }

    [HubName("ProjectEdit")]
    public class ProjectEditHub : Hub
    {
        static Dictionary<int, ProjectHubSessions> Sessions = new Dictionary<int, ProjectHubSessions>();

        /// <summary>
        /// this will notify other users of a change and show the document changes
        /// </summary>
        /// <param name="ProjectID"></param>
        public void TryUpdateChange(int ProjectID, ASP.NetMVCExample.Models.ProjectView.ProjectTasks Change)
        {
            ICollection<ValidationResult> ValidationResults;
            bool ChangeSuccess = Change.TryValidate(out ValidationResults);
            //if (ChangeSuccess) //update task and database
                
            //else if (Sessions[ProjectID].UnSavedTasks.ContainsKey(Change.TaskID)) // else up date task if exists
            
            //else // make new Task for

            Clients.Group(ProjectID.ToProjectGroupName(), Context.ConnectionId).NotifyChange(ChangeSuccess, Change);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="Change"></param>
        public void DeleteTask(int ProjectID, ASP.NetMVCExample.Models.ProjectView.ProjectTasks Change)
        {
            Clients.Group(ProjectID.ToProjectGroupName(), Context.ConnectionId).NotifyChange(Change.TaskID);
        }


        /// <summary>
        /// this is called after a successfull connect.
        /// here we build a session fro the user to extend from their actual session 
        /// then we call back to the client
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="ProjectID"></param>
        public void InIt(dynamic Session, int ProjectID)
        {
            
            //return Task.Run(() =>
            //{
                if(!Sessions.ContainsKey(ProjectID))
                    Sessions.Add(ProjectID, new ProjectHubSessions(ProjectID, Session, Context.ConnectionId));
                //using ()
                Sessions[ProjectID].AddSession(Session, Context.ConnectionId);
                Groups.Add(Context.ConnectionId, ProjectID.ToProjectGroupName());
                Clients.Group(ProjectID.ToProjectGroupName(), Context.ConnectionId).onJoinNotify();
                Clients.Caller.onInItDone(Sessions[ProjectID]);
            //});
        }

        /// <summary>
        /// this is called any time we disconnect
        /// - here we will remove our self from the hubs groupings
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
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



/*
 * //validation objects
public class Recipe : IValidatableObject 
{
    [Required]
    public string Name { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // ...
    }
}
 * 
 * 
var recipe = new Recipe();
var context = new ValidationContext(recipe, serviceProvider: null, items: null);
var results = new List<ValidationResult>();

var isValid = Validator.TryValidateObject(recipe, context, results);

if (!isValid)
{
    foreach (var validationResult in results)
    {
        Console.WriteLine(validationResult.ErrorMessage);
    }
}
 * 
public class DataAnnotationsValidator
{
    public bool TryValidate(object @object, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(@object, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(
            @object, context, results, 
            validateAllProperties: true
        );
    }
}
 * 
 */
