﻿
using Yezz.Communication.Packets.Outgoing.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class SendGraphicAlertCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_13"; }
        }

        public string Parameters
        {
            get { return "[IMAGEN]"; }
        }

        public string Description
        {
            get { return "Envía un mensaje de alerta con imagen a todo el hotel."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor escribe el nombre de la imagen a enviar.");
                return;
            }

            string image = Params[2];

            YezzEnvironment.GetGame().GetClientManager().SendMessage(new GraphicAlertComposer(image));
            return;
        }
    }
}
