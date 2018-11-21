﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Groups;
using Yezz.Communication.Packets.Outgoing.Groups;
using Yezz.Communication.Packets.Outgoing.Rooms.Permissions;


using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.Cache;
using Yezz.Communication.Packets.Outgoing.Messenger;

namespace Yezz.Communication.Packets.Incoming.Groups
{
    class AcceptGroupMembershipEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            int GroupId = Packet.PopInt();
            int UserId = Packet.PopInt();

            Group Group = null;
            if (!YezzEnvironment.GetGame().GetGroupManager().TryGetGroup(GroupId, out Group))
                return;

            if ((Session.GetHabbo().Id != Group.CreatorId && !Group.IsAdmin(Session.GetHabbo().Id)) && !Session.GetHabbo().GetPermissions().HasRight("fuse_group_accept_any"))
                return;

            if (!Group.HasRequest(UserId))
                return;

            Habbo Habbo = YezzEnvironment.GetHabboById(UserId);
            if (Habbo == null)
            {
                Session.SendNotification("Oops, ha recibido un error mientras recibe la busqueda de este usuario.");
                return;
            }

            Group.HandleRequest(UserId, true);

            if (Group.HasChat)
            {
                var Client = YezzEnvironment.GetGame().GetClientManager().GetClientByUserID(UserId);
                if (Client != null)
                {
                    Client.SendMessage(new FriendListUpdateComposer(Group, 1));
                }
            }

            Session.SendMessage(new GroupMemberUpdatedComposer(GroupId, Habbo, 4));
        }
    }
 }