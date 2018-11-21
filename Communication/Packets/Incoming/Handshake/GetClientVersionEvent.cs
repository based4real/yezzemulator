using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Incoming;

namespace Yezz.Communication.Packets.Incoming.Handshake
{
    public class GetClientVersionEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            string Build = Packet.PopString();

            if (YezzEnvironment.SWFRevision != Build)
                YezzEnvironment.SWFRevision = Build;
        }
    }
}