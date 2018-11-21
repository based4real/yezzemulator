using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Engine;

namespace Yezz.Communication.Packets.Incoming.Rooms.Engine
{
    class GetFurnitureAliasesEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new FurnitureAliasesComposer());
        }
    }
}
