using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Rooms.Session;
using Yezz.Communication.Packets.Outgoing.Messenger;

namespace Yezz.Communication.Packets.Incoming.Messenger
{
    class FindNewFriendsEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            Room Instance = YezzEnvironment.GetGame().GetRoomManager().TryGetRandomLoadedRoom();

            if (Instance != null)
            {
                Session.SendMessage(new FindFriendsProcessResultComposer(true));
                Session.SendMessage(new RoomForwardComposer(Instance.Id));
            }
            else
            {
                Session.SendMessage(new FindFriendsProcessResultComposer(false));
            }
        }
    }
}
