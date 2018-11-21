using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.Support;
using Yezz.HabboHotel.GameClients;


namespace Yezz.Communication.Packets.Incoming.Moderation
{
    class ModerationCautionEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.GetHabbo() == null || !Session.GetHabbo().GetPermissions().HasRight("mod_caution"))
                return;

            int UserId = Packet.PopInt();
            String Message = Packet.PopString();

            GameClient Client = YezzEnvironment.GetGame().GetClientManager().GetClientByUserID(UserId);
            if (Client == null || Client.GetHabbo() == null)
                return;

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("UPDATE `user_info` SET `cautions` = `cautions` + '1' WHERE `user_id` = '" + Client.GetHabbo().Id + "' LIMIT 1");
            }

            Client.SendNotification(Message);
        }
    }
}