using System;
using Yezz.Communication.Packets.Incoming;

using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.Communication.Packets.Outgoing.BuildersClub;

namespace Yezz.Communication.Packets.Incoming.Catalog
{
    public class GetCatalogIndexEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {

            Session.SendMessage(new CatalogIndexComposer(Session, YezzEnvironment.GetGame().GetCatalog().GetPages(), "NORMAL"));
            Session.SendMessage(new CatalogIndexComposer(Session, YezzEnvironment.GetGame().GetCatalog().GetBCPages(), "BUILDERS_CLUB"));

            Session.SendMessage(new CatalogItemDiscountComposer());
            Session.SendMessage(new BCBorrowedItemsComposer());
        }
    }
}