using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.Users.Messenger;
using Yezz.HabboHotel.Cache;

namespace Yezz.Communication.Packets.Outgoing.Messenger
{
    class BuddyRequestsComposer : ServerPacket
    {
        public BuddyRequestsComposer(ICollection<MessengerRequest> Requests)
            : base(ServerPacketHeader.BuddyRequestsMessageComposer)
        {
            base.WriteInteger(Requests.Count);
            base.WriteInteger(Requests.Count);

            foreach (MessengerRequest Request in Requests)
            {
                base.WriteInteger(Request.From);
               base.WriteString(Request.Username);

                UserCache User = YezzEnvironment.GetGame().GetCacheManager().GenerateUser(Request.From);
               base.WriteString(User != null ? User.Look : "");
            }
        }
    }
}
