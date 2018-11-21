using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class KickCommand : IChatCommand
    {
        public string PermissionRequired => "user_10";
        public string Parameters => "[USUARIO] [MENSAJE]";
        public string Description => "Expulsar al usuario y enviarle la razón por la cual.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Escribe el nombre del usuario.", 34);
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("Ocurrio un error, al parecer no existe el usuario o no se encuentra online.", 34);
                return;
            }

            if (TargetClient.GetHabbo() == null)
            {
                Session.SendWhisper("Ocurrio un error, al parecer no existe el usuario o no se encuentra online.", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Get a life.", 34);
                return;
            }

            if (!TargetClient.GetHabbo().InRoom)
            {
                Session.SendWhisper("El usuario no se encuentra en la sala", 34);
                return;
            }

            Room TargetRoom;
            if (!YezzEnvironment.GetGame().GetRoomManager().TryGetRoom(TargetClient.GetHabbo().CurrentRoomId, out TargetRoom))
                return;

            if (Params.Length > 2)
                TargetClient.SendNotification("Un moderador te ha expulsado de la sala por la siguiente razón: " + CommandManager.MergeParams(Params, 2));
            else
                TargetClient.SendNotification("Un moderador te ha expulsado de la sala.");

            TargetRoom.GetRoomUserManager().RemoveUserFromRoom(TargetClient, true, false);
        }
    }
}
