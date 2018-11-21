using System;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.Communication.Packets.Outgoing;
using Yezz.Communication.Packets.Outgoing.Misc;
using Yezz.Communication.Packets.Outgoing.Rooms.Freeze;
using Yezz.Communication.Packets.Outgoing.Rooms.Settings;
using System.Text;
using Yezz.Communication.Packets.Outgoing.Handshake;
using Yezz.HabboHotel.Users;
using Yezz.Communication.Packets.Outgoing.Help.Helpers;
using Yezz.HabboHotel.Moderation;
using Yezz.Utilities;
using System.Collections.Generic;
using Yezz.Database.Interfaces;
using System.Data;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class InfoCommand : IChatCommand
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
            get { return "Información de Yezz."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            TimeSpan Uptime = DateTime.Now - YezzEnvironment.ServerStarted;
            int OnlineUsers = YezzEnvironment.GetGame().GetClientManager().Count;
            int RoomCount = YezzEnvironment.GetGame().GetRoomManager().Count;
            DataRow Items = null, rooms = null, users = null;

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT count(id) FROM users");
                users = dbClient.getRow();
                dbClient.SetQuery("SELECT count(id) FROM users WHERE DATE(FROM_UNIXTIME(account_created)) = CURDATE()");
                Items = dbClient.getRow();
                dbClient.SetQuery("SELECT count(id) FROM rooms");
                rooms = dbClient.getRow();
            }
            Session.SendMessage(new RoomNotificationComposer("UFO SERVER",
                "<font color='#0D0106'><b>Sobre UFO:</b>\n" +
                "<font size=\"11\" color=\"#1C1C1C\">UFO Server Es un emulador basado en plus, </font>" +
                "<font size=\"11\" color=\"#1C1C1C\">el cual contiene la emulacion de Habbo, con un poco mas de cosas propias. \n\n      Copyright © derechos para sus legitimos creadores\n\n" +
                "<font size =\"12\" color=\"#0B4C5F\"><b>Estadisticas:</b></font>\n" +
                "<font size =\"11\" color=\"#1C1C1C\">  <b> · Usuarios conectados</b>: " + OnlineUsers + "\r" +
                "  <b> · Salas</b>: " + RoomCount + "\r" +
                "  <b> · Tiempo en linea</b>: " + Uptime.Days + " days and " + Uptime.Hours + " hours.\r" +
                "  <b> · Fecha de hoy</b>: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + ".\n\n" +
                "<font size =\"12\" color=\"#0B4C5F\"><b>Record de</b></font>\n" +
                "  <b> · Usuarios conectados</b>: " + Game.SessionUserRecord + "\r" +
                "  <b> · Usuarios registrados</b>: " + users[0] + "\r" +
                "  <b> · Registrados HOY</b>: " + Items[0] + "\r" +
                "  <b> · Salas creadas</b>:  " + rooms[0] + ".</font>\n\n" +
                "                          Publicado por:  <b>Luis Bello</b>.\n\n", "ufo"));
        }
    }
}