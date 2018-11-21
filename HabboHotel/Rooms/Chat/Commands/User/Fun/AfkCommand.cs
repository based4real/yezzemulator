using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Communication.Packets.Outgoing.Rooms.Avatar;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Items;

using Yezz.Communication.Packets.Outgoing.Rooms.Chat;

using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class AfkCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_normal"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Ponte ausente."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            User.IsAsleep = true;
            Room.SendMessage(new SleepComposer(User, true));

            Session.SendWhisper("¡Ahora estás dormido!", 34);
        }
    }
}
