using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Database.Interfaces;



namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class DisableGiftsCommand : IChatCommand
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
            get { return "Activar o desactivar la recepción de regalos."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            Session.GetHabbo().AllowGifts = !Session.GetHabbo().AllowGifts;
            Session.SendWhisper("Usted " + (Session.GetHabbo().AllowGifts == true ? "ahora" : "ya no") + " acepta regalos.", 34);

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("UPDATE `users` SET `allow_gifts` = @AllowGifts WHERE `id` = '" + Session.GetHabbo().Id + "'");
                dbClient.AddParameter("AllowGifts", YezzEnvironment.BoolToEnum(Session.GetHabbo().AllowGifts));
                dbClient.RunQuery();
            }
        }
    }
}