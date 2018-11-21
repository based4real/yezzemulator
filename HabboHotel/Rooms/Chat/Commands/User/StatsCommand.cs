using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Communication.Packets.Outgoing.Notifications;
using Yezz.Database.Interfaces;
using System.Data;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class StatsCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_normal"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Revisar tus estadísticas."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            double Minutes = Session.GetHabbo().GetStats().OnlineTime / 60;
            double Hours = Minutes / 60;
            int OnlineTime = Convert.ToInt32(Hours);
            string s = OnlineTime == 1 ? "" : "s";

            DataRow UserInfo = null;

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT * FROM `users` WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                UserInfo = dbClient.getRow();
            }

            StringBuilder List = new StringBuilder("");
            List.AppendLine("Las estadísticas de su cuenta son:\n\n");

            List.AppendLine("Info Monetaria:");
            List.AppendLine("Creditos: " + Session.GetHabbo().Credits + "");
            List.AppendLine("Duckets: " + Session.GetHabbo().Duckets + "");
            List.AppendLine("Diamantes: " + Session.GetHabbo().Diamonds + "\n\n");

            List.AppendLine("Eventos:");
            List.AppendLine("Puntos en eventos: " + Convert.ToInt32(UserInfo["puntos_eventos"]) + "\n\n");

            List.AppendLine("Info personal:\n");
            List.AppendLine("Rango: " + Session.GetHabbo().Rank + "");
            List.AppendLine("Tiempo en línea: " + OnlineTime + " Horas" + s + "");
            List.AppendLine("Respetos: " + Session.GetHabbo().GetStats().Respect + "");


            Session.SendMessage(new MOTDNotificationComposer(List.ToString()));
        }
    }
}
