using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.GameClients;

namespace Yezz.Communication.Packets
{
    public interface IPacketEvent
    {
        void Parse(GameClient Session, ClientPacket Packet);
    }
}