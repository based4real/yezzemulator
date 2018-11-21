using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;
using Yezz.Communication.Packets.Outgoing.Inventory.Furni;

using Yezz.Database.Interfaces;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using System.Data;

namespace Yezz.Communication.Packets.Incoming.Rooms.Furni
{
    class CreditFurniRedeemEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (!Session.GetHabbo().InRoom)
                return;

            Room Room;

            if (!YezzEnvironment.GetGame().GetRoomManager().TryGetRoom(Session.GetHabbo().CurrentRoomId, out Room))
                return;

            if (!Room.CheckRights(Session, true))
                return;

            if (YezzEnvironment.GetDBConfig().DBData["exchange_enabled"] != "1")
            {
                Session.SendNotification("De momento no puedes canjear tus monedas para llevarlo a su monedero.");
                return;
            }

            Item Exchange = Room.GetRoomItemHandler().GetItem(Packet.PopInt());
            if (Exchange == null)
                return;

            if (!Exchange.GetBaseItem().ItemName.StartsWith("CF_") && !Exchange.GetBaseItem().ItemName.StartsWith("CFC_") && !Exchange.GetBaseItem().ItemName.StartsWith("DF_") && !Exchange.GetBaseItem().ItemName.StartsWith("DFD_") && !Exchange.GetBaseItem().ItemName.StartsWith("DI_") && !Exchange.GetBaseItem().ItemName.StartsWith("DIA_") && !Exchange.GetBaseItem().ItemName.StartsWith("DIAMND_"))
                return;

            string[] Split = Exchange.GetBaseItem().ItemName.Split('_');
            int Value = int.Parse(Split[1]);


            if (Value > 0)
            {
                if (Exchange.GetBaseItem().ItemName.StartsWith("CF_") || Exchange.GetBaseItem().ItemName.StartsWith("CFC_"))
                {
                    Session.GetHabbo().Credits += Value;
                    Session.SendMessage(new CreditBalanceComposer(Session.GetHabbo().Credits));
                }
                else if (Exchange.GetBaseItem().ItemName.StartsWith("DIA_") || Exchange.GetBaseItem().ItemName.StartsWith("DIAMND_") || Exchange.GetBaseItem().ItemName.StartsWith("DF_") || Exchange.GetBaseItem().ItemName.StartsWith("DFD_") || Exchange.GetBaseItem().ItemName.StartsWith("DI_"))
                {
                    Session.GetHabbo().Diamonds += Value;
                    Session.SendMessage(new HabboActivityPointNotificationComposer(Session.GetHabbo().Diamonds, Value, 5));
                }
            }


            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("DELETE FROM `items` WHERE `id` = '" + Exchange.Id + "' LIMIT 1");
            }

            Session.SendMessage(new FurniListUpdateComposer());
            Room.GetRoomItemHandler().RemoveFurniture(null, Exchange.Id, false);
            Session.GetHabbo().GetInventoryComponent().RemoveItem(Exchange.Id);

        }
    }
}
