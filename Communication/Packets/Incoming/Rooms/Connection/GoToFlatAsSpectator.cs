using System;
using System.Linq;
using System.Text;

using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Session;
using Yezz.Communication.Packets.Outgoing.Rooms.Engine;
using Yezz.Communication.Packets.Outgoing.Nux;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Users;

namespace Yezz.Communication.Packets.Incoming.Rooms.Connection
{
    class GoToFlatAsSpectatorEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            if (!Session.GetHabbo().InRoom)
                return;

            //Session.GetHabbo().Spectating = true;
            //Session.SendMessage(new RoomSpectatorComposer());

            //Room roomToSpec = Session.GetHabbo().CurrentRoom;
            
            //roomToSpec.QueueingUsers.Remove(Session.GetHabbo());
            //foreach (Habbo user in roomToSpec.QueueingUsers)
            //{
            //    if (roomToSpec.QueueingUsers.First().Id == user.Id)
            //    {
            //        user.PrepareRoom(roomToSpec.Id, "");
            //    }
            //    else
            //    {
            //        user.GetClient().SendMessage(new RoomQueueComposer(roomToSpec.QueueingUsers.IndexOf(user)));
            //    }
            //}
            
            if (!Session.GetHabbo().EnterRoom(Session.GetHabbo().CurrentRoom))
                Session.SendMessage(new CloseConnectionComposer());
        }
    }
}
