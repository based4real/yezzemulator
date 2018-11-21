using Yezz.Database.Interfaces;
using Yezz.HabboHotel.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class FilterCommand : IChatCommand
    {
        public string PermissionRequired => "user_15";
        public string Parameters => "[PALABRA]";
        public string Description => "Añadir palabras al filtro.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Introduce la palabra que quieres agregar al Filtro.", 34);
                return;
            }
            string BannedWord = Params[1];
            if (!string.IsNullOrWhiteSpace(BannedWord))
                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.SetQuery("INSERT INTO wordfilter (`word`, `addedby`, `bannable`) VALUES " +
                        "(@ban, '" + Session.GetHabbo().Username + "', '1');");
                    dbClient.AddParameter("ban", BannedWord.ToLower());
                    dbClient.RunQuery();
                    Session.SendWhisper("'" + BannedWord + "' Ha sido agregado correctamente al Filtro", 34);
                }
        }
    }
}