using System;
using System.Linq;
using System.Collections.Generic;

using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Inventory.Furni;

using Yezz.Communication.Packets.Outgoing.Rooms.Furni;
using Yezz.HabboHotel.Items.Crafting;

namespace Yezz.Communication.Packets.Incoming.Rooms.Furni
{
    class GetCraftingItemEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            //var result = Packet.PopString();

            //CraftingRecipe recipe = null;
            //foreach (CraftingRecipe Receta in YezzEnvironment.GetGame().GetCraftingManager().CraftingRecipes.Values)
            //{
            //    if (Receta.Result.Contains(result))
            //    {
            //        recipe = Receta;
            //        break;
            //    }
            //}

            //var Final = YezzEnvironment.GetGame().GetCraftingManager().GetRecipe(recipe.Id);

            //Session.SendMessage(new CraftingResultComposer(recipe, true));
            //Session.SendMessage(new CraftableProductsComposer());
        }
    }
}