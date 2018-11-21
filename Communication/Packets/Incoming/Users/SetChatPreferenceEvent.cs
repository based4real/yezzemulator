using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Sound;
using Yezz.Database.Interfaces;


namespace Yezz.Communication.Packets.Incoming.Users
{
    class SetChatPreferenceEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Boolean ChatPreference = Packet.PopBoolean();

            Session.GetHabbo().ChatPreference = ChatPreference;
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("UPDATE `users` SET `chat_preference` = @chatPreference WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                dbClient.AddParameter("chatPreference", YezzEnvironment.BoolToEnum(ChatPreference));
                dbClient.RunQuery();
            }
        }
    }
}
