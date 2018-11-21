using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Database.Interfaces;


namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class DeleteGroupCommand : IChatCommand
    {
        public string PermissionRequired => "user_11";
        public string Parameters => "";
        public string Description => "Eliminar un grupo de la base de datos y la memoria caché.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            Room = Session.GetHabbo().CurrentRoom;
            if (Room == null)
                return;

            if (Room.Group == null)
            {
                Session.SendWhisper("Oops, al parecer no hay un grupo aquí.", 34);
                return;
            }

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("DELETE FROM `groups` WHERE `id` = '" + Room.Group.Id + "'");
                dbClient.RunQuery("DELETE FROM `group_memberships` WHERE `group_id` = '" + Room.Group.Id + "'");
                dbClient.RunQuery("DELETE FROM `group_requests` WHERE `group_id` = '" + Room.Group.Id + "'");
                dbClient.RunQuery("UPDATE `rooms` SET `group_id` = '0' WHERE `group_id` = '" + Room.Group.Id + "' LIMIT 1");
                dbClient.RunQuery("UPDATE `user_stats` SET `groupid` = '0' WHERE `groupid` = '" + Room.Group.Id + "' LIMIT 1");
                dbClient.RunQuery("DELETE FROM `items_groups` WHERE `group_id` = '" + Room.Group.Id + "'");
            }

            YezzEnvironment.GetGame().GetGroupManager().DeleteGroup(Room.RoomData.Group.Id);

            Room.Group = null;
            Room.RoomData.Group = null;

            YezzEnvironment.GetGame().GetRoomManager().UnloadRoom(Room, true);

            Session.SendNotification("Grupo eliminado satisfactoriamente.");
            return;
        }
    }
}
