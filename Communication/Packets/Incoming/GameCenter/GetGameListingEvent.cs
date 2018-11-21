using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Games;
using Yezz.Communication.Packets.Outgoing.GameCenter;

namespace Yezz.Communication.Packets.Incoming.GameCenter
{
    class GetGameListingEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            ICollection<GameData> Games = YezzEnvironment.GetGame().GetGameDataManager().GameData;

            Session.SendMessage(new GameListComposer(Games));
        }
    }
}
