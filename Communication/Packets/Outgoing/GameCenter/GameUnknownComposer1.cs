﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Yezz.Communication.Packets.Outgoing.GameCenter
{
    class GameUnknownComposer1 : ServerPacket
    {
        public GameUnknownComposer1()
            : base(ServerPacketHeader.GameUnknownComposer1)
        {
        }
    }
}
