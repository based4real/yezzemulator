using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Items;


using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class PickAllCommand : IChatCommand
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
            get { return "Recoger todos tus objetos que tengas en la sala."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (!Room.CheckRights(Session, true))
                return;

            Room.GetRoomItemHandler().RemoveItems(Session);
            Room.GetGameMap().GenerateMaps();

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("UPDATE `items` SET `room_id` = '0' WHERE `room_id` = @RoomId AND `user_id` = @UserId");
                dbClient.AddParameter("RoomId", Room.Id);
                dbClient.AddParameter("UserId", Session.GetHabbo().Id);
                dbClient.RunQuery();
            }

            List<Item> Items = Room.GetRoomItemHandler().GetWallAndFloor.ToList();
            if (Items.Count > 0)
                Session.SendWhisper("Todavía hay más items en esta sala?, elimina manualmente utilizando :ejectall para expulsarlos!", 34);

            Session.SendMessage(new FurniListUpdateComposer());
        }
    }
}