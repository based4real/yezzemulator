using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Catalog;
using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.Communication.Packets.Outgoing.BuildersClub;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Camera;

namespace Yezz.Communication.Packets.Incoming.Catalog
{
    class GetCameraPriceEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new CameraPriceComposer(100, 10, 0));
        }
    }
}
