using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.Communication.Packets.Outgoing.Rooms.Engine;
using Yezz.Communication.Packets.Outgoing.Rooms.Furni;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Items;
using Yezz.HabboHotel.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fan
{
    internal class BuildCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_give"; }
        }

        public string Parameters
        {
            get { return "%height%"; }
        }

        public string Description
        {
            get { return ""; }
        }
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            string height = Params[1];
            if (Session.GetHabbo().Id == Room.OwnerId)
            {
                if (!Room.CheckRights(Session, true))
                    return;
                Item[] items = Room.GetRoomItemHandler().GetFloor.ToArray();
                foreach (Item Item in items.ToList())
                {
                    GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUserID(Item.UserID);
                    if (Item.GetBaseItem().InteractionType == InteractionType.STACKTOOL)

                        Room.SendMessage(new UpdateMagicTileComposer(Item.Id, int.Parse(height)));
                }
            }
        }
    }
}