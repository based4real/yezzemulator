﻿using System;

using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Handshake;

namespace Yezz.Communication.Packets.Incoming.Handshake

{
    public class SSOTicketEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.RC4Client == null || Session.GetHabbo() != null)
                return;

            string SSO = Packet.PopString();

            if (string.IsNullOrEmpty(SSO) /*|| SSO.Length < 15*/)
                return;

            Session.TryAuthenticate(SSO);
        }
    }
}
//{
//    public class SSOTicketEvent : IPacketEvent
//    {
//        public void Parse(GameClient Session, ClientPacket Packet)
//        {
//            if (Session == null || Session.RC4Client == null || Session.GetHabbo() != null)
//                return;

//            Session.TryAuthenticate(Packet.PopString());
//        }
//    }
//}