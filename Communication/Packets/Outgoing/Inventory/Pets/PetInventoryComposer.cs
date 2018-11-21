﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms.AI;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Users.Inventory;

namespace Yezz.Communication.Packets.Outgoing.Inventory.Pets
{
    class PetInventoryComposer : ServerPacket
    {
        public PetInventoryComposer(ICollection<Pet> Pets)
            : base(ServerPacketHeader.PetInventoryMessageComposer)
        {
            base.WriteInteger(1);
            base.WriteInteger(1);
            base.WriteInteger(Pets.Count);
            foreach (Pet Pet in Pets.ToList())
            {
                base.WriteInteger(Pet.PetId);
                base.WriteString(Pet.Name);
                base.WriteInteger(Pet.Type);
                base.WriteInteger(int.Parse(Pet.Race));
                base.WriteString(Pet.Color);
                base.WriteInteger(0);
                base.WriteInteger(0);
                base.WriteInteger(0);
            }
        }
    }
}