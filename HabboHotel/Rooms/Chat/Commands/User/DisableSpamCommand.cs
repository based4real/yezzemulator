using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class DisableSpamCommand : IChatCommand
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
            get { return "Activar o desactivar la capacidad de recibir spam."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            Session.GetHabbo().AllowMessengerInvites = false;
            Session.SendWhisper("Usted " + (Session.GetHabbo().AllowMessengerInvites ? "ahora" : "ya no") + " recibe Spams de consola!", 34);
        }
    }
}
