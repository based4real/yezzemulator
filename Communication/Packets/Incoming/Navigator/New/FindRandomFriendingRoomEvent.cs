using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Rooms.Session;
using Yezz.Communication.Packets.Outgoing.Nux;

namespace Yezz.Communication.Packets.Incoming.Navigator
{
    class FindRandomFriendingRoomEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            var type = Packet.PopString();
            if (type == "predefined_noob_lobby")
            {
                Session.SendMessage(new NuxAlertComposer("nux/lobbyoffer/hide"));
                Session.SendMessage(new RoomForwardComposer(4));
                return;
            }

            Room Instance = YezzEnvironment.GetGame().GetRoomManager().TryGetRandomLoadedRoom();
            if (Instance != null) Session.SendMessage(new RoomForwardComposer(Instance.Id));
        }
    }
}
