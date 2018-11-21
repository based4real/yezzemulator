﻿using System;
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
    class GetGameCenterLeaderboardsEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            int GameId = Packet.PopInt();
            int UserId = Packet.PopInt();
            int weekNum = new GregorianCalendar(GregorianCalendarTypes.Localized).GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int lastWeekNum = 0;

            if (weekNum == 1) { lastWeekNum = 52; } else { lastWeekNum = weekNum - 1; }

            GameData GameData = null;

            if (YezzEnvironment.GetGame().GetGameDataManager().TryGetGame(GameId, out GameData))
            {               
                Session.SendMessage(new Game2LastWeekLeaderboardMessageComposer(GameId, lastWeekNum)); // Derecha Whats funcionando
                Session.SendMessage(new Game2WeeklyLeaderboardComposer(GameId, weekNum)); // Funcionando Whats Izquierda
            }
        }
    }
}
