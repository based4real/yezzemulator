using System;
using Yezz.HabboHotel.Users;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class UnBanCommand : IChatCommand
    {

        public string PermissionRequired => "user_14";
        public string Parameters => "[USUARIO]";
        public string Description => "Desbanear usuario.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {

            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor, introduzca el nombre de usuario del usuario.", 34);
                return;
            }

            Habbo Habbo = YezzEnvironment.GetHabboByUsername(Params[1]);
            if (Habbo == null)
            {
                Session.SendWhisper("Se produjo un error mientras que la búsqueda de usuario en la base de datos.", 34);
                return;
            }

            if (Habbo.GetPermissions().HasRight("mod_soft_ban") && !Session.GetHabbo().GetPermissions().HasRight("mod_ban_any"))
            {
                Session.SendWhisper("Vaya, no se puede desbanear este usuario.", 34);
                return;
            }

            string Username = Habbo.Username;
            string IPAddress = "";
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT `ip_last` FROM `users` WHERE `id` = '" + Habbo.Id + "' LIMIT 1");
                IPAddress = dbClient.getString();

                dbClient.runFastQuery("DELETE FROM `bans` WHERE `value` = '" + Habbo.Username + "' or `value` =  '" + IPAddress + "' LIMIT 1");
            }

            Session.SendWhisper("Éxito, usted desbaneo a '" + Username + "'!", 34);
        }
    }
}