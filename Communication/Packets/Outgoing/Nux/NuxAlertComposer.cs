﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Outgoing.Nux
{
    class NuxAlertComposer : ServerPacket
    {
        public NuxAlertComposer(string Message) : base(ServerPacketHeader.NuxAlertMessageComposer)
        {
            base.WriteString(Message);
        }
    }
}
