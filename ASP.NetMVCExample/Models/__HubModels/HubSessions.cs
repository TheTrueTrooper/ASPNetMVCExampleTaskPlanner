using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NetMVCExample.Models.__HubModels
{
    public class HubSessions
    {
        public Dictionary<string, dynamic> UsersConnected { get; } = new Dictionary<string, dynamic>();

        public int SessionCount { get { return UsersConnected.Count; } }

        public dynamic this[string ConnectionID]
        {
            get
            {
                return UsersConnected[ConnectionID];
            }
        }

        public dynamic this[int SessionUserID]
        {
            get
            {
                return UsersConnected.First(x => { return ((int)x.Value["SessionUserID"]) == SessionUserID; });
            }
        }

        public int GetSessionIDByConnectionID(string ConnectionID)
        {
            return this[ConnectionID]["SessionUserID"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="ConnectionID"></param>
        protected void AddSessionSuper(dynamic Session, string ConnectionID)
        {
            if (!UsersConnected.ContainsKey(ConnectionID))
                UsersConnected.Add(ConnectionID, Session);
            else
                UsersConnected[ConnectionID] = Session;
        }

        /// <summary>
        /// allows for virtual override of AddSessionSuper() dynamic function
        /// This is for a around to dynamic overrides not being able to call from bases
        /// To fix Simply call the reserved name AddSessionSuper(). There will be no loss as that is all the virtual does any way.
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="ConnectionID"></param>
        public virtual void AddSession(dynamic Session, string ConnectionID)
        {
            AddSessionSuper(Session, ConnectionID);
        }

        public void RemoveSession(string ConnectionID)
        {
            UsersConnected.Remove(ConnectionID);
        }

        public bool ContainsConnection(string ConnectionID)
        {
            return UsersConnected.ContainsKey(ConnectionID);
        }

    }
}