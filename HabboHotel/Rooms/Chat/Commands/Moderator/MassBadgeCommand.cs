using System.Linq;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class MassBadgeCommand : IChatCommand
    {
        public string PermissionRequired => "user_13";
        public string Parameters => "[CODIGO]";
        public string Description => "Dar placas a todo el hotel.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor introduce el codigo de la placa que deseas enviar a todos");
                return;
            }

            foreach (GameClient Client in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList())
            {
                if (Client == null || Client.GetHabbo() == null || Client.GetHabbo().Username == Session.GetHabbo().Username)
                    continue;

                if (!Client.GetHabbo().GetBadgeComponent().HasBadge(Params[1]))
                {
                    Client.GetHabbo().GetBadgeComponent().GiveBadge(Params[1], true, Client);
                    Client.SendMessage(RoomNotificationComposer.SendBubble("badge/" + Params[1], Session.GetHabbo().Username + " te acaba de enviar la placa " + Params[1] + ".", "/inventory/open/badge"));
                }
                else
                    Client.SendMessage(RoomNotificationComposer.SendBubble("erro", "" + Session.GetHabbo().Username + " ha intentado enviarte la placa " + Params[1] + " pero ya la tienes.", ""));
            }

            Session.SendWhisper("Usted le ha dado con exito a cada uno de los del hotel la placa " + Params[1] + "!", 34);
        }
    }
}
