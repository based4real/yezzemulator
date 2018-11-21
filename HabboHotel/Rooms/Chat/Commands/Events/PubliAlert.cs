using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Users;
using Yezz.Communication.Packets.Outgoing.Notifications;


using Yezz.Communication.Packets.Outgoing.Handshake;
using Yezz.Communication.Packets.Outgoing.Quests;
using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.HabboHotel.Quests;
using Yezz.HabboHotel.Rooms;
using System.Threading;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Avatar;
using Yezz.Communication.Packets.Outgoing.Pets;
using Yezz.Communication.Packets.Outgoing.Messenger;
using Yezz.HabboHotel.Users.Messenger;
using Yezz.Communication.Packets.Outgoing.Rooms.Polls;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Availability;
using Yezz.Communication.Packets.Outgoing;


namespace Yezz.HabboHotel.Rooms.Chat.Commands.Events
{
    internal class PubliAlert : IChatCommand
    {
        public string PermissionRequired
        {
            get
            {
                return "command_publi_alert";
            }
        }
        public string Parameters
        {
            get { return "%message%"; }
        }
        public string Description
        {
            get
            {
                return "Manda un Evento a todo el Hotel!";
            }
        }
        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            string Message = CommandManager.MergeParams(Params, 1);
            YezzEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("Se ha abierto oleada de publicidad..",
                 "¡Hay una nueva oleada de publicidad en activo! Si quieres ganar <b>distintas recompensas</b> por participar acude a la sala de publicidad.<br><br>¿Quién ha abierto la oleada? <b> <font color=\"#58ACFA\">  "
                 + Session.GetHabbo().Username + "</font></b><br>Si quieres participar haz click en el botón inferior de <b>Ir a la sala del evento</b>, y ahí dentro podrás participar.<br><br>¿De qué trata este evento?<br><br><font color='#084B8A'><b>Trata de seguir las instrucciones de los guías de la oleada para participar y así ganar tu premio!</b></font><br><br>¡Te esperamos!", "zpam", "Ir a la sala de la oleada", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));

        }
    }
}

