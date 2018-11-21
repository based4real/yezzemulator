﻿using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class ColourPrefixCommand : IChatCommand
    {

        public string PermissionRequired => "user_4";
        public string Parameters => "";
        public string Description => "off/red/green/blue/cyan/purple";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendMessage(new RoomNotificationComposer("Lista de colores:",
                     "<font color='#FF8000'><b>LISTA DE COLORES:</b>\n" +
                     "<font size=\"12\" color=\"#1C1C1C\">El comando :pcolor te permitirá fijar un color que tu desees en tu prefijo, para poder seleccionar el color deberás especificarlo después de hacer el comando, como por ejemplo:\r\r" +
                     "<font size =\"11\" color=\"#00e676\"><b>:pcolor cgreen</b> » Prefijo Verde Claro</font>\r\n" +
                     "<font size =\"11\" color=\"#00bcd4\"><b>:pcolor cyan</b> » Prefijo Azul Cielo</font>\r\n" +
                     "<font size =\"11\" color=\"#0000FF\"><b>:pcolor blue</b> » Prefijo Azul Fuerte</font>\r\n" +
                     "<font size =\"11\" color=\"#e91e63\"><b>:pcolor pink</b> » Prefijo Rosado</font>\r\n" +
                     "<font size =\"11\" color=\"#f50101\"><b>:pcolor red</b> » Prefijo Rojo</font>\r\n" +
                     "<font size =\"11\" color=\"#0000FF\"><b>:pcolor tblue</b> » Prefijo Azul Fuerte Claro :D</font>\r\n" +
                     "<font size =\"11\" color=\"#ff0000\"><b>:pcolor orange</b> » Prefijo Naranja</font>\r\n" +
                     "<font size =\"11\" color=\"#31B404\"><b>:pcolor green</b> » Prefijo Verde</font>\r\n" +
                     "<font size =\"11\" color=\"#ff9100\"><b>:pcolor torange</b> » Prefijo Naranja Claro</font>\r\n" +
                     "", "", ""));
                return;
            }
            string chatColour = Params[1];
            string Colour = chatColour.ToUpper();
            switch (chatColour)
            {
                case "none":
                case "black":
                case "off":
                    Session.GetHabbo()._tag = "";
                    Session.SendWhisper("Tu color de prefijo Ha Sido Desactivado");
                    break;
                case "rainbow":
                    Session.GetHabbo()._tag = "rainbow";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = 'rainbow' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: rainbow");
                    break;
                case "cgreen":
                    Session.GetHabbo()._tag = "#00e676";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = '#00e676' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: cgreen");
                    break;
                case "tblue":
                    Session.GetHabbo()._tag = "#0000FF";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = '#0000FF' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: tblue");
                    break;
                case "torange":
                    Session.GetHabbo()._tag = "#ff9100";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = '#ff9100' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: torange");
                    break;
                case "pink":
                    Session.GetHabbo()._tag = "#e91e63";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = '#e91e63' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: pink");
                    break;
                case "blue":
                    Session.GetHabbo()._tag = "#0000FF";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = '#0000FF' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: blue");
                    break;
                case "red":
                    Session.GetHabbo()._tag = "#f50101";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = '#f50101' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: red");
                    break;
                case "green":
                    Session.GetHabbo()._tag = "#31B404";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = '#31B404' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: green");
                    break;
                case "cyan":
                    Session.GetHabbo()._tag = "#00bcd4";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = '#00bcd4' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: cyan");
                    break;
                case "orange":
                    Session.GetHabbo()._tag = "#ff9100";
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `tagcolor` = '#ff9100' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    Session.SendWhisper("Tu Color de chat ha sido activado a: orange");
                    break;
                default:
                    Session.SendWhisper("El color del prefijo: " + Colour + " No Existe!");
                    break;
            }
            return;
        }
    }
}