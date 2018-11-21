using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using System;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Events
{
    internal class EventAlertCommand : IChatCommand
    {
        public string PermissionRequired => "user_12";
        public string Parameters => "[MENSAJE]";
        public string Description => "Enviar Alerta de Evento al Hotel!";

        public void Execute(GameClient Session, Room Room, string[] Params)
        {

            string Message = CommandManager.MergeParams(Params, 1);

            Session.GetHabbo()._eventsopened++;

            YezzEnvironment.GetGame().GetClientManager().SendEventType1(new RoomNotificationComposer("¡Nuevo evento en " + YezzEnvironment.HotelName + "!",
                             "¡<font color=\"#00adff\"><b>" + Session.GetHabbo().Username + "</b></font> está organizando un nuevo evento en este momento! Si quieres ganar  <font color=\"#235F8C\"><b>Diamantes</b></font> participa ahora mismo.\n\n" +
                             "¿Quieres participar en este juego? ¡Haz click en el botón inferior de <b> Ir a la sala del evento</b>, y ahí dentro podrás participar.\n\n" +
                             "<b>¿De qué se trata este evento?<b>\n\n <font color=\"#f11648\"><b>" + Message +
                             "</b></font> <br><br>¡Te esperamos! :)<b>",
                             "events", "¡Ir a la sala del evento!", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));

            YezzEnvironment.GetGame().GetClientManager().SendEventType2(new WhisperComposer(-1, "Hay un nuevo evento, haz <font color=\"#2E9AFE\"><a href='event:navigator/goto/" + Session.GetHabbo().CurrentRoomId + "'><b>click aquí</b></a></font> para ir al evento.", 0, 33));
            YezzEnvironment.GetGame().GetClientManager().SendEventType2(new WhisperComposer(-1, Message, 0, 33));
            YezzEnvironment.GetGame().GetClientManager().SendEventType2(new WhisperComposer(-1, "Evento organizado por: " + Session.GetHabbo().Username + ".", 0, 33));

            LogEvent(Session.GetHabbo().Id, Room.Id, Message);
        }

        public void LogEvent(int MasterID, int RoomID, string Message)
        {
            DateTime Now = DateTime.Now;
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("INSERT INTO event_logs VALUES (NULL, " + MasterID + ", " + RoomID + ", @message, UNIX_TIMESTAMP())");
                dbClient.AddParameter("message", Message);
                dbClient.RunQuery();
            }
        }


    }
}

