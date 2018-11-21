using Yezz.Communication.Packets.Outgoing.Rooms.Engine;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.HabboHotel.Items.Interactor
{
    class InteractorPlantSeed : IFurniInteractor
    {
        public void OnPlace(GameClient Session, Item Item)
        {
        }

        public void OnRemove(GameClient Session, Item Item)
        {
        }

        public void OnTrigger(GameClient Session, Item Item, int Request, bool HasRights)
        {
            if (Session == null || Session.GetHabbo() == null || Item == null)
                return;

            Room Room = Session.GetHabbo().CurrentRoom;
            if (Room == null)
                return;

            RoomUser Actor = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (Actor == null)
                return;

            var tick = int.Parse(Item.ExtraData);
            if (Gamemap.TileDistance(Actor.X, Actor.Y, Item.GetX, Item.GetY) < 2)
            {
                tick++;
                Item.ExtraData = tick.ToString();
                Item.UpdateState(true, true);
                int X = Item.GetX, Y = Item.GetY, Rot = Item.Rotation;
                Double Z = Item.GetZ;
                if (tick >= 12)
                {
                    Item.ExtraData = "0";
                    YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(Actor.GetClient(), "ACH_AdvancedHorticulturist", 1);
                }
            }

        }

        public void OnWiredTrigger(Item Item)
        {

        }
    }
}
