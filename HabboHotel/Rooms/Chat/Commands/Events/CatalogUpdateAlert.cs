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
    internal class CatalogUpdateAlert : IChatCommand
    {
        public string PermissionRequired
        {
            get
            {
                return "command_addpredesigned";
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
                return "Avisar de una actualización en el catálogo del hotel.";
            }
        }
        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            string Message = CommandManager.MergeParams(Params, 1);
            YezzEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("¡Actualización en el catálogo!",
              "¡El catálogo de <font color=\"#2E9AFE\"><b>Havvo</b></font> acaba de ser actualizado! Si quieres observar <b>las novedades</b> sólo debes hacer click en el botón de abajo.<br>", "cata", "Ir a la página", "event:catalog/open/" + Message));

            Session.SendWhisper("Catalogo actualizado satisfactoriamente.");
        }
    }
}

