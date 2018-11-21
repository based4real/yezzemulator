using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Navigator;

namespace Yezz.Communication.Packets.Incoming.Navigator
{
    class UpdateNavigatorSettingsEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            int roomID = Packet.PopInt();
            if (roomID == 0)
                return;

            RoomData Data = YezzEnvironment.GetGame().GetRoomManager().GenerateRoomData(roomID);
            if (Data == null)
                return;

            Session.GetHabbo().HomeRoom = roomID;
            Session.SendMessage(new NavigatorSettingsComposer(roomID));
        }
    }
}
