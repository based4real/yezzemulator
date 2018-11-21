using System.Collections.Generic;

using Yezz.HabboHotel.Items;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Trading
{

    public class TradeUser
    {
        public int UserId;
        private readonly int RoomId;
        public List<Item> OfferedItems;

        public TradeUser(int UserId, int RoomId)
        {
            this.UserId = UserId;
            this.RoomId = RoomId;
            HasAccepted = false;
            OfferedItems = new List<Item>();
        }

        public bool HasAccepted { get; set; }

        public RoomUser GetRoomUser()
        {
            Room Room;

            if (!YezzEnvironment.GetGame().GetRoomManager().TryGetRoom(RoomId, out Room))
                return null;

            return Room.GetRoomUserManager().GetRoomUserByHabbo(UserId);
        }

        public GameClient GetClient()
        {
            return YezzEnvironment.GetGame().GetClientManager().GetClientByUserID(UserId);
        }
    }
}
