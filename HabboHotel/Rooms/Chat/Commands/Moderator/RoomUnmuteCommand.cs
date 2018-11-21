using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class RoomUnmuteCommand : IChatCommand
    {
        public string PermissionRequired => "user_10";
        public string Parameters => "";
        public string Description => "Desmutear sala.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (!Room.RoomMuted)
            {
                Session.SendWhisper("Esta habitación no está muteada.", 34);
                return;
            }

            Room.RoomMuted = false;

            List<RoomUser> RoomUsers = Room.GetRoomUserManager().GetRoomUsers();
            if (RoomUsers.Count > 0)
            {
                foreach (RoomUser User in RoomUsers)
                {
                    if (User == null || User.GetClient() == null || User.GetClient().GetHabbo() == null || User.GetClient().GetHabbo().Username == Session.GetHabbo().Username)
                        continue;

                    User.GetClient().SendWhisper("Esta sala ha sido desmuteada, puedes volver a hablar con normalidad.", 34);
                }
            }
        }
    }
}