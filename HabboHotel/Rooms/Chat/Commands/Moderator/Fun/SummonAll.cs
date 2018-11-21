using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Session;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class SummonAll : IChatCommand
    {
        public string PermissionRequired => "user_13";
        public string Parameters => "";
        public string Description => "Trae a todos los usuarios.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {

            foreach (GameClient Client in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList())
            {
                if (Client == null || Client.GetHabbo() == null || Client.GetHabbo().Username == Session.GetHabbo().Username)
                    continue;

                if (Client.GetHabbo().InRoom && Client.GetHabbo().CurrentRoomId != Session.GetHabbo().CurrentRoomId)
                {
                    Client.SendMessage(new RoomForwardComposer(Session.GetHabbo().CurrentRoomId));
                    Client.SendMessage(RoomNotificationComposer.SendBubble("volada", "¡Usted ha sido llamado por " + Session.GetHabbo().Username + "!", ""));
                }
                else if (!Client.GetHabbo().InRoom)
                {
                    Client.SendMessage(new RoomForwardComposer(Session.GetHabbo().CurrentRoomId));
                    Client.SendMessage(RoomNotificationComposer.SendBubble("volada", "¡Usted ha sido llamado por " + Session.GetHabbo().Username + "!", ""));
                }
                else if (Client.GetHabbo().CurrentRoomId == Session.GetHabbo().CurrentRoomId)
                {
                    Client.SendWhisper("Vaya, parece que se acaba de traer a todo el hotel en la sala en la que te encuentras...", 34);
                }
            }

            Session.SendWhisper("Acabas de atraer a todo el puto hotel men.");


        }
    }
}
