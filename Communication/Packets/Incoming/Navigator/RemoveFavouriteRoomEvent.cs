
using Yezz.Communication.Packets.Outgoing.Navigator;

using Yezz.Database.Interfaces;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Users;
using Yezz.Communication.Packets.Incoming;

namespace Yezz.Communication.Packets.Incoming.Navigator
{
    public class RemoveFavouriteRoomEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            int Id = Packet.PopInt();

            Session.GetHabbo().FavoriteRooms.Remove(Id);
            Session.SendMessage(new UpdateFavouriteRoomComposer(Id, false));

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.RunQuery("DELETE FROM user_favorites WHERE user_id = " + Session.GetHabbo().Id + " AND room_id = " + Id + " LIMIT 1");
            }
        }
    }
}