//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Yezz.HabboHotel.GameClients;
//using Yezz.Communication.Packets.Outgoing.LandingView;

//namespace Yezz.Communication.Packets.Incoming.Quests
//{
//    class GetDailyQuestEvent : IPacketEvent
//    {
//        public void Parse(GameClient Session, ClientPacket Packet)
//        {
//            int UsersOnline = YezzEnvironment.GetGame().GetClientManager().Count;

//            Session.SendMessage(new ConcurrentUsersGoalProgressComposer(UsersOnline));
//        }
//    }
//}
