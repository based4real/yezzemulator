using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Incoming.Help.Helpers
{
    class CloseHelperChatSessionEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            var Element = HelperToolsManager.GetElement(Session);

            if (Element != null)
            {
                Element.End();
                if (Element.OtherElement != null)
                    Element.OtherElement.End();
            }

            if(Session.GetHabbo().OnHelperDuty)
            {
                YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(Session, "ACH_GuideTourGiver", 1);
            }
        }
    }
}
