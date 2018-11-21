using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.Support;
using Yezz.Communication.Packets.Outgoing.Moderation;

namespace Yezz.Communication.Packets.Incoming.Moderation
{
    class GetModeratorUserChatlogEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.GetHabbo() == null)
                return;

            if (!Session.GetHabbo().GetPermissions().HasRight("mod_tool"))
                return;

            int UserId = Packet.PopInt();
            Habbo Habbo = YezzEnvironment.GetHabboById(UserId);

            if (Habbo == null)
            {
                Session.SendNotification("Oops, no se consigue este usuario");
                return;
            }

            try
            {
                Session.SendMessage(new ModeratorUserChatlogComposer(UserId));
            }
            catch { Session.SendNotification("Overflow :/"); }
        }
    }
}
