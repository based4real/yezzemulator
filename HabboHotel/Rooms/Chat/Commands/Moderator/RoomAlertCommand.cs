using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class RoomAlertCommand : IChatCommand
    {
        public string PermissionRequired => "user_10";
        public string Parameters => "[MENSAJE]";
        public string Description => "Enviar mensaje a todos en la sala.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor introduce el mensaje que deseas enviar en la sala");
                return;
            }

            if (!Session.GetHabbo().GetPermissions().HasRight("mod_alert") && Room.OwnerId != Session.GetHabbo().Id)
            {
                Session.SendWhisper("Solo puede hacerlo en su propia habitacion..", 34);
                return;
            }

            string Message = CommandManager.MergeParams(Params, 1);
            foreach (RoomUser RoomUser in Room.GetRoomUserManager().GetRoomUsers())
            {
                if (RoomUser == null || RoomUser.GetClient() == null || Session.GetHabbo().Id == RoomUser.UserId)
                    continue;

                YezzEnvironment.GetGame().GetClientManager().SendMessage(new RoomCustomizedAlertComposer(Message + "\n\n- " + Session.GetHabbo().Username));
            }
            Session.SendWhisper("Mensaje enviado correctamente en la sala.", 34);
        }
    }
}
