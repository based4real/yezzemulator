using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.GameClients;

namespace Yezz.Communication.Packets.Outgoing.Users
{
    class RespectNotificationComposer : ServerPacket
    {
        public RespectNotificationComposer(int userID, int Respect)
            : base(ServerPacketHeader.RespectNotificationMessageComposer)
        {
            base.WriteInteger(userID);
            base.WriteInteger(Respect);
        }
    }
}
