using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Rooms.Furni.RentableSpaces;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Items;
using Yezz.HabboHotel.Items.RentableSpaces;

namespace Yezz.Communication.Packets.Incoming.Rooms.Furni.RentableSpaces
{
    class BuyRentableSpaceEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {

            int itemId = Packet.PopInt();

            Room room;
            if (!YezzEnvironment.GetGame().GetRoomManager().TryGetRoom(Session.GetHabbo().CurrentRoomId, out room))
                return;

            if (room == null || room.GetRoomItemHandler() == null)
                return;

            RentableSpaceItem rsi;
            if (YezzEnvironment.GetGame().GetRentableSpaceManager().GetRentableSpaceItem(itemId, out rsi))
            {
                YezzEnvironment.GetGame().GetRentableSpaceManager().ConfirmBuy(Session, rsi, 3600);
            }


        }
    }
}