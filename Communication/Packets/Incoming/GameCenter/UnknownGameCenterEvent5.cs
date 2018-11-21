using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Games;
using Yezz.Communication.Packets.Outgoing.GameCenter;
using System.Data;

using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.Communication.Packets.Incoming.GameCenter
{
    class UnknownGameCenterEvent5 : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            int pop1 = Packet.PopInt();
            int pop2 = Packet.PopInt();
            int pop3 = Packet.PopInt();
            int pop4 = Packet.PopInt();
            int pop5 = Packet.PopInt();

        }
    }
}
