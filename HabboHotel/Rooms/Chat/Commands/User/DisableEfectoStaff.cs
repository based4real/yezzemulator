using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Database.Interfaces;



namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class DisableEfectoStaff : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_4"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Activar o desactivar la opción de que copien tu ropa."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            Session.GetHabbo().AllowMimic = !Session.GetHabbo().AllowMimic;
            Session.SendWhisper("Usted " + (Session.GetHabbo().AllowMimic == true ? "ahora" : "ya no") + " tiene el efecto staff.", 34);

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("UPDATE `users` SET `allow_mimic` = @AllowMimic WHERE `id` = '" + Session.GetHabbo().Id + "'");
                dbClient.AddParameter("AllowMimic", YezzEnvironment.BoolToEnum(Session.GetHabbo().AllowMimic));
                dbClient.RunQuery();
            }
        }
    }
}