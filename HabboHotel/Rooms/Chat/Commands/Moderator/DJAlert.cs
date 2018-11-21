using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class DJAlert : IChatCommand
    {
        public string PermissionRequired => "user_6";
        public string Parameters => "[MENSAJE]";
        public string Description => "Envía una alerta a todo el hotel de emisión.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor escribe el mensaje a enviar");
                return;
            }
            string Message = CommandManager.MergeParams(Params, 1);
            YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("DJAlertNEW", "¡DJ " + Message + " está emitiendo en vivo! Sintoniza JabbozFM ahora mismo y disfruta al máximo.", ""));
            return;
        }
    }
}
