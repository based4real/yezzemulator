using System;

using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.HabboHotel.Catalog;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Incoming;

namespace Yezz.Communication.Packets.Incoming.Catalog
{
    public class GetCatalogPageEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            int PageId = Packet.PopInt();
            int Something = Packet.PopInt();
            string CataMode = Packet.PopString();            

            CatalogPage Page = null;
            BCCatalogPage BCPage = null;

            if (CataMode == "NORMAL")
            {
                if (!YezzEnvironment.GetGame().GetCatalog().TryGetPage(PageId, out Page))
                    return;

                if (!Page.Enabled || !Page.Visible || Page.MinimumRank > Session.GetHabbo().Rank || (Page.MinimumVIP > Session.GetHabbo().VIPRank && Session.GetHabbo().Rank == 1))
                    return;

                Session.SendMessage(new CatalogPageComposer(Page, CataMode, Session));
            }

            if (CataMode == "BUILDERS_CLUB")
            {
                if (!YezzEnvironment.GetGame().GetCatalog().TryGetBCPage(PageId, out BCPage))
                    return;

                if (!BCPage.Enabled || !BCPage.Visible || BCPage.MinimumRank > Session.GetHabbo().Rank || (BCPage.MinimumVIP > Session.GetHabbo().VIPRank && Session.GetHabbo().Rank == 1))
                    return;

                Session.SendMessage(new BCCatalogPageComposer(BCPage, CataMode));
            }

            Session.GetHabbo().lastLayout = Page.Template;
           
        }
    }
}