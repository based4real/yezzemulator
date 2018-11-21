﻿using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using System;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class EventSwapCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_normal"; }
        }

        public string Parameters
        {
            get { return "%type% %id%"; }
        }

        public string Description
        {
            get { return "Cambie el formato de la alerta seleccionada."; }
        }

        public void Execute(GameClients.GameClient Session, Room Room, string[] Params)
        {
            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (User == null)
                return;

            if (Params.Length == 1)
            {
                Session.SendMessage(new MassEventComposer("habbopages/alertexplanationz.txt"));
                return;
            }

            string Type = Params[1];
            string AlertType = Params[2];

            if (Params.Length == 3)
            {
                switch (Type)
                {
                    case "events":
                        Session.GetHabbo()._eventtype = AlertType;
                        Session.SendWhisper("Has establecido tu alerta de eventos en el tipo " + AlertType + ".", 34);
                        Session.SendWhisper("Recuerda que puedes colocar la alerta anterior diciendo :eventtype events 1 o 2.", 34);
                        break;
                }
            }
        }
    }
}