using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.Items.Crafting;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Furni
{
    class MysticBoxCloseComposer : ServerPacket
    {
        public MysticBoxCloseComposer()
            : base(ServerPacketHeader.MysticBoxCloseComposer)
        {
        }
    }
}