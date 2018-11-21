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
using System.Globalization;

namespace Yezz.Communication.Packets.Incoming.GameCenter
{
    class GetWeeklyLeaderBoardEvent : IPacketEvent // Get2GameWeeklySmallLeaderboardComposer
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            int GameId = Packet.PopInt();

            GameData GameData = null;

            if (YezzEnvironment.GetGame().GetGameDataManager().TryGetGame(GameId, out GameData))
            {
                Session.SendMessage(new Game2WeeklySmallLeaderboardComposer(GameId)); // El pequeño antes de que pulses nada. UNICO NECESARIO AQUI.
                Session.SendMessage(new GameCenterPrizeMessageComposer(GameId));
                Session.SendMessage(new GameCenterLuckyLoosersWinnersComposer(GameId));
                //Session.SendMessage(new Game2CurrentWeekLeaderboardMessageComposer(GameData, weekNum)); // Izquierda
                //Session.SendMessage(new Game2LastWeekLeaderboardMessageComposer(GameData, weekNum)); // Derecha
                //Session.SendMessage(new Game2WeeklyLeaderboardComposer(GameId)); sin custom
            }
        }
    }
}
