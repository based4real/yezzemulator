using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Moderation;

namespace Yezz.Communication.Packets.Incoming.Moderation
{
    class GetModeratorRoomInfoEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (!Session.GetHabbo().GetPermissions().HasRight("mod_tool"))
                return;

            int RoomId = Packet.PopInt();

            RoomData Data = YezzEnvironment.GetGame().GetRoomManager().GenerateRoomData(RoomId);
            if (Data == null)
                return;

            Room Room;

            if (!YezzEnvironment.GetGame().GetRoomManager().TryGetRoom(RoomId, out Room))
                return;

            Session.SendMessage(new ModeratorRoomInfoComposer(Data, (Room.GetRoomUserManager().GetRoomUserByHabbo(Data.OwnerName) != null)));
        }
    }
}
