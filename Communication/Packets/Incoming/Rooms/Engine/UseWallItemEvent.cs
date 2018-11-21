using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Quests;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Items;
using Yezz.HabboHotel.Items.Wired;
using Yezz.Communication.Packets.Outgoing.Rooms.Engine;


namespace Yezz.Communication.Packets.Incoming.Rooms.Engine
{
    class UseWallItemEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.GetHabbo() == null || !Session.GetHabbo().InRoom)
                return;

            Room Room;

            if (!YezzEnvironment.GetGame().GetRoomManager().TryGetRoom(Session.GetHabbo().CurrentRoomId, out Room))
                return;

            int itemID = Packet.PopInt();
            Item Item = Room.GetRoomItemHandler().GetItem(itemID);
            if (Item == null)
                return;

            bool hasRights = false;
            if (Room.CheckRights(Session, false, true))
                hasRights = true;

            string oldData = Item.ExtraData;
            int request = Packet.PopInt();

            Item.Interactor.OnTrigger(Session, Item, request, hasRights);
            Item.GetRoom().GetWired().TriggerEvent(WiredBoxType.TriggerStateChanges, Session.GetHabbo(), Item);
        
            YezzEnvironment.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.EXPLORE_FIND_ITEM, Item.GetBaseItem().Id);

            //IMPORTANTE
        }
    }
}
