using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Games;
using Yezz.Communication.Packets.Outgoing.GameCenter;
using System.Data;
using System.Globalization;

using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.GameClients;

namespace Yezz.Communication.Packets.Incoming.GameCenter
{
    class Game2GetWeeklyLeaderboardEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            int GameId = Packet.PopInt();
            int weekNum = new GregorianCalendar(GregorianCalendarTypes.Localized).GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int lastWeekNum = 0;

            if(weekNum == 1) { lastWeekNum = 52; } else { lastWeekNum = weekNum - 1; }

            GameData GameData = null;


            if (YezzEnvironment.GetGame().GetGameDataManager().TryGetGame(GameId, out GameData))
            {

            }
        }
    }
}
