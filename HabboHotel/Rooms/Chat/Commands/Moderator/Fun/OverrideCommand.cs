using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class OverrideCommand : IChatCommand
    {
        public string PermissionRequired => "user_6";
        public string Parameters => "";
        public string Description => "Caminar sobre cualquier cosa.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (User == null)
                return;

            User.AllowOverride = !User.AllowOverride;
            if (User.AllowOverride)
                Session.SendWhisper("Override Activado!", 34);
            else
                Session.SendWhisper("Override Desactivado!", 34);
        }
    }
}
