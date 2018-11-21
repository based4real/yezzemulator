﻿using System;

using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Rooms.Games;
using Yezz.HabboHotel.Rooms.Games.Teams;

namespace Yezz.HabboHotel.Items.Interactor
{
    public class IdolCounter : IFurniInteractor
    {
        public void OnPlace(GameClient Session, Item Item)
        {
            //if (Item.team == TEAM.NONE) USAR PARA EL USER
            //    return;

            Item.ExtraData = "0";
            Item.UpdateState(false, true);
        }

        public void OnRemove(GameClient Session, Item Item)
        {
            Item.ExtraData = "0";
        }

        public void OnTrigger(GameClient Session, Item Item, int nada, bool HasRights)
        {
            if (HasRights)
            {
                string votos = Item.GetRoom().yesPoolAnswers.Count.ToString();

                Item.ExtraData = votos;
                Item.UpdateState();
            }
        }

        public void OnWiredTrigger(Item Item)
        {
        }
    }
}