using System.Collections.Generic;
using Yezz.Communication.Packets.Outgoing.Rooms.Avatar;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Items;

using Yezz.Communication.Packets.Outgoing.Rooms.Chat;

using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class BackCommand : IChatCommand
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
            get { return "Despierta."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            User.UnIdle();
            Room.SendMessage(new ChatComposer(User.VirtualId, "*¡He despertado!*", 0, User.LastBubble));
        }
    }
}
