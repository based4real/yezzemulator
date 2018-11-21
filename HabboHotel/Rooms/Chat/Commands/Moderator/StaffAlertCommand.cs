using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class StaffAlertCommand : IChatCommand
    {
        public string PermissionRequired => "user_4";
        public string Parameters => "[MENSAJE]";
        public string Description => "Enviar mensaje a los staff.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Escribe el mensaje que deseas enviar.", 34);
                return;
            }

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(YezzEnvironment.GetUnixTimestamp()).ToLocalTime();

            string Message = CommandManager.MergeParams(Params, 1);
            YezzEnvironment.GetGame().GetClientManager().StaffAlert(new MOTDNotificationComposer("[STAFF]\r[" + dtDateTime + "]\r\r" + Message + "\r\r - " + Session.GetHabbo().Username + " [" + Session.GetHabbo().Rank + "]"));
            return;

        }
    }
}
