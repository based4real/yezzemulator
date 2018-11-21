using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Utilities;
using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.GameClients;


using Yezz.HabboHotel.Moderation;

using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class BanCommand : IChatCommand
    {

        public string PermissionRequired => "user_11";
        public string Parameters => "[USUARIO] [TIEMPO] [RAZÓN]";
        public string Description => "Banear usuario.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor introduzca el nombre del usuario.", 34);
                return;
            }

            Habbo Habbo = YezzEnvironment.GetHabboByUsername(Params[1]);
            if (Habbo == null)
            {
                Session.SendWhisper("El usuario " + Params[1] + " no existe.", 34);
                return;
            }

            if (Habbo.GetPermissions().HasRight("mod_soft_ban") && !Session.GetHabbo().GetPermissions().HasRight("mod_ban_any"))
            {
                Session.SendWhisper("Vaya... al parecer no puedes banear a " + Params[1] + ".", 34);
                return;
            }

            Double Expire = 0;
            string Hours = Params[2];
            if (String.IsNullOrEmpty(Hours) || Hours == "perm")
                Expire = YezzEnvironment.GetUnixTimestamp() + 78892200;
            else
                Expire = (YezzEnvironment.GetUnixTimestamp() + (Convert.ToDouble(Hours) * 3600));

            string Reason = null;
            if (Params.Length >= 4)
                Reason = CommandManager.MergeParams(Params, 3);
            else
                Reason = "Sin razón.";

            string Username = Habbo.Username;
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("UPDATE `user_info` SET `bans` = `bans` + '1' WHERE `user_id` = '" + Habbo.Id + "' LIMIT 1");
            }

            YezzEnvironment.GetGame().GetModerationManager().BanUser(Session.GetHabbo().Username, ModerationBanType.USERNAME, Habbo.Username, Reason, Expire);

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Username);
            if (TargetClient != null)
                TargetClient.Disconnect();

            Session.SendWhisper("Excelente, ha sido baneado el usuario '" + Username + "' por " + Hours + " hora(s) con la razon '" + Reason + "'!", 34);
        }
    }
}