using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class HotelAlertCommand : IChatCommand
    {
        public string PermissionRequired => "user_11";
        public string Parameters => "[MENSAJE]";
        public string Description => "Enviar alerta al hotel.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor escribe el mensaje a enviar", 34);
                return;
            }
            string Message = CommandManager.MergeParams(Params, 1);
            YezzEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("Mensaje de " + Session.GetHabbo().Username + ":", "<font size =\"11\">Querido usuario de " + YezzEnvironment.HotelName + ", el usuario <b>" + Session.GetHabbo().Username + "</b> tiene un mensaje para todo el hotel:</font><br><br><font size =\"16\" color=\"#B40404\">" + Message + "</font>", "habboarte", ""));
            return;
        }
    }
}
