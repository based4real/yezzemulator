using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class DisableDiagonalCommand : IChatCommand
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
            get { return "Desactivar la opción de andar en diagonal en tu sala."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (!Room.CheckRights(Session, true))
            {
                Session.SendWhisper("Oops, solo el dueño de la sala puede ejecutar el comando!", 34);
                return;
            }

            Room.GetGameMap().DiagonalEnabled = !Room.GetGameMap().DiagonalEnabled;
            Session.SendWhisper("Nadie puede caminar en diagonal en la sala", 34);
        }
    }
}
