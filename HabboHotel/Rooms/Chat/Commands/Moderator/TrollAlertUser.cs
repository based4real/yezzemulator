using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Notifications;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class TrollAlertUser : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_6"; }
        }

        public string Parameters
        {
            get { return "[MENSAJE]"; }
        }

        public string Description
        {
            get { return "Enviale un mensaje de alerta a todos los staff online."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Escribe el mensaje que deseas enviar.", 34);
                return;
            }

            string Image = CommandManager.MergeParams(Params, 3);
            string Message = CommandManager.MergeParams(Params, 1);
            string figure = Session.GetHabbo().Look;

            YezzEnvironment.GetGame().GetClientManager().MsgAlert2(RoomNotificationComposer.SendBubble("fig/" + figure, Message + "", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId + ""));
            return;

        }
    }
}
