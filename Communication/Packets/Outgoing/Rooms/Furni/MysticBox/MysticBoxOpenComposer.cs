using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.Items.Crafting;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Furni
{
    class MysticBoxOpenComposer : ServerPacket
    {
        public MysticBoxOpenComposer()
            : base(ServerPacketHeader.MysticBoxOpenComposer)
        {
        }
    }
}