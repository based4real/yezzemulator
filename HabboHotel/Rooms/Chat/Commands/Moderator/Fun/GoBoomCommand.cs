using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class GoBoomCommand : IChatCommand
    {
        public string PermissionRequired => "user_6";
        public string Parameters => "";
        public string Description => "Hacer boom! (Se aplica el efecto 108)";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            List<RoomUser> Users = Room.GetRoomUserManager().GetRoomUsers();
            if (Users.Count > 0)
            {
                foreach (RoomUser U in Users.ToList())
                {
                    if (U == null)
                        continue;

                    U.ApplyEffect(108);
                }
            }
        }
    }
}
