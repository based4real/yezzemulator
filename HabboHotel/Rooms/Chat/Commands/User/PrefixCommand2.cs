using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;
using Yezz.Database.Interfaces;
using System.Data;
using Yezz.Communication.Packets.Outgoing.Rooms.Engine;
using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class PrefixCommand2 : IChatCommand
    {
        public string PermissionRequired => "user_vip";
        public string Parameters => "%remove%";
        public string Description => "Borrar prefijo.";
        public void Execute(GameClients.GameClient Session, Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Diga ':prefijo remove' para borrar su prefijo (o Tag)", 34);
                return;
            }

            if (Session.GetHabbo() == null)
                return;

            if (Params[1].ToLower() == "remove")
            {
                if (Session.GetHabbo().Rank >= 1)
                {
                    Session.GetHabbo()._tag = string.Empty;
                    UpdateDatabase(Session);
                }
            }

            Session.SendWhisper("Prefijo (o Tag) removido correctamente!", 34);
            return;
        }

        public void UpdateDatabase(GameClients.GameClient Session)
        {
            if (Session == null || Session.GetHabbo() == null)
                return;

            using (var dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("UPDATE `users` SET `tag` = '" + Session.GetHabbo()._tag + "' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");

            }
        }
    }
}