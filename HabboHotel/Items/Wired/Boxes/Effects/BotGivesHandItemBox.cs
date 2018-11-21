﻿using Yezz.Communication.Packets.Incoming;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Users;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.HabboHotel.Items.Wired.Boxes.Effects
{
    class BotGivesHandItemBox : IWiredItem
    {
        public Room Instance { get; set; }
        public Item Item { get; set; }
        public WiredBoxType Type { get { return WiredBoxType.EffectBotGivesHanditemBox; } }
        public ConcurrentDictionary<int, Item> SetItems { get; set; }
        public string StringData { get; set; }
        public bool BoolData { get; set; }
        public string ItemsData { get; set; }

        public BotGivesHandItemBox(Room Instance, Item Item)
        {
            this.Instance = Instance;
            this.Item = Item;
            this.SetItems = new ConcurrentDictionary<int, Item>();
        }

        public void HandleSave(ClientPacket Packet)
        {
            int Unknown = Packet.PopInt();
            int DrinkID = Packet.PopInt();
            string BotName = Packet.PopString();

            if (this.SetItems.Count > 0)
                this.SetItems.Clear();

            this.StringData = BotName.ToString() + ";" + DrinkID.ToString();
        }

        public bool Execute(params object[] Params)
        {
            if (Params == null || Params.Length == 0)
                return false;

            if (String.IsNullOrEmpty(this.StringData))
                return false;

            Habbo Player = (Habbo)Params[0];

            if (Player == null)
                return false;

            RoomUser Actor = this.Instance.GetRoomUserManager().GetRoomUserByHabbo(Player.Id);

            if (Actor == null)
                return false;

            RoomUser User = this.Instance.GetRoomUserManager().GetBotByName(this.StringData.Split(';')[0]);

            if (User == null)
                return false;

            if (User.BotData.TargetUser == 0)
            {
                if (!Instance.GetGameMap().CanWalk(Actor.SquareBehind.X, Actor.SquareBehind.Y, false))
                {
                    Player.GetClient().SendMessage(new WhisperComposer(User.VirtualId, "No puedo alcanzarte ¡debes acercarte más a mi!", 0, 31));
                }
                else
                {
                    string[] Data = this.StringData.Split(';');

                    int DrinkId = int.Parse(Data[1]);

                    User.CarryItem(DrinkId);
                    User.BotData.TargetUser = Actor.HabboId;
                    Player.GetClient().SendMessage(new WhisperComposer(User.VirtualId, "Aquí tienes tu bebida " + Player.GetClient().GetHabbo().Username + "!", 0, 31));

                    User.MoveTo(Actor.SquareBehind.X, Actor.SquareBehind.Y);
                }
            }
            return true;
        }
    }
}
