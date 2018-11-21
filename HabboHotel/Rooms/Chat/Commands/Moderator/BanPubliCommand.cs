using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Moderation;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class BanPubliCommand : IChatCommand
    {

        public string PermissionRequired => "user_4";
        public string Parameters => "[USUARIO]";
        public string Description => "Banear a publicista.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor, introduzca el nombre de usuario del usuario que desea Ban IP y cuenta de la prohibición.", 34);
                return;
            }

            Habbo Habbo = YezzEnvironment.GetHabboByUsername(Params[1]);
            if (Habbo == null)
            {
                Session.SendWhisper("Se produjo un error mientras que la búsqueda de usuario en la base de datos.", 34); //BPU PROGRAMADO POR JOSEMY.
                return;
            }

            if (Habbo.GetPermissions().HasRight("mod_soft_ban") && !Session.GetHabbo().GetPermissions().HasRight("mod_ban_any"))
            {
                Session.SendWhisper("Oops, no puedes banear este usuario.", 34);
                return;
            }
            int time = 1576108800;
            string Reason = "No puedes publicar otros hoteles en " + YezzEnvironment.HotelName;
            string Username = Habbo.Username;
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.runFastQuery("UPDATE `user_info` SET `bans` = `bans` + '1' WHERE `user_id` = '" + Habbo.Id + "' LIMIT 1");
            }

            YezzEnvironment.GetGame().GetModerationManager().BanUser(Session.GetHabbo().Username, ModerationBanType.USERNAME, Habbo.Username, Reason, time);

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Username);
            if (TargetClient != null)
                TargetClient.Disconnect();

            Session.SendWhisper("Has baneado a '" + Username + "'  por publicista", 34);
        }
    }
}