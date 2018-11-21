using System;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Handshake;

namespace Yezz.Communication.Packets.Incoming.Handshake
{
    public class UniqueIDEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            string Junk = Packet.PopString();
            string MachineId = Packet.PopString();

            Session.MachineId = MachineId;

            Session.SendMessage(new SetUniqueIdComposer(MachineId));
        }
    }
}