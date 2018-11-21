using Yezz.Communication.Packets.Outgoing;
using Yezz.HabboHotel.GameClients;

namespace Yezz.Communication.Packets.Incoming.Rooms.Camera
{
    public class RenderRoomMessageComposer : ServerPacket
    {
        public RenderRoomMessageComposer()
            : base(ServerPacketHeader.TakedRoomPhoto)
        {

        }
    }

    public class RenderRoomMessageComposerEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket paket)
        {
            Session.SendMessage(new RenderRoomMessageComposer());
        }
    }
}