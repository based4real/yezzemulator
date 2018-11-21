using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Rooms.Chat.Styles;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class BubbleCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_vip"; }
        }

        public string Parameters
        {
            get { return "[ID]"; }
        }

        public string Description
        {
            get { return "Use una burbuja de conversacion con un ID"; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (User == null)
                return;

            if (Params.Length == 1)
            {
                Session.SendWhisper("Oops, usted no ha introducido el ID", 34);
                return;
            }

            int Bubble = 0;
            if (!int.TryParse(Params[1].ToString(), out Bubble))
            {
                Session.SendWhisper("Por favor introduce un numero valido.", 34);
                return;
            }

            ChatStyle Style = null;
            if (!YezzEnvironment.GetGame().GetChatManager().GetChatStyles().TryGetStyle(Bubble, out Style) || (Style.RequiredRight.Length > 0 && !Session.GetHabbo().GetPermissions().HasRight(Style.RequiredRight)))
            {
                Session.SendWhisper("Oops, No puede utilizar esta burbuja por los permisos de rangos [ Raros: 32, 28]!", 34);
                return;
            }

            User.LastBubble = Bubble;
            Session.GetHabbo().CustomBubbleId = Bubble;
            Session.SendWhisper("Burbuja se establecida a: " + Bubble);
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.runFastQuery("UPDATE `users` SET `bubble_id` = '" + Bubble + "' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
            }
        }
    }
}