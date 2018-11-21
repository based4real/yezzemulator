using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class OnlineCommand : IChatCommand
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
            get { return "Ver cantidad de usuarios en línea."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            int OnlineUsers = YezzEnvironment.GetGame().GetClientManager().Count;

            Session.SendWhisper("Ahora mismo hay " + OnlineUsers + " usuarios conectados en "+YezzEnvironment.HotelName+" :).", 34);
        }
    }
}

