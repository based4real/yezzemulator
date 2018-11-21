using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Users;
using Yezz.Communication.Packets.Outgoing.Notifications;


using Yezz.Communication.Packets.Outgoing.Handshake;
using Yezz.Communication.Packets.Outgoing.Quests;
using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.HabboHotel.Quests;
using Yezz.HabboHotel.Rooms;
using System.Threading;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Avatar;
using Yezz.Communication.Packets.Outgoing.Pets;
using Yezz.Communication.Packets.Outgoing.Messenger;
using Yezz.HabboHotel.Users.Messenger;
using Yezz.Communication.Packets.Outgoing.Rooms.Polls;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Availability;
using Yezz.Communication.Packets.Outgoing;
using Yezz.Communication.Packets.Outgoing.Nux;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class PriceList : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_info"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Ver la lista de precios de raros."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            StringBuilder List = new StringBuilder("");
            List.AppendLine("                          ¥ LISTA DE PRECIOS DE HABBI¥");
            List.AppendLine("   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets");
            List.AppendLine("   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets");
            List.AppendLine("   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets");
            List.AppendLine("   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets");
            List.AppendLine("   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets");
            List.AppendLine("   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets   »   SOFÁ VIP: Duckets");
            List.AppendLine("Esta lista todavía está en construcción por Custom, su última actualización fue el día 28 de Julio de 2016.");
            Session.SendMessage(new MOTDNotificationComposer(List.ToString()));


        }
    }
}