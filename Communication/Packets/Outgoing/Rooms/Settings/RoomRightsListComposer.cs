using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.Cache;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Settings
{
    class RoomRightsListComposer : ServerPacket
    {
        public RoomRightsListComposer(Room Instance)
            : base(ServerPacketHeader.RoomRightsListMessageComposer)
        {
            base.WriteInteger(Instance.Id);

            base.WriteInteger(Instance.UsersWithRights.Count);
            foreach (int Id in Instance.UsersWithRights.ToList())
            {
                UserCache Data = YezzEnvironment.GetGame().GetCacheManager().GenerateUser(Id);
                if (Data == null)
                {
                    base.WriteInteger(0);
                    base.WriteString("Unknown Error");
                }
                else
                {
                    base.WriteInteger(Data.Id);
                    base.WriteString(Data.Username);
                }
            }
        }
    }
}
