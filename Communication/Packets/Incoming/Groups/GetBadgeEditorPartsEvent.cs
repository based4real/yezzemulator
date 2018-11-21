using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Groups;

namespace Yezz.Communication.Packets.Incoming.Groups
{
    class GetBadgeEditorPartsEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            Session.SendMessage(new BadgeEditorPartsComposer(
                YezzEnvironment.GetGame().GetGroupManager().Bases,
                YezzEnvironment.GetGame().GetGroupManager().Symbols,
                YezzEnvironment.GetGame().GetGroupManager().BaseColours,
                YezzEnvironment.GetGame().GetGroupManager().SymbolColours,
                YezzEnvironment.GetGame().GetGroupManager().BackGroundColours));

        }
    }
}
