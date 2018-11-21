using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class MassiveEventCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_13"; }
        }

        public string Parameters
        {
            get { return "[EVENTO]"; }
        }

        public string Description
        {
            get { return "Ejecuta un EVENT a todos los usuarios en línea."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor escribe el event a lanzar.");
                return;
            }

            string Event = CommandManager.MergeParams(Params, 1);
            YezzEnvironment.GetGame().GetClientManager().SendMessage(new MassEventComposer(Event));
            return;
        }
    }
}
