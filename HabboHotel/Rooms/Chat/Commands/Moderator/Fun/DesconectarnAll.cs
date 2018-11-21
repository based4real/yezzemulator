using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Session;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class DesconectarnAll : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_16"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Reinicia (deconecta a todos)."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {

            foreach (GameClient Client in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList())
            {
                if (Client == null || Client.GetHabbo() == null || Client.GetHabbo().Username == Session.GetHabbo().Username)
                    continue;


                if (!Client.GetHabbo().InRoom)
                    Client.GetConnection().Dispose();
                else if (Client.GetHabbo().InRoom)
                    Client.GetConnection().Dispose();
                Client.SendNotification("El hotel dará un pequeño reinicio, para aplicar todos los cambios dentro del Hotel. \n\nVolveremos enseguida:)\n\n\n- " + Session.GetHabbo().Username + "");
            }



        }
    }
}
