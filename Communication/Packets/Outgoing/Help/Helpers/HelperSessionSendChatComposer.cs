﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Outgoing.Help.Helpers
{
    class HelperSessionSendChatComposer : ServerPacket
    {
        public HelperSessionSendChatComposer(int senderId, string message)
            : base(ServerPacketHeader.HelperSessionSendChatMessageComposer)
        {
            base.WriteString(message);
            base.WriteInteger(senderId);
        }
    }
}
