using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Session;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class SummonCommand : IChatCommand
    {
        public string PermissionRequired => "user_7";
        public string Parameters => "[USUARIO]";
        public string Description => "Traer a un usuario a la sala actual.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor introduce el nombre del usuario que deseas traer a la sala", 34);
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("Ocurrio un error, no se encuentra al usuario o tal vez no esta online", 34);
                return;
            }

            if (TargetClient.GetHabbo() == null)
            {
                Session.SendWhisper("Ocurrio un error, no se encuentra al usuario o tal vez no esta online", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Get a life.", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == "Forbi" || TargetClient.GetHabbo().Username == "Forb")
            {
                Session.SendWhisper("¡No puedes darle summon a ese usuario!", 34);
                return;
            }

            TargetClient.SendMessage(RoomNotificationComposer.SendBubble("volada", "¡Usted ha sido llamado por " + Session.GetHabbo().Username + "!", ""));
            if (!TargetClient.GetHabbo().InRoom)
                TargetClient.SendMessage(new RoomForwardComposer(Session.GetHabbo().CurrentRoomId));
            else
                TargetClient.GetHabbo().PrepareRoom(Session.GetHabbo().CurrentRoomId, "");
        }
    }
}