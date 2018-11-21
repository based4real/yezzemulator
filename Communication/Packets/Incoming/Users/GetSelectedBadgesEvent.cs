using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Users;
using Yezz.Communication.Packets.Outgoing.Users;

namespace Yezz.Communication.Packets.Incoming.Users
{
    class GetSelectedBadgesEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            int UserId = Packet.PopInt();
            Habbo Habbo = YezzEnvironment.GetHabboById(UserId);
            if (Habbo == null)
                return;

            Session.GetHabbo().lastUserId = UserId;
            Session.SendMessage(new HabboUserBadgesComposer(Habbo));
        }
    }
}