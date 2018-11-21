using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.Users.Messenger;
using Yezz.HabboHotel.Users.Relationships;

namespace Yezz.Communication.Packets.Outgoing.Messenger
{
    class MessengerInitComposer : ServerPacket
    {
        public MessengerInitComposer()
            : base(ServerPacketHeader.MessengerInitMessageComposer)
        {
            base.WriteInteger(YezzStaticGameSettings.MessengerFriendLimit);//Friends max.
            base.WriteInteger(300);
            base.WriteInteger(800);
            base.WriteInteger(1); // category count
            base.WriteInteger(1);//category id
            base.WriteString("Grupos");//category name
        }
    }
}
