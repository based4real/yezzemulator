﻿using Yezz.Communication.Packets;
using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.HabboHotel.Rooms.Instance
{
    class FootballGateComponent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.GetHabbo() == null)
                return;

            int RoomId = Session.GetHabbo().CurrentRoomId;

            Room Room = Session.GetHabbo().CurrentRoom;
            if (Room == null || !Room.CheckRights(Session, true))
                return;

            int ItemId = Packet.PopInt();

            Item Item = Room.GetRoomItemHandler().GetItem(ItemId);
            if (Item == null || Item.GetBaseItem() == null || Item.GetBaseItem().InteractionType != InteractionType.FOOTBALL_GATE)
                return;

            string Gender = Packet.PopString();
            string Look = Packet.PopString();

            if (Gender.ToUpper() == "M")
            {
                Item.ExtraData = Look + "," + Item.ExtraData.Split(',')[1];
            }
            else if (Gender.ToUpper() == "F")
            {
                Item.ExtraData = Item.ExtraData.Split(',')[0] + "," + Look;
            }

            Item.UpdateState();
        }
    }
}