using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Database.Interfaces;


namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class DisableForcedFXCommand : IChatCommand
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
            get { return "Posibilidad de ignorar o permitir efectos forzadas."; }
        }

        public void Execute(GameClients.GameClient Session, Room Room, string[] Params)
        {
            Session.GetHabbo().DisableForcedEffects = !Session.GetHabbo().DisableForcedEffects;
            Session.SendWhisper("Modo FX Forzado está " + (Session.GetHabbo().DisableForcedEffects == true ? "desactivado!" : "activado!"), 34);

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("UPDATE `users` SET `disable_forced_effects` = @DisableForcedEffects WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                dbClient.AddParameter("DisableForcedEffects", YezzEnvironment.BoolToEnum(Session.GetHabbo().DisableForcedEffects));
                dbClient.RunQuery();
            }

        }
    }
}
