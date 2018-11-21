using System;
using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Users;
using Yezz.Communication.Packets.Outgoing.Handshake;
using Yezz.HabboHotel.Rooms;

namespace Yezz.Communication.Packets.Incoming.Users
{
    class ScrGetUserInfoEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new ScrSendUserInfoComposer(Session.GetHabbo()));
            Session.SendMessage(new UserRightsComposer(Session.GetHabbo()));

        }
    }
}
