using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Incoming;
using Yezz.Communication.Packets.Outgoing.Navigator;
using Yezz.HabboHotel.Navigator;

namespace Yezz.Communication.Packets.Incoming.Navigator
{
    class GetNavigatorFlatsEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            ICollection<SearchResultList> Categories = YezzEnvironment.GetGame().GetNavigator().GetEventCategories();

            Session.SendMessage(new NavigatorFlatCatsComposer(Categories, Session.GetHabbo().Rank));
        }
    }
}