using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms.AI;
using Yezz.Communication.Packets.Outgoing.Inventory.Pets;

namespace Yezz.Communication.Packets.Incoming.Inventory.Pets
{
    class GetPetInventoryEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session.GetHabbo().GetInventoryComponent() == null)
                return;

            ICollection<Pet> Pets = Session.GetHabbo().GetInventoryComponent().GetPets();
            Session.SendMessage(new PetInventoryComposer(Pets));
        }
    }
}