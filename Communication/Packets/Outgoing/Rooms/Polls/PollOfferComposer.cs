﻿using Yezz.HabboHotel.Rooms.Polls;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Polls
{
    class PollOfferComposer : ServerPacket
    {
        public PollOfferComposer(RoomPoll poll)
            : base(ServerPacketHeader.PollOfferMessageComposer)
        {
            base.WriteInteger(poll.Id);
            base.WriteString(RoomPollTypeUtility.GetRoomPollType(poll.Type).ToUpper());
            base.WriteString(poll.Headline);
            base.WriteString(poll.Summary);
        }
    }
}