using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Incoming;

namespace Yezz.Communication.Packets.Incoming.Catalog
{
    public class GetRecyclerRewardsEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new RecyclerRewardsComposer());
        }
    }
}