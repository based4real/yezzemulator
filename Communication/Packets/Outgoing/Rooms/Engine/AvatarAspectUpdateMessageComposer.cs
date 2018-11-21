using System;
using System.Linq;
using System.Text;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Engine
{
    class AvatarAspectUpdateMessageComposer : ServerPacket
    {
        public AvatarAspectUpdateMessageComposer(string Figure, string Gender)
            : base(ServerPacketHeader.AvatarAspectUpdateMessageComposer)
        {
            base.WriteString(Figure);
            base.WriteString(Gender);

        }
    }
}