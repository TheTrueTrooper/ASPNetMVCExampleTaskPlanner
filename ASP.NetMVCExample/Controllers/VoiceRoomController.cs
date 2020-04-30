using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace ASP.NetMVCExample.Controllers
{
    public class VoiceRoomController : Controller
    {
        //internal class VoiceRooms
        //{
        //    //static Dictionary<int, VoiceRooms> RoomList = new Dictionary<int, VoiceRooms>();

        //    //List<VoiceClient> Clients = new List<VoiceClient>();



        //    //    internal void BrodCastAllOthers(VoiceClient Client)
        //    //    {
        //    //        VoiceClient[] ListeningClients;
        //    //        lock (Clients)
        //    //        {
        //    //            ListeningClients = Clients.Where(x => { return !x.Equals(Client); }).ToArray();
        //    //        }
        //    //        for (int i = 0; i < ListeningClients.Count(); i++)
        //    //            ListeningClients[i].Write();

        //    //    }

        //    //    static public async Task VoiceSocket(AspNetWebSocketContext webSocketContext)
        //    //    {

        //    //        int Room = int.Parse(webSocketContext.QueryString["RoomID"]);
        //    //        VoiceClient NewClient = new VoiceClient(webSocketContext, RoomList[Room]);

        //    //        if (!RoomList.ContainsKey(Room))
        //    //        {
        //    //            VoiceRooms NewRoom = new VoiceRooms();
        //    //            RoomList.Add(Room, NewRoom);
        //    //        }

        //    //        lock (RoomList)
        //    //        {
        //    //            RoomList[Room].Clients.Add(NewClient);
        //    //        }

        //    //        Task Tx = Task.Run(NewClient.ListenLoop);
        //    //        Task Rx = Task.Run(NewClient.WriteLoop);

        //    //        await Tx;
        //    //        await Rx;
        //    //    }
        //    //}

        //    //internal class VoiceClient
        //    //{
        //    //    const int maxMessageSize = 1024;


        //    //    ArraySegment<Byte> TxBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);
        //    //    ArraySegment<Byte> RxBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);

        //    //    CancellationToken CancellationToken = new CancellationToken();

        //    //    AspNetWebSocketContext WebSocketContext;
        //    //    WebSocket WebSocket;
        //    //    VoiceRooms CurrentRoom;


        //    //    internal VoiceClient(AspNetWebSocketContext webSocketContext, VoiceRooms voiceRooms)
        //    //    {
        //    //        WebSocketContext = webSocketContext;
        //    //        CurrentRoom = voiceRooms;
        //    //        WebSocket = WebSocketContext.WebSocket;

        //    //    }

        //    //    internal async Task ListenLoop()
        //    //    {
        //    //        while (WebSocket.State == WebSocketState.Open)
        //    //        {
        //    //            WebSocketReceiveResult WebSocketReceiveResult = await WebSocket.ReceiveAsync(TxBuffer, CancellationToken);

        //    //        }
        //    //    }

        //    //    internal async Task WriteLoop()
        //    //    {
        //    //        while (WebSocket.State == WebSocketState.Open)
        //    //        {

        //    //            await WebSocket.SendAsync(RxBuffer, WebSocketMessageType.Binary, false, CancellationToken);

        //    //        }
        //    //    }



        //    //}



        //    //// GET: VoiceRoom
        //    //public void Index(int RoomID)
        //    //{
        //    //    if(HttpContext.IsWebSocketRequest)
        //    //    {
        //    //        HttpContext.AcceptWebSocketRequest(VoiceRooms.VoiceSocket);
        //    //        //return HttpContext.Response;
        //    //    }
        //    //}
        //}

    }
}