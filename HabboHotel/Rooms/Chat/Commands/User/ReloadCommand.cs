﻿using System.Linq;
using System.Collections.Generic;
using Yezz.Communication.Packets.Outgoing.Rooms.Session;


namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class Reloadcommand : IChatCommand
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
            get { return "Recarga la sala"; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Session.GetHabbo().Id != Room.OwnerId && !Session.GetHabbo().GetPermissions().HasRight("room_any_owner"))
            {
                Session.SendWhisper("Lo sentimos, este comando solo está disponible si eres el propietario de la sala");
                return;
            }

            List<RoomUser> UsersToReturn = Room.GetRoomUserManager().GetRoomUsers().ToList();

            YezzEnvironment.GetGame().GetRoomManager().UnloadRoom(Room);


            foreach (RoomUser User in UsersToReturn)
            {
                if (User == null || User.GetClient() == null)
                    continue;

                User.GetClient().SendMessage(new RoomForwardComposer(Room.Id));
            }


        }
    }
}
