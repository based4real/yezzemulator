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

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class ViewOnlineCommand : IChatCommand
    {
        public string PermissionRequired => "user_6";
        public string Parameters => "";
        public string Description => "Ver los usuarios online.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            Dictionary<Habbo, UInt32> clients = new Dictionary<Habbo, UInt32>();

            StringBuilder content = new StringBuilder();
            content.Append("- LISTA DE LOS USUARIOS ONLINE -\r\n");

            foreach (var client in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList())
            {
                if (client == null)
                    continue;

                content.Append("¥ " + client.GetHabbo().Username + " » Se encuentra en la sala: " + ((client.GetHabbo().CurrentRoom == null) ? "En ninguna sala." : client.GetHabbo().CurrentRoom.RoomData.Name) + "\r\n");
            }

            Session.SendMessage(new MOTDNotificationComposer(content.ToString()));
            return;
        }
    }
}
