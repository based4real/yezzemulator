﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Notifications
{
    class WiredSmartAlertComposer : ServerPacket
    {
        public WiredSmartAlertComposer(string Message)
            : base(ServerPacketHeader.WiredSmartAlertComposer)

        {
            base.WriteString(Message);
        }
    }
}