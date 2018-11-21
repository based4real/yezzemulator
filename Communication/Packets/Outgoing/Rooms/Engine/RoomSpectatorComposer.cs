using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Items;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Engine
{
    class RoomSpectatorComposer : ServerPacket
    {
        public RoomSpectatorComposer()
            : base(ServerPacketHeader.RoomSpectatorComposer)
        {
        }
    }
}
