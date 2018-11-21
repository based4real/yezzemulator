﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Pathfinding;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class AllEyesOnMeCommand : IChatCommand
    {
        public string PermissionRequired => "user_4";
        public string Parameters => "";
        public string Description => "Quieres un poco de Atencion? todos te miran";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            List<RoomUser> Users = Room.GetRoomUserManager().GetRoomUsers();
            foreach (RoomUser U in Users.ToList())
            {
                if (U == null || Session.GetHabbo().Id == U.UserId)
                    continue;

                U.SetRot(Rotation.Calculate(U.X, U.Y, ThisUser.X, ThisUser.Y), false);
            }
        }
    }
}
