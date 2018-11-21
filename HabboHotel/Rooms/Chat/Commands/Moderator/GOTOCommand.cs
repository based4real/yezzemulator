using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class GOTOCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_vip"; }
        }

        public string Parameters
        {
            get { return "[ROOMID]"; }
        }

        public string Description
        {
            get { return ""; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Usted debe especificar el ID de la sala", 34);
                return;
            }

            int RoomID;

            if (!int.TryParse(Params[1], out RoomID))
                Session.SendWhisper("Usted debe escribir correctamente el ID de la sala", 34);
            else
            {
                Room _room = YezzEnvironment.GetGame().GetRoomManager().LoadRoom(RoomID);
                if (_room == null)
                    Session.SendWhisper("Esta sala no existe!", 34);
                else
                {
                    Session.GetHabbo().PrepareRoom(_room.Id, "");
                }
            }
        }
    }
}