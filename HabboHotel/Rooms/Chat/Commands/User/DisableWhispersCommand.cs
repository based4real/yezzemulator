using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class DisableWhispersCommand : IChatCommand
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
            get { return "Activar o desactivar la capacidad de recibir susurros."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            Session.GetHabbo().ReceiveWhispers = !Session.GetHabbo().ReceiveWhispers;
            Session.SendWhisper("Usted " + (Session.GetHabbo().ReceiveWhispers ? "ahora" : "ya no") + " recibe susurros!", 34);
        }
    }
}
