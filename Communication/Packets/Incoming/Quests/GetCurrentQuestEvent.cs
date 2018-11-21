using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Yezz.Communication.Packets.Incoming.Quests
{
    class GetCurrentQuestEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            YezzEnvironment.GetGame().GetQuestManager().GetCurrentQuest(Session, Packet);
        }
    }
}
