﻿using System;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Camera
{
    public class TakedRoomPhoto : ServerPacket
    {
        public TakedRoomPhoto()
            : base(ServerPacketHeader.TakedRoomPhoto)
        {
            base.WriteInteger(15);
            base.WriteInteger(0);
            base.WriteInteger(0);
        }
    }
}