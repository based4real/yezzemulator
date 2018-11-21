﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Notifications
{
    class MassEventComposer : ServerPacket
    {
        public MassEventComposer(string Message)
            : base(ServerPacketHeader.MassEventComposer)

        {
            base.WriteString(Message);
        }
    }
}