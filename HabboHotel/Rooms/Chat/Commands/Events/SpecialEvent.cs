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


namespace Yezz.HabboHotel.Rooms.Chat.Commands.Events
{
    internal class SpecialEvent : IChatCommand
    {
        public string PermissionRequired
        {
            get
            {
                return "user_13";
            }
        }
        public string Parameters
        {
            get { return "[EXPLICACION]"; }
        }
        public string Description
        {
            get
            {
                return "Manda un evento a todo el hotel.";
            }
        }
        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor, digite un mensaje para enviar.", 34);
                return;
            }
            else
            {
                string Message = CommandManager.MergeParams(Params, 1);

                YezzEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("¿Qué está pasando en " + YezzEnvironment.HotelName + "...?",
                     Message, "event_image", "¡A la aventura!", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));
            }

        }
    }
}

