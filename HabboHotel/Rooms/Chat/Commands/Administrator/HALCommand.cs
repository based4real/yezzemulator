using Yezz.Communication.Packets.Outgoing;
using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class HALCommand : IChatCommand
    {
        public string PermissionRequired => "user_13";
        public string Parameters => "[URL] [MENSAJE]";
        public string Description => "Mandar mensaje al hotel con link.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 2)
            {
                Session.SendWhisper("Por favor escribe el mensaje y el Link a enviar.");
                return;
            }

            string URL = Params[1];
            string Message = CommandManager.MergeParams(Params, 2);

            YezzEnvironment.GetGame().GetClientManager().SendMessage(new SendHotelAlertLinkEventComposer("Alerta del Equipo Administrativo:\r\n" + Message + "\r\n-" + Session.GetHabbo().Username, URL));
            return;
        }
    }
}
