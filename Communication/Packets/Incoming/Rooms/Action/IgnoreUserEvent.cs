using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Users;
using Yezz.Communication.Packets.Outgoing.Rooms.Action;

namespace Yezz.Communication.Packets.Incoming.Rooms.Action
{
    class IgnoreUserEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (!Session.GetHabbo().InRoom)
                return;

            Room Room = Session.GetHabbo().CurrentRoom;
            if (Room == null)
                return;

            String Username = Packet.PopString();
            Habbo User = YezzEnvironment.GetHabboByUsername(Username);
            if (User == null || Session.GetHabbo().MutedUsers.Contains(User.Id) || User.GetPermissions().HasRight("mod_tool"))
                return;

            Session.GetHabbo().MutedUsers.Add(User.Id);
            Session.SendMessage(new IgnoreStatusComposer(1, Username));

            YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(Session, "ACH_SelfModIgnoreSeen", 1);
        }
    }
}
