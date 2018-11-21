using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Help;

namespace Yezz.Communication.Packets.Incoming.Help
{
    class GetSanctionStatusEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new SanctionStatusComposer());
        }
    }
}
