using Yezz.Communication.Packets.Outgoing;
using Yezz.Communication.Packets.Outgoing.Groups;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Groups.Forums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Incoming.Groups
{
    class GetForumStatsEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            var GroupForumId = Packet.PopInt();

            GroupForum Forum;
            if (!YezzEnvironment.GetGame().GetGroupForumManager().TryGetForum(GroupForumId, out Forum))
            {
                Session.SendNotification("Opss, Forum inexistente!");
                return;
            }

            Session.SendMessage(new GetGroupForumsMessageEvent(Forum, Session));

        }
    }
}
