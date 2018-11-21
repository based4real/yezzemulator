using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class ViewStaffEventListCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_13"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Observa una lista de los eventos abiertos por los Staffs."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            Dictionary<Habbo, UInt32> clients = new Dictionary<Habbo, UInt32>();

            StringBuilder content = new StringBuilder();
            content.Append("Lista de eventos totales abiertos:\r\n");

            foreach (var client in YezzEnvironment.GetGame().GetClientManager()._clients.Values)
            {
                if (client != null && client.GetHabbo() != null && client.GetHabbo().Rank > 5)
                    clients.Add(client.GetHabbo(), (Convert.ToUInt16(client.GetHabbo().Rank)));
            }

            foreach (KeyValuePair<Habbo, UInt32> client in clients.OrderBy(key => key.Value))
            {
                if (client.Key == null)
                    continue;

                content.Append("¥ " + client.Key.Username + " [Rango: " + client.Key.Rank + "] - Ha abierto: " + client.Key._eventsopened + " eventos.\r\n");
            }

            Session.SendMessage(new MOTDNotificationComposer(content.ToString()));

            return;
        }
    }
}