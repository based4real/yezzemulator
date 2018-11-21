using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Global;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Quests;

using Yezz.Communication.Packets.Outgoing.Rooms.Engine;
using Yezz.Database.Interfaces;
using Yezz.Communication.Packets.Outgoing.Moderation;

namespace Yezz.Communication.Packets.Incoming.Users
{
    class UpdateFigureDataEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.GetHabbo() == null)
                return;

            string Gender = Packet.PopString().ToUpper();
            string Look = YezzEnvironment.GetGame().GetAntiMutant().RunLook(Packet.PopString());

            if (Look == Session.GetHabbo().Look)
                return;

            if ((DateTime.Now - Session.GetHabbo().LastClothingUpdateTime).TotalSeconds <= 2.0)
            {
                Session.GetHabbo().ClothingUpdateWarnings += 1;
                if (Session.GetHabbo().ClothingUpdateWarnings >= 25)
                    Session.GetHabbo().SessionClothingBlocked = true;
                return;
            }

            if (Session.GetHabbo().SessionClothingBlocked)
                return;

            Session.GetHabbo().LastClothingUpdateTime = DateTime.Now;

            string[] AllowedGenders = { "M", "F" };
            if (!AllowedGenders.Contains(Gender))
            {
                Session.SendMessage(new BroadcastMessageAlertComposer("Lo sentimos, ha elegido un genero invalido"));
                return;
            }

            YezzEnvironment.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.PROFILE_CHANGE_LOOK);

            Session.GetHabbo().Look = YezzEnvironment.FilterFigure(Look);
            Session.GetHabbo().Gender = Gender.ToLower();

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("UPDATE users SET look = @look, gender = @gender WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                dbClient.AddParameter("look", Look);
                dbClient.AddParameter("gender", Gender);
                dbClient.RunQuery();
            }

            YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(Session, "ACH_AvatarLooks", 1);
            Session.SendMessage(new AvatarAspectUpdateMessageComposer(Look, Gender)); //esto
            if (Session.GetHabbo().Look.Contains("ha-1006"))
                YezzEnvironment.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.WEAR_HAT);

            if (Session.GetHabbo().InRoom)
            {
                RoomUser RoomUser = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
                if (RoomUser != null)
                {
                    Session.SendMessage(new UserChangeComposer(RoomUser, true));
                    Session.GetHabbo().CurrentRoom.SendMessage(new UserChangeComposer(RoomUser, false));
                }
            }

            foreach (var buddy in Session.GetHabbo().GetMessenger().GetFriends())
            {
                if (buddy.client == null)
                    continue;

                buddy.client.GetHabbo().GetMessenger().UpdateFriend(Session.GetHabbo().Id, Session, true);
            }
        }
    }
}