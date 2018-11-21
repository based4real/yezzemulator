using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.Items.Crafting;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Furni
{
    class MysticBoxRewardComposer : ServerPacket
    {
        public MysticBoxRewardComposer(string type, int itemID)
            : base(ServerPacketHeader.MysticBoxRewardComposer)
        {
            base.WriteString(type);
            base.WriteInteger(itemID);
        }
    }
}