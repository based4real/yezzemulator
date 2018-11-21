using System;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Nux;
using Yezz.Communication.Packets.Outgoing.Rooms.Furni.RentableSpaces;
using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.Communication.Packets.Outgoing.Users;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class ControlCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_16"; }
        }

        public string Parameters
        {
            get { return "<usuario>"; }
        }

        public string Description
        {
            get { return "Controla al usuario que selecciones."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (Params.Length != 2)
            {
                Session.SendWhisper("Introduce el nombre del usuario a quien deseas enviar una placa!");
                return;
            }

            if (Params.Length == 2 && Params[1] == "end")
            {
                Session.SendWhisper("Has dejado de controlar a " + Session.GetHabbo().Opponent +".");
                Session.GetHabbo().isControlling = false;
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient != null)
            {
                Session.GetHabbo().Opponent = TargetClient.GetHabbo().Username;
                Session.GetHabbo().isControlling = true;
                Session.SendMessage(RoomNotificationComposer.SendBubble("definitions", "Ahora estás controlando a " + TargetClient.GetHabbo().Username + ". Para parar di :control end."));
                return;
            }

            else Session.SendMessage(RoomNotificationComposer.SendBubble("definitions", "No se ha encontrado el usuario " + Params[1] + ".", ""));
        }
    }
}
