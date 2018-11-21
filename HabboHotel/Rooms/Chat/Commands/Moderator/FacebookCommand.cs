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

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class FacebookCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_13"; }
        }

        public string Parameters
        {
            get { return "[DE Q SE TRATA]"; }
        }

        public string Description
        {
            get { return "¡¡Nuevo concurso de fb!!"; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 2)
            {
                Session.SendWhisper("Porfavor introduce el mensaje y el link para enviarlo..");
                return;
            }

            string URL = "https://www.facebook.com/jabbozz/";

            string Message = CommandManager.MergeParams(Params, 1);
            Session.SendMessage(new RoomNotificationComposer("Hay un nuevo concurso en Facebook",
                 "   ¿De qué se trata?\n\n\n" +
                 "  <font color=\"#a62984\"><b>" + Message +
                 "  <br><br></b></font><b>El concurso de Facebook es realizado por: </b> <b><font color=\"#224CAD\">" + Session.GetHabbo().Username +
                 "  <br><br></b></font></b><b>Hora actual:</b> " + DateTime.Now + "\n\n" +
                 "Para acceder a la página de Facebook haz clic en Ir al Facebook", "habbo_talent_show_stage", "Ir al facebook >>", URL));
            return;

        }
    }
}
