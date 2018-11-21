using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Avatar;

namespace Yezz.Communication.Packets.Incoming.Avatar
{
    class GetWardrobeEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new WardrobeComposer(Session));
        }
    }
}
