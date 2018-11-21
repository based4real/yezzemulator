using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Database.Interfaces;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class spamCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_6"; }
        }
        public string Parameters
        {
            get { return ""; }
        }
        public string Description
        {
            get { return "evita spam!"; }
        }
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {

            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (User == null || User.GetClient() == null)
                return;

            //Comando por Desconocido.

            else
            {
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));
                Room.SendMessage(new ChatComposer(User.VirtualId, "**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM**NO SPAM* *NO SPAM**", 0, User.LastBubble));

                Room.SendMessage(new ChatComposer(User.VirtualId, "*Hola perdone la molestia att: " + Session.GetHabbo().Username + "", 33, User.LastBubble));
            }
        }
    }
}
