using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Database.Interfaces;


namespace Yezz.Communication.Packets.Incoming.Users
{
    class SetMessengerInviteStatusEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            Boolean Status = Packet.PopBoolean();

            Session.GetHabbo().AllowMessengerInvites = Status;
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("UPDATE `users` SET `ignore_invites` = @MessengerInvites WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                dbClient.AddParameter("MessengerInvites", YezzEnvironment.BoolToEnum(Status));
                dbClient.RunQuery();
            }
        }
    }
}
