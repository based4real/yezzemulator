using System;

using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.Groups;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Handshake;

namespace Yezz.Communication.Packets.Incoming.Handshake
{
    public class InfoRetrieveEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new UserObjectComposer(Session.GetHabbo()));
            Session.SendMessage(new UserPerksComposer(Session.GetHabbo()));
        }
    }
}