using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class PrefixNameCommand : IChatCommand
    {

        public string PermissionRequired => "user_4";
        public string Parameters => "[PREFIX]";
        public string Description => "off/red/green/blue/cyan/purple";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor, introduce por ejemplo: [ADM]", 34);
                return;
            }

            if (Params[1].ToString().ToLower() == "off")
            {
                Session.GetHabbo()._tag = "";
                Session.SendWhisper("Desactivaste tu prefijo con éxito!");
                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.runFastQuery("UPDATE `users` SET `tag` = '' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                }
            }
            else
            {
                string PrefixName = CommandManager.MergeParams(Params, 1);
                Session.GetHabbo()._tag = PrefixName;
                Session.SendWhisper("Tu prefijo para el nombre se añadio correctamente");
                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.SetQuery("UPDATE `users` SET `tag` = @prefix WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    dbClient.AddParameter("prefix", PrefixName);
                    dbClient.RunQuery();
                }
            }
            return;
        }
    }
}