﻿using Yezz.HabboHotel.Moderation;
using Yezz.Utilities;

namespace Yezz.Communication.Packets.Outgoing.Moderation
{
    class CallForHelpPendingCallsComposer : ServerPacket
    {
        public CallForHelpPendingCallsComposer(ModerationTicket ticket)
            : base(ServerPacketHeader.CallForHelpPendingCallsMessageComposer)
        {
            base.WriteInteger(1);// Count for whatever reason?
            {
                base.WriteString(ticket.Id.ToString());
                base.WriteString(UnixTimestamp.FromUnixTimestamp(ticket.Timestamp).ToShortTimeString());
                base.WriteString(ticket.Issue);
            }
        }
    }
}
