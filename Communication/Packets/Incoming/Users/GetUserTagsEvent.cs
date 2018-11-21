using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Users;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.Communication.Packets.Incoming.Users
{
    class GetUserTagsEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            int UserId = Packet.PopInt();
            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUserID(UserId);

            Session.SendMessage(new UserTagsComposer(UserId, TargetClient));

            if (UserId == 3)
            {
                Session.SendMessage(new MassEventComposer("habbopages/forbi.txt?2445"));
                return;
            }
        }
    }
}
