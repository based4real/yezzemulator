using System;
using System.Linq;
using System.Text;

using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Session;
using Yezz.Communication.Packets.Outgoing;
using Yezz.Communication.Packets.Outgoing.Nux;

namespace Yezz.Communication.Packets.Incoming.Rooms.Connection
{
    class GoToFlatEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            if (!Session.GetHabbo().InRoom)
                return;

            if (!Session.GetHabbo().EnterRoom(Session.GetHabbo().CurrentRoom))
                Session.SendMessage(new CloseConnectionComposer());
        }
    }
}
