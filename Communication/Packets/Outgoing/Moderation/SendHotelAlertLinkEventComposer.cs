﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Outgoing.Moderation
{
    class SendHotelAlertLinkEventComposer : ServerPacket
    {
        public SendHotelAlertLinkEventComposer(string Message, string URL = "")
            : base(ServerPacketHeader.SendHotelAlertLinkEvent)
        {
            base.WriteString(Message);
            base.WriteString(URL);
        }
    }
}
