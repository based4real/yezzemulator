﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Notifications
{
    class GetGuestRoomResultMessageComposer : ServerPacket
    {
        public GetGuestRoomResultMessageComposer(int roomId)
            : base(ServerPacketHeader.GetGuestRoomResultMessageComposer)
        {
            base.WriteInteger(roomId);
        }
    }
}
