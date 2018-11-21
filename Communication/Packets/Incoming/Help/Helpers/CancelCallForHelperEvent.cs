using Yezz.Communication.Packets.Outgoing.Help.Helpers;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Incoming.Help.Helpers
{
    class CancelCallForHelperEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            var call = HelperToolsManager.GetCall(Session);
            HelperToolsManager.RemoveCall(call);
            Session.SendMessage(new CloseHelperSessionComposer());
            if (call.Helper != null)
            {
                call.Helper.CancelCall();
            }

            Session.SendMessage(new CloseHelperSessionComposer());
        }
    }
}
