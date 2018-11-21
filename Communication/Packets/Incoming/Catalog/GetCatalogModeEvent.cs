﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Catalog;
using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.Communication.Packets.Outgoing.BuildersClub;

namespace Yezz.Communication.Packets.Incoming.Catalog
{
    class GetCatalogModeEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            string PageMode = Packet.PopString();

            if (PageMode == "NORMAL")
                Session.SendMessage(new CatalogIndexComposer(Session, YezzEnvironment.GetGame().GetCatalog().GetPages(), PageMode));//, Sub));
            else if (PageMode == "BUILDERS_CLUB")
                Session.SendMessage(new CatalogIndexComposer(Session, YezzEnvironment.GetGame().GetCatalog().GetBCPages(), PageMode));
        }
    }
}
