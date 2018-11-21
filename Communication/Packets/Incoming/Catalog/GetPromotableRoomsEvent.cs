using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Catalog;

namespace Yezz.Communication.Packets.Incoming.Catalog
{
    class GetPromotableRoomsEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            List<RoomData> Rooms = Session.GetHabbo().UsersRooms;
            Rooms = Rooms.Where(x => (x.Promotion == null || x.Promotion.TimestampExpires < YezzEnvironment.GetUnixTimestamp())).ToList();
            Session.SendMessage(new PromotableRoomsComposer(Rooms));
        }
    }
}
