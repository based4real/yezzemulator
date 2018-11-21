using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Catalog;
using Yezz.Communication.Packets.Outgoing.Catalog;

namespace Yezz.Communication.Packets.Incoming.Catalog
{
    class GetCatalogOfferEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            int OfferId = Packet.PopInt();
            if (!YezzEnvironment.GetGame().GetCatalog().ItemOffers.ContainsKey(OfferId))
                return;

            int PageId = YezzEnvironment.GetGame().GetCatalog().ItemOffers[OfferId];

            CatalogPage Page;
            if (!YezzEnvironment.GetGame().GetCatalog().TryGetPage(PageId, out Page))
                return;

            if (!Page.Enabled || !Page.Visible || Page.MinimumRank > Session.GetHabbo().Rank || (Page.MinimumVIP > Session.GetHabbo().VIPRank && Session.GetHabbo().Rank == 1))
                return;

            CatalogItem Item = null;
            if (!Page.ItemOffers.ContainsKey(OfferId))
                return;

            Item = (CatalogItem)Page.ItemOffers[OfferId];
            if (Item != null)
                Session.SendMessage(new CatalogOfferComposer(Item));
        }
    }
}
