using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Outgoing.Inventory.Purse
{
    class CreditBalanceComposer : ServerPacket
    {
        public CreditBalanceComposer(int creditsBalance)
            : base(ServerPacketHeader.CreditBalanceMessageComposer)
        {
           base.WriteString(creditsBalance + ".0");
        }
    }
}
