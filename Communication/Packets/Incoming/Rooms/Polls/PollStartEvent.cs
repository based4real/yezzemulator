using Yezz.HabboHotel.Rooms.Polls;
using Yezz.Communication.Packets.Outgoing.Rooms.Polls;

namespace Yezz.Communication.Packets.Incoming.Rooms.Polls
{
    class PollStartEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient session, ClientPacket packet)
        {
            int pollId = packet.PopInt();

            RoomPoll poll = null;
            if (!YezzEnvironment.GetGame().GetPollManager().TryGetPoll(pollId, out poll))
                return;

            session.SendMessage(new PollContentsComposer(poll));
        }
    }
}
