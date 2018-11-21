using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Games;
using Yezz.Communication.Packets.Outgoing.GameCenter;
using System.Data;

using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.Communication.Packets.Incoming.GameCenter
{
    class UnknownGameCenterEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            int GameId = Packet.PopInt();
            int UserId = Packet.PopInt();

            GameData GameData = null;
            if (YezzEnvironment.GetGame().GetGameDataManager().TryGetGame(GameId, out GameData))
            {
               // Session.SendMessage(new Game2WeeklyLeaderboardComposer(GameId)); Comentado y funciona
            }
        }
    }
}
