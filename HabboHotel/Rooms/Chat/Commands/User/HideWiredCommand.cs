using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.HabboHotel.Global;
using System.Globalization;
using Yezz.Database.Interfaces;
using Yezz.Communication.Packets.Outgoing;
using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Rooms.Engine;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class HideWiredCommand : IChatCommand
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
            get { return "Esconde los furnis Wired de tu sala."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {

            if (!Room.CheckRights(Session, false, false))
            {
                Session.SendWhisper("No tienes permisos en esta sala.", 34);
                return;
            }

            Room.HideWired = !Room.HideWired;
            if (Room.HideWired)
                Session.SendWhisper("Has escondido todos los Wired de la sala.", 34);
            else
                Session.SendWhisper("Has mostrado todos los Wired de la sala.", 34);

            //using (IQueryAdapter con = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            //{
            //    con.SetQuery("UPDATE `rooms` SET `hide_wired` = @enum WHERE `id` = @id LIMIT 1");
            //    con.AddParameter("enum", YezzEnvironment.BoolToEnum(Room.HideWired));
            //    con.AddParameter("id", Room.Id);
            //    con.RunQuery();
            //}

            List<ServerPacket> list = new List<ServerPacket>();

            list = Room.HideWiredMessages(Room.HideWired);

            Room.SendMessage(list);


        }
    }
}
