using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Rooms.Session;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class UnloadCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_normal"; }
        }

        public string Parameters
        {
            get { return "id"; }
        }

        public string Description
        {
            get { return "Recarga la sala."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Session.GetHabbo().GetPermissions().HasRight("room_unload_any"))
            {
                Room r = null;
                if (!YezzEnvironment.GetGame().GetRoomManager().TryGetRoom(Room.Id, out r))
                {
                    return;
                }

                List<RoomUser> UsersToReturn = Room.GetRoomUserManager().GetRoomUsers().ToList();
                YezzEnvironment.GetGame().GetRoomManager().UnloadRoom(r, true);

                foreach (RoomUser User in UsersToReturn)
                {
                    if (User != null)
                    {
                        User.GetClient().SendMessage(new RoomForwardComposer(Room.Id));
                    }
                }
            }
            else
            {
                if (Room.CheckRights(Session, true))
                {
                    List<RoomUser> UsersToReturn = Room.GetRoomUserManager().GetRoomUsers().ToList();
                    YezzEnvironment.GetGame().GetRoomManager().UnloadRoom(Room);

                    foreach (RoomUser User in UsersToReturn)
                    {
                        if (User != null)
                        {
                            User.GetClient().SendMessage(new RoomForwardComposer(Room.Id));
                        }
                    }
                }
            }
        }
    }
}
