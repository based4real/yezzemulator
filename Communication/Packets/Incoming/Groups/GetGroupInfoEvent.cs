using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.Groups;
using Yezz.Communication.Packets.Outgoing.Groups;

namespace Yezz.Communication.Packets.Incoming.Groups
{
    class GetGroupInfoEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            int GroupId = Packet.PopInt();
            bool NewWindow = Packet.PopBoolean();

            Group Group = null;
            if (!YezzEnvironment.GetGame().GetGroupManager().TryGetGroup(GroupId, out Group))
                return;

            Session.SendMessage(new GroupInfoComposer(Group, Session, NewWindow));     
        }
    }
}
