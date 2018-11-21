using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Incoming;
using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.Navigator;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Navigator;

namespace Yezz.Communication.Packets.Incoming.Navigator
{
    public class GetUserFlatCatsEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            if (Session == null)
                return;

            ICollection<SearchResultList> Categories = YezzEnvironment.GetGame().GetNavigator().GetFlatCategories();

            Session.SendMessage(new UserFlatCatsComposer(Categories, Session.GetHabbo().Rank));
        }
    }
}