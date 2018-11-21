using System;
using System.Linq;
using Yezz.Database.Interfaces;
using System.Data;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class ShutdownCommand : IChatCommand
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
            get { return "¡Cierra el hotel!"; }
        }
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {

            YezzEnvironment.GetGame().GetClientManager().SendMessage(new RoomCustomizedAlertComposer(YezzEnvironment.HotelName + " será cerrado en pocos segundos.\n\n - " + Session.GetHabbo().Username + ""));
            Session.SendWhisper("Se cerrara el hotel en un minuto!", 34);
            YezzEnvironment.PerformShutDown();
        }
    }
}