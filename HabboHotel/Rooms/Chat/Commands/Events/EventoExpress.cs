using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Notifications;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Events
{
    class EventoExpress : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_6"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Crea un nuevo evento express."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("eventos", "Se acaba de abrir un nuevo Evento Express, para más información pincha aquí.", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId + ""));
            return;

        }
    }
}
