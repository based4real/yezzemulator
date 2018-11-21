using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class RoomBadgeCommand : IChatCommand
    {
        public string PermissionRequired => "user_12";
        public string Parameters => "[CODIGO]";
        public string Description => "Dar placa a toda la sala.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Introduce el codigo de la placa que deseas enviar en esta sala.");
                return;
            }

            foreach (RoomUser User in Room.GetRoomUserManager().GetUserList().ToList())
            {
                if (User == null || User.GetClient() == null || User.GetClient().GetHabbo() == null)
                    continue;

                if (!User.GetClient().GetHabbo().GetBadgeComponent().HasBadge(Params[1]))
                {
                    User.GetClient().GetHabbo().GetBadgeComponent().GiveBadge(Params[1], true, User.GetClient());
                    User.GetClient().SendMessage(RoomNotificationComposer.SendBubble("badge/" + Params[1], "Acabas de recibir una placa!", "/inventory/open/badge"));
                }
                else
                    User.GetClient().SendMessage(RoomNotificationComposer.SendBubble("erro", Session.GetHabbo().Username + " Trató de darle una placa, pero ya la tienes!", "/inventory/open/badge"));
            }
            Session.SendWhisper("Usted ha dado con éxito a cada usuario en esta sala la placa " + Params[2] + "!", 34);
        }
    }
}
