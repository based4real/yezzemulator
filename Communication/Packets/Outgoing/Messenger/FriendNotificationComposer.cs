﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.Users.Messenger;

namespace Yezz.Communication.Packets.Outgoing.Messenger
{
    class FriendNotificationComposer : ServerPacket
    {
        public FriendNotificationComposer(int UserId, MessengerEventTypes type, string data)
            : base(ServerPacketHeader.FriendNotificationMessageComposer)
        {
            base.WriteString(UserId.ToString());
            base.WriteInteger(MessengerEventTypesUtility.GetEventTypePacketNum(type));
            base.WriteString(data);
        }
    }
}
