using Yezz.HabboHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Incoming.Rooms.Avatar
{
    class ApplySignEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            int SignId = Packet.PopInt();
            Room Room;

            if (!YezzEnvironment.GetGame().GetRoomManager().TryGetRoom(Session.GetHabbo().CurrentRoomId, out Room))
                return;

            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (User == null)
                return;


            User.UnIdle();

            User.AddStatus("sign", Convert.ToString(SignId));
            User.UpdateNeeded = true;
            User.SignTime = YezzEnvironment.GetUnixTimestamp() + 5;
        }
    }
}