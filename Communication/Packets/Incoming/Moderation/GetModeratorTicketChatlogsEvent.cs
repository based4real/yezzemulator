using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Moderation;
using Yezz.Communication.Packets.Outgoing.Moderation;

namespace Yezz.Communication.Packets.Incoming.Moderation
{
    class GetModeratorTicketChatlogsEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.GetHabbo() == null || !Session.GetHabbo().GetPermissions().HasRight("mod_tickets"))
                return;

            int TicketId = Packet.PopInt();

            ModerationTicket Ticket = null;
            if (!YezzEnvironment.GetGame().GetModerationManager().TryGetTicket(TicketId, out Ticket) || Ticket.Room == null)
                return;

            RoomData Data = YezzEnvironment.GetGame().GetRoomManager().GenerateRoomData(Ticket.Room.Id);
            if (Data == null)
                return;

            Session.SendMessage(new ModeratorTicketChatlogComposer(Ticket, Data, Ticket.Timestamp));
        }
    }
}
