using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Users;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Rooms.Chat.Commands;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class ReloadUserrVIPRankCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_15"; }
        }
        public string Parameters
        {
            get { return "[USUARIO]"; }
        }
        public string Description
        {
            get { return "Dar rango VIP a una persona."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.runFastQuery("UPDATE `users` SET `rank` = '2' WHERE `id` = '" + TargetClient.GetHabbo().Id + "'");
                dbClient.runFastQuery("UPDATE `users` SET `rank_vip` = '1' WHERE `id` = '" + TargetClient.GetHabbo().Id + "'");
                TargetClient.GetHabbo().Rank = 2;
                TargetClient.GetHabbo().VIPRank = 1;
            }

            TargetClient.GetHabbo().GetClubManager().AddOrExtendSubscription("club_vip", 1 * 24 * 3600, Session);
            TargetClient.GetHabbo().GetBadgeComponent().GiveBadge("DVIP", true, Session);

            YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(Session, "ACH_VipClub", 1);
            TargetClient.SendMessage(new ScrSendUserInfoComposer(Session.GetHabbo()));

            string figure = TargetClient.GetHabbo().Look;
            YezzEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("fig/" + figure, Params[1] + " ahora es un usuario VIP!", ""));
            Session.SendWhisper("VIP dado con exito!");
        }
    }
}