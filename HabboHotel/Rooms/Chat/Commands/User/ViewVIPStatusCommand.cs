﻿using Yezz.HabboHotel.GameClients;

using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Database.Interfaces;
using System.Data;
using System;
using Yezz.Communication.Packets.Outgoing.Rooms.Engine;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class ViewVIPStatusCommand : IChatCommand
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
            get { return "Información de tu suscripción VIP."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            Session.SendMessage(RoomNotificationComposer.SendBubble("abuse", "No eres miembro del Club VIP de Havvo, haz click aquí para abonarte.", ""));
        }
    }
}