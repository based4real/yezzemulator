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
    class ColourList : IChatCommand
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
            get { return "Información de Yezz."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            Session.SendMessage(new RoomNotificationComposer("Lista de colores:",
                 "<font color='#FF8000'><b>COLORES:</b>\n" +
                 "<font size=\"12\" color=\"#1C1C1C\">El comando :color te permitirá fijar un color que tu desees en tu bocadillo de chat, para poder seleccionar el color deberás especificarlo después de hacer el comando, como por ejemplo:<br><i>:color red</i></font>" +
                 "<font size =\"13\" color=\"#0B4C5F\"><b>Stats:</b></font>\n" +
                 "<font size =\"11\" color=\"#1C1C1C\">  <b> · Users: </b> \r  <b> · Rooms: </b> \r  <b> · Uptime: </b>minutes.</font>\n\n" +
                 "", "quantum", ""));
        }
    }
}