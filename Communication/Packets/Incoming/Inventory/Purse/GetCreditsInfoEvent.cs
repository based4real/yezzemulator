using System;
using System.Linq;
using System.Text;

using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;

namespace Yezz.Communication.Packets.Incoming.Inventory.Purse
{
    class GetCreditsInfoEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new CreditBalanceComposer(Session.GetHabbo().Credits));
            Session.SendMessage(new ActivityPointsComposer(Session.GetHabbo().Duckets, Session.GetHabbo().Diamonds, Session.GetHabbo().GOTWPoints));
        }
    }
}
