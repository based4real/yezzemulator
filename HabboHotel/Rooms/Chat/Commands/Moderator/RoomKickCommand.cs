using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class RoomKickCommand : IChatCommand
    {
        public string PermissionRequired => "user_10";
        public string Parameters => "[MENSAJE]";
        public string Description => "Expulsar a todos los usuarios de estas ala.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor dale una razon a los usuarios.");
                return;
            }

            string Message = CommandManager.MergeParams(Params, 1);
            foreach (RoomUser RoomUser in Room.GetRoomUserManager().GetUserList().ToList())
            {
                if (RoomUser == null || RoomUser.IsBot || RoomUser.GetClient() == null || RoomUser.GetClient().GetHabbo() == null || RoomUser.GetClient().GetHabbo().GetPermissions().HasRight("mod_tool") || RoomUser.GetClient().GetHabbo().Id == Session.GetHabbo().Id)
                    continue;

                RoomUser.GetClient().SendNotification("Usted ha sido expulsado por un moderador por la siguiente razón: " + Message);

                Room.GetRoomUserManager().RemoveUserFromRoom(RoomUser.GetClient(), true, false);
            }

            Session.SendWhisper("Expulso a todos correctamente", 34);
        }
    }
}
