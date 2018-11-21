using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.Users;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Handshake;

namespace Yezz.Communication.Packets.Incoming.Misc
{
    class GetAdsOfferEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new VideoOffersRewardsComposer());
        }
    }
}
