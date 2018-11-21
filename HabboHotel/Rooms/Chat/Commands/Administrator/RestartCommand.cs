using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Rooms.Chat.Styles;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class RestartCommand : IChatCommand
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
            get { return "Reinisialo wn"; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            YezzEnvironment.GetGame().GetClientManager().SendMessage(new RoomCustomizedAlertComposer(YezzEnvironment.HotelName+" dará un pequeño reinicio, para aplicar todos los cambios dentro del Hotel.\n\nVolveremos enseguida:)\n\n - " + Session.GetHabbo().Username + ""));

            YezzEnvironment.PerformRestart();
        }
    }
}