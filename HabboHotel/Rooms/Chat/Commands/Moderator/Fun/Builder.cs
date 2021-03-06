﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

using Yezz.HabboHotel.Rooms;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class Builder : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_normal"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Habilita el teletransporte en tu sala para construir con más facilidad."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (!Room.CheckRights(Session, true))
                return;

            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (User == null)
                return;


            User.TeleportEnabled = !User.TeleportEnabled;
            Room.GetGameMap().GenerateMaps();

            Session.SendMessage(RoomNotificationComposer.SendBubble("builders_club_room_locked_small", "Acabas de activar el modo de constructor.", ""));
        }
    }
}
