using System.Collections.Generic;

using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.Groups;
using Yezz.HabboHotel.GameClients;

using Yezz.Database.Interfaces;
using Yezz.Communication.Packets.Outgoing.Users;

namespace Yezz.Communication.Packets.Incoming.Groups.Forums
{
    class GetForumUserProfileEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            string username = Packet.PopString();

            Habbo targetData = YezzEnvironment.GetHabboByUsername(username);
            if (targetData == null)
            {
                Session.SendNotification("Ha ocurrido un error buscando el perfil del usuario.");
                return;
            }

            List<Group> groups = YezzEnvironment.GetGame().GetGroupManager().GetGroupsForUser(targetData.Id);

            int friendCount = 0;
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT COUNT(0) FROM `messenger_friendships` WHERE (`user_one_id` = @userid OR `user_two_id` = @userid)");
                dbClient.AddParameter("userid", targetData.Id);
                friendCount = dbClient.getInteger();
            }

            Session.SendMessage(new ProfileInformationComposer(targetData, Session, groups, friendCount));
        }
    }
}
