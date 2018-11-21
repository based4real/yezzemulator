using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class TrollAlert : IChatCommand
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

        public void Execute(GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Escribe el mensaje que deseas enviar.", 34);
                return;
            }

            string Message = CommandManager.MergeParams(Params, 1);
            string figure = Session.GetHabbo().Look;
            YezzEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("fig/" + figure, Message + "\n\n- " + Session.GetHabbo().Username + "", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId + ""));
            return;


        }
    }
}
