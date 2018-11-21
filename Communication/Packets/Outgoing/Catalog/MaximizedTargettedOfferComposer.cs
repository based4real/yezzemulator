using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Outgoing.Handshake
{
    class MaximizedTargettedOfferComposer : ServerPacket
    {
        public MaximizedTargettedOfferComposer()
            : base(ServerPacketHeader.MaximizedTargettedOfferComposer)
        {
            base.WriteInteger(1);
            base.WriteInteger(1);
        }
    }
}