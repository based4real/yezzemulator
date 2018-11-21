using System.Collections.Generic;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Quests;
using Yezz.Communication.Packets.Incoming;

namespace Yezz.Communication.Packets.Incoming.Quests
{
    public class GetQuestListEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            YezzEnvironment.GetGame().GetQuestManager().GetList(Session, null);
        }
    }
}