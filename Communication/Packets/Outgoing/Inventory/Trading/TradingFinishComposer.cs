using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Yezz.Communication.Packets.Outgoing.Inventory.Trading
{
    class TradingFinishComposer : ServerPacket
    {
        public TradingFinishComposer()
            : base(ServerPacketHeader.TradingFinishMessageComposer)
        {
        }
    }
}
