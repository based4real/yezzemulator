using System;
using System.Collections.Generic;

using Yezz.HabboHotel.Catalog;

namespace Yezz.Communication.Packets.Outgoing.Catalog
{
    public class RecyclerRewardsComposer : ServerPacket
    {
        public RecyclerRewardsComposer()
            : base(ServerPacketHeader.RecyclerRewardsMessageComposer)
        {
            base.WriteInteger(0);// Count of items
        }
    }
}