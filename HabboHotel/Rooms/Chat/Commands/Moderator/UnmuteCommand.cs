using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Database.Interfaces;
using Yezz.Utilities;
using Yezz.HabboHotel.GameClients;


namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class UnmuteCommand : IChatCommand
    {
        public string PermissionRequired => "user_10";
        public string Parameters => "[USUARIO]";
        public string Description => "Desmutear usuario.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Introduce el nombre del usuario que deseas desmutear..", 34);
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null || TargetClient.GetHabbo() == null)
            {
                Session.SendWhisper("Ocurrio un error, escribe correctamente el nombre o no se encuentra online.", 34);
                return;
            }

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("UPDATE `users` SET `time_muted` = '0' WHERE `id` = '" + TargetClient.GetHabbo().Id + "' LIMIT 1");
            }

            TargetClient.GetHabbo().TimeMuted = 0;
            TargetClient.SendNotification("Usted ha sido desmuteado por " + Session.GetHabbo().Username + "!");
            Session.SendWhisper("Acabas de desmutear a  " + TargetClient.GetHabbo().Username + "!", 34);
        }
    }
}