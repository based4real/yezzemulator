using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    internal class EndPollCommand : IChatCommand
    {

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
           
                    Room.endQuestion();
        }

        public string Description =>
            "Borrar una encuesta rápida.";

        public string Parameters =>
            "";

        public string PermissionRequired =>
            "user_6";
    }
}