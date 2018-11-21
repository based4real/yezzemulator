using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Notifications;
using Yezz.Core;
using Yezz.Communication.Packets.Outgoing.Moderation;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class NotificaCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_13"; }
        }

        public string Parameters
        {
            get { return "[NOTIFICACION]"; }
        }

        public string Description
        {
            get { return "Envía una notificación a todos los usuarios."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Lista de notificaciones \r");
                    stringBuilder.Append("------------------------------------------------------------------------------\r");
                    stringBuilder.Append(":notifica micro [TEXTO] / notifica con la imagen del micrófono\r  ");
                    stringBuilder.Append(":notifica custom [TEXTO] / notifica con su muñeco en la imagen\r  ");
                    stringBuilder.Append(":notifica emoji [TEXTO] [ID EMOJI] / notifica con emoji elegido\r ");
                    stringBuilder.Append(":notifica link [TEXTO] [URL]   / notifica un sitio\r              ");
                    stringBuilder.Append(":notifica sala [TEXTO] / notifica una sala\r                      ");
                    stringBuilder.Append(":notifica placa [TEXTO] [PLACA] / notifica con una placa\r        ");
                    stringBuilder.Append("------------------------------------------------------------------------------\r"); ;
                Session.SendMessage(new MOTDNotificationComposer(stringBuilder.ToString()));
                return;
            }
            string notificathiago = Params[1];
            string Colour = notificathiago.ToUpper();
            switch (notificathiago)
            {
                // Comando editaveu abaixo mais cuidado pra não faze merda
                case "lista":
                case "list":
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Lista de notificaciones \r");
                    stringBuilder.Append("------------------------------------------------------------------------------\r");
                    stringBuilder.Append(":notifica micro [TEXTO] / notifica con la imagen del micrófono\r  ");
                    stringBuilder.Append(":notifica custom [TEXTO] / notifica con su muñeco en la imagen\r  ");
                    stringBuilder.Append(":notifica emoji [TEXTO] [ID EMOJI] / notifica con emoji elegido\r ");
                    stringBuilder.Append(":notifica link [TEXTO] [URL]   / notifica un sitio\r              ");
                    stringBuilder.Append(":notifica sala [TEXTO] / notifica una sala\r                      ");
                    stringBuilder.Append(":notifica placa [TEXTO] [PLACA] / notifica con una placa\r        ");
                    stringBuilder.Append("------------------------------------------------------------------------------\r"); ;
                    Session.SendMessage(new MOTDNotificationComposer(stringBuilder.ToString()));
                    break;

                case "micro":
                case "microfono":
                    string Message = CommandManager.MergeParams(Params, 2);

                    YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("micro", Message, ""));
                    break;

                case "custom":
                    string Messagecustom = CommandManager.MergeParams(Params, 2);

                    string figure = Session.GetHabbo().Look;
                    YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("fig/" + figure, "" + Messagecustom + "", ""));
                    break;

                case "sala":
                    string Messageseguir = CommandManager.MergeParams(Params, 2);
                    string Messageseguirs = CommandManager.MergeParams(Params, 3);

                    string figureseguir = Session.GetHabbo().Look;
                    YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("fig/" + figureseguir, Messageseguir + " n/ @Click para ir!@", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));
                    break;

                case "link":
                case "url":
                    string URL = Params[4];
                    string Messagelink = CommandManager.MergeParams(Params, 2);

                    YezzEnvironment.GetGame().GetClientManager().SendMessage(new SendHotelAlertLinkEventComposer("Alerta del Equipo Administrativo:\r\n" + Messagelink + "\r\n-" + Session.GetHabbo().Username, URL));
                    break;

                case "imagen":
                case "foto":
                case "emoji":
                    string Messageimagem = CommandManager.MergeParams(Params, 2);
                    string Messageimagems = CommandManager.MergeParams(Params, 3);

                    string figureimagem = Session.GetHabbo().Look;
                    YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("<img src='/game/c_images/emoji/" + Messageimagems + ".png' height='20' width='20'><br>    >", "" + Messageimagem, ""));
                    break;

                case "placa":
                case "badge":
                    string Messageemblema = Params[2];
                    string Messageemblemas = Params[3];

                    YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("badge/" + Messageemblemas, "" + Messageemblema + "", ""));
                    break;
            }
        }
    }
}
