using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Rooms;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class FollowCommand : IChatCommand
    {
        public string PermissionRequired => "user_normal";
        public string Parameters => "[USUARIO]";
        public string Description => "Seguir a un usuario a la sala en la que esté.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Introduce el nombre correctamente.", 34);
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("Ocurrio un error, escribe correctamente el nombre o el usuario no se encuentra online.", 34);
                return;
            }

            if (TargetClient.GetHabbo().CurrentRoom == Session.GetHabbo().CurrentRoom)
            {
                Session.SendWhisper("Hey! Abre los ojos, el usuario " + TargetClient.GetHabbo().Username + " esta en esta sala!", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == "Forbi" || TargetClient.GetHabbo().Username == "Forb")
            {
                Session.SendWhisper("¡No puedes seguir a ese usuario!", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Sadooooooooo!", 34);
                return;
            }

            if (!TargetClient.GetHabbo().InRoom)
            {
                Session.SendWhisper("El no esta en ninguna sala", 34);
                return;
            }

            if (TargetClient.GetHabbo().CurrentRoom.Access != RoomAccess.OPEN && !Session.GetHabbo().GetPermissions().HasRight("mod_tool"))
            {
                Session.SendWhisper("Oops, el usuario esta en una sala cerrada con timbre o contraseña, no puedes seguirlo!", 34);
                return;
            }

            Session.GetHabbo().PrepareRoom(TargetClient.GetHabbo().CurrentRoom.RoomId, "");
        }
    }
}
