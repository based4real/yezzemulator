using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.HabboHotel.GameClients;
using Yezz.Core;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Events
{
    class PublicityAlertCommand : IChatCommand
    {
        public string PermissionRequired => "user_7";
        public string Parameters => "";
        public string Description => "Enviar una alerta de hotel para su evento!";

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (Session == null) return;
            if (Room == null) return;

            YezzEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("¡Hay una Nueva Oleada en " + YezzEnvironment.HotelName + "!",
                             "La oleada es realizada por: <font color=\"#00adff\"><b>" + Session.GetHabbo().Username + "</b></font>\n\n" +
                "¿Quieres participar en la oleada? ¡Haz click en el botón inferior de <b> Ir a la oleada</b>, y ahí dentro podrás participar.\n\n" +
                "<b>¿Que es una oleada de publicidad?</b>\n" +
                "La oleada de publicidad se basa en ir a otros hoteles a publicar " + YezzEnvironment.HotelName + " e invitar amigos y así poder crecer y ser cada vez más, únete y diviértete.\n\n\n" +
                "<b>¿Que beneficios tengo en participar en una?</b>\n" +
                "Podrás poco a poco ir subiendo de rango o nivel para mas adelante pertenecer a nuestro equipo staff <b>¿te animas?</b>\n\n",
                             "oleadas", "¡Ir a la oleada!", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));
        }
    }
}