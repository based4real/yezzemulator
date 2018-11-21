using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Database.Interfaces;
using Yezz.Utilities;
using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.GameClients;


using Yezz.HabboHotel.Moderation;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class MIPCommand : IChatCommand
    {
        public string PermissionRequired => "user_11";
        public string Parameters => "[USUARIO]";
        public string Description => "Machine ban, Banear IP y la cuenta de otro usuario.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {

            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor introduce el Nombre o la IP del usuario a Banear.", 34);
                return;
            }

            Habbo Habbo = YezzEnvironment.GetHabboByUsername(Params[1]);
            if (Habbo == null)
            {
                Session.SendWhisper("Ocurrio un error en la busqueda por la base de datos.", 34);
                return;
            }

            if (Habbo.GetPermissions().HasRight("mod_tool") && !Session.GetHabbo().GetPermissions().HasRight("mod_ban_any"))
            {
                Session.SendWhisper("Oops, you cannot ban that user.", 34);
                return;
            }

            String IPAddress = String.Empty;
            Double Expire = YezzEnvironment.GetUnixTimestamp() + 78892200;
            string Username = Habbo.Username;

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("UPDATE `user_info` SET `bans` = `bans` + '1' WHERE `user_id` = '" + Habbo.Id + "' LIMIT 1");

                dbClient.SetQuery("SELECT `ip_last` FROM `users` WHERE `id` = '" + Habbo.Id + "' LIMIT 1");
                IPAddress = dbClient.getString();
            }

            string Reason = null;
            if (Params.Length >= 3)
                Reason = CommandManager.MergeParams(Params, 2);
            else
                Reason = "No se especifico la razon";

            if (!string.IsNullOrEmpty(IPAddress))
                YezzEnvironment.GetGame().GetModerationManager().BanUser(Session.GetHabbo().Username, ModerationBanType.IP, IPAddress, Reason, Expire);
            YezzEnvironment.GetGame().GetModerationManager().BanUser(Session.GetHabbo().Username, ModerationBanType.USERNAME, Habbo.Username, Reason, Expire);

            if (!string.IsNullOrEmpty(Habbo.MachineId))
                YezzEnvironment.GetGame().GetModerationManager().BanUser(Session.GetHabbo().Username, ModerationBanType.MACHINE, Habbo.MachineId, Reason, Expire);

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Username);
            if (TargetClient != null)
                TargetClient.Disconnect();
            Session.SendWhisper("Se ha baneado exitosamente al usuario '" + Username + "' por la siguiente razon: '" + Reason + "'!", 34);
        }
    }
}