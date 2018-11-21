using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Rooms.AI;
using Yezz.Communication.Packets.Incoming;

namespace Yezz.Communication.Packets.Incoming.Catalog
{
    public class GetSellablePetBreedsEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            string Type = Packet.PopString();
            string PacketType = "";
            int PetId = YezzEnvironment.GetGame().GetCatalog().GetPetRaceManager().GetPetId(Type, out PacketType);

            Session.SendMessage(new SellablePetBreedsComposer(PacketType, PetId, YezzEnvironment.GetGame().GetCatalog().GetPetRaceManager().GetRacesForRaceId(PetId)));
        }
    }
}