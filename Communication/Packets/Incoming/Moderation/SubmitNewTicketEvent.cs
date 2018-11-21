using System.Collections.Generic;

using Yezz.Utilities;
using Yezz.HabboHotel.Users;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.Moderation;
using Yezz.Communication.Packets.Outgoing.Moderation;

namespace Yezz.Communication.Packets.Incoming.Moderation
{
    class SubmitNewTicketEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.GetHabbo() == null)
                return;

            if (YezzEnvironment.GetGame().GetModerationManager().UserHasTickets(Session.GetHabbo().Id))
            {
                ModerationTicket PendingTicket = YezzEnvironment.GetGame().GetModerationManager().GetTicketBySenderId(Session.GetHabbo().Id);
                if (PendingTicket != null)
                {
                    Session.SendMessage(new CallForHelpPendingCallsComposer(PendingTicket));
                    return;
                }
            }

            List<string> Chats = new List<string>();

            string Message = StringCharFilter.Escape(Packet.PopString().Trim());
            int Category = Packet.PopInt();
            int ReportedUserId = Packet.PopInt();
            int Type = Packet.PopInt();

            Habbo ReportedUser = YezzEnvironment.GetHabboById(ReportedUserId);
            if (ReportedUser == null)
            {
                return;
            }

            int Messagecount = Packet.PopInt();
            for (int i = 0; i < Messagecount; i++)
            {
                Packet.PopInt();
                Chats.Add(Packet.PopString());
            }

            ModerationTicket Ticket = new ModerationTicket(1, Type, Category, UnixTimestamp.GetNow(), 1, Session.GetHabbo(), ReportedUser, Message, Session.GetHabbo().CurrentRoom, Chats);
            if (!YezzEnvironment.GetGame().GetModerationManager().TryAddTicket(Ticket))
                return;

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("UPDATE `user_info` SET `cfhs` = `cfhs` + '1' WHERE `user_id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
            }

            YezzEnvironment.GetGame().GetClientManager().ModAlert("A new support ticket has been submitted!");
            YezzEnvironment.GetGame().GetClientManager().SendMessage(new ModeratorSupportTicketComposer(Session.GetHabbo().Id, Ticket), "mod_tool");
        }
    }
}
