using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class MassEnableCommand : IChatCommand
    {
        public string PermissionRequired => "user_12";
        public string Parameters => "[EFFECTID]";
        public string Description => "Efecto a cada usuario en la sala.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor introduce el efecto ID.", 34);
                return;
            }

            int EnableId = 0;
            if (int.TryParse(Params[1], out EnableId))
            {
                if (EnableId == 102 || EnableId == 178)
                {
                    Session.SendWhisper("Hay kekos que no pueden usar este efecto, pendejo.", 34);
                    return;
                }

                if (!Session.GetHabbo().GetPermissions().HasCommand("command_override_massenable") && Room.OwnerId != Session.GetHabbo().Id)
                {
                    Session.SendWhisper("Este comando lo puedes usar solo si eres el dueño.", 34);
                    return;
                }

                List<RoomUser> Users = Room.GetRoomUserManager().GetRoomUsers();
                if (Users.Count > 0)
                {
                    foreach (RoomUser U in Users.ToList())
                    {
                        if (U == null || U.RidingHorse)
                            continue;

                        U.ApplyEffect(EnableId);
                    }
                }
            }
            else
            {
                Session.SendWhisper("Por favor introduce el efecto ID.", 34);
                return;
            }

        }
    }
}
