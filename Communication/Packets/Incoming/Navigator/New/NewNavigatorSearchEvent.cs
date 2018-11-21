using System.Collections.Generic;

using Yezz.HabboHotel.Navigator;
using Yezz.Communication.Packets.Outgoing.Navigator;

namespace Yezz.Communication.Packets.Incoming.Navigator
{
    class NewNavigatorSearchEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient session, ClientPacket packet)
        {
            string Category = packet.PopString();
            string Search = packet.PopString();

            ICollection<SearchResultList> Categories = new List<SearchResultList>();

            if (!string.IsNullOrEmpty(Search))
            {
                SearchResultList QueryResult = null;
                if (YezzEnvironment.GetGame().GetNavigator().TryGetSearchResultList(0, out QueryResult))
                {
                    Categories.Add(QueryResult);
                }
            }
            else
            {
                Categories = YezzEnvironment.GetGame().GetNavigator().GetCategorysForSearch(Category);
                if (Categories.Count == 0)
                {
                    //Are we going in deep?!
                    Categories = YezzEnvironment.GetGame().GetNavigator().GetResultByIdentifier(Category);
                    if (Categories.Count > 0)
                    {
                        session.SendMessage(new NavigatorSearchResultSetComposer(Category, Search, Categories, session, 2, 100));
                        return;
                    }
                }
            }

            session.SendMessage(new NavigatorSearchResultSetComposer(Category, Search, Categories, session));
        }
    }
}
