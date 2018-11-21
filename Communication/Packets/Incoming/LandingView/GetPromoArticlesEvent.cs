using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.LandingView;
using Yezz.HabboHotel.LandingView.Promotions;
using Yezz.Communication.Packets.Outgoing.LandingView;

namespace Yezz.Communication.Packets.Incoming.LandingView
{
    class GetPromoArticlesEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            ICollection<Promotion> LandingPromotions = YezzEnvironment.GetGame().GetLandingManager().GetPromotionItems();

            Session.SendMessage(new PromoArticlesComposer(LandingPromotions));
        }
    }
}
