using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class CustomizedHotelAlert : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_11"; }
        }

        public string Parameters
        {
            get { return "[MENSAJE]"; }
        }

        public string Description
        {
            get { return "Envia un mensaje a todo el Hotel"; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor escribe el mensaje a enviar.");
                return;
            }

            string Message = CommandManager.MergeParams(Params, 1);
            YezzEnvironment.GetGame().GetClientManager().SendMessage(new RoomCustomizedAlertComposer("\n" + Message + "\n\n - " + Session.GetHabbo().Username + ""));
            return;
        }
    }
}
