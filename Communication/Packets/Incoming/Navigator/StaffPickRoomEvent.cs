using Yezz.HabboHotel.Navigator;
using Yezz.HabboHotel.GameClients;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Navigator;
using Yezz.Communication.Packets.Outgoing.Rooms.Settings;

namespace Yezz.Communication.Packets.Incoming.Navigator
{
    class StaffPickRoomEvent : IPacketEvent
    {
        public void Parse(GameClient session, ClientPacket packet)
        {
            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(session.GetHabbo().CurrentRoom.OwnerName);
        
            if (!session.GetHabbo().GetPermissions().HasRight("room.staff_picks.management"))
                return;

            Room room = null;
            if (!YezzEnvironment.GetGame().GetRoomManager().TryGetRoom(packet.PopInt(), out room))
                return;

            StaffPick staffPick = null;
            if (!YezzEnvironment.GetGame().GetNavigator().TryGetStaffPickedRoom(room.Id, out staffPick))
            {
                if (YezzEnvironment.GetGame().GetNavigator().TryAddStaffPickedRoom(room.Id))
                {
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.SetQuery("INSERT INTO `navigator_staff_picks` (`room_id`,`image`) VALUES (@roomId, null)");
                        dbClient.AddParameter("roomId", room.Id);
                        dbClient.RunQuery();
                    }
                    if (TargetClient != null)
                    {
                        YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(TargetClient, "ACH_Spr", 1, false);
                    }
                }
            }

            else
            {
                if (YezzEnvironment.GetGame().GetNavigator().TryRemoveStaffPickedRoom(room.Id))
                {
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.SetQuery("DELETE FROM `navigator_staff_picks` WHERE `room_id` = @roomId LIMIT 1");
                        dbClient.AddParameter("roomId", room.Id);
                        dbClient.RunQuery();
                    }
                }
            }

            room.SendMessage(new RoomSettingsSavedComposer(room.RoomId));
            room.SendMessage(new RoomInfoUpdatedComposer(room.RoomId));
        }
    }
}