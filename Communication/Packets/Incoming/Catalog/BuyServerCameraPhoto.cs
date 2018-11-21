﻿using Yezz.Communication.Packets.Incoming.Rooms.Camera;
using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.Communication.Packets.Outgoing.Rooms.Camera;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.Camera;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Items;
using Yezz.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;

namespace Yezz.Communication.Packets.Incoming.Catalog
{
    public class BuyServerCameraPhoto : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket paket)
        {
            if (!Session.GetHabbo().lastPhotoPreview.Contains("-"))
                return;

            if (Session.GetHabbo().Duckets < 10)
            {
                Session.SendMessage(RoomNotificationComposer.SendBubble("definitions", "Necesitas tener al menos 10 Duckets para adquirir una foto en " + YezzEnvironment.HotelName + ".", ""));
                return;
            }

            if (Session.GetHabbo().Credits < 100)
            {
                Session.SendMessage(RoomNotificationComposer.SendBubble("definitions", "Necesitas tener al menos 100 Créditos para adquirir una foto en " + YezzEnvironment.HotelName + ".", ""));
                return;
            }

            string roomId = Session.GetHabbo().lastPhotoPreview.Split('-')[0];
            string timestamp = Session.GetHabbo().lastPhotoPreview.Split('-')[1];
            string md5image = URLPost.GetMD5(Session.GetHabbo().lastPhotoPreview);
            ItemData Item = null;
            if (!YezzEnvironment.GetGame().GetItemManager().GetItem(1100001495, out Item))
                return;
            if (Item == null)
                return;


            Item photoPoster = ItemFactory.CreateSingleItemNullable(Item, Session.GetHabbo(), "{\"timestamp\":\"" + timestamp + "\", \"id\":\"" + md5image + "\"}", "");

            if (photoPoster != null)
            {
                Session.GetHabbo().GetInventoryComponent().TryAddItem(photoPoster);

                Session.GetHabbo().Credits -= 100;
                Session.SendMessage(new CreditBalanceComposer(Session.GetHabbo().Credits));

                Session.GetHabbo().Duckets -= 10;
                Session.SendMessage(new HabboActivityPointNotificationComposer(Session.GetHabbo().Duckets, Session.GetHabbo().Duckets));

                Session.SendMessage(new FurniListAddComposer(photoPoster));
                Session.SendMessage(new FurniListUpdateComposer());
                YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(Session, "ACH_CameraPhotoCount", 1);
            }

            Session.SendMessage(new BuyPhoto());

            Session.GetHabbo().GetInventoryComponent().UpdateItems(false);

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("INSERT INTO items_camera VALUES (@id, '" + Session.GetHabbo().Id + "',@creator_name, '" + roomId + "','" + timestamp + "', '" + YezzEnvironment.GetUnixTimestamp() + "')");
                dbClient.AddParameter("id", md5image);
                dbClient.AddParameter("creator_name", Session.GetHabbo().Username);
                dbClient.RunQuery();
            }

        }
    }
}
