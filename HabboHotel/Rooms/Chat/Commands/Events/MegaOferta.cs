using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yezz.Utilities;
using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.GameClients;
using Yezz.Database.Interfaces;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Catalog;
using Yezz.Core;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class MegaOferta : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_15"; }
        }

        public string Parameters
        {
            get { return "[ON] ó [OFF]"; }
        }

        public string Description
        {
            get { return "Encender o apagar una mega oferta."; ; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendMessage(RoomNotificationComposer.SendBubble("advice", "Ops, usted debe teclear así: ':megaoferta on o :megaoferta off'!", ""));
                return;
            }

            if (Params[1] == "on" || Params[1] == "ON")
            {
                // Comando editaveu abaixo mais cuidado pra não faze merda
                using (var dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.RunQuery("UPDATE targeted_offers SET active = 'true' WHERE active = 'false'");
                    dbClient.RunQuery("UPDATE users SET targeted_buy = '0'");
                }
                YezzEnvironment.GetGame().GetTargetedOffersManager().Initialize(YezzEnvironment.GetDatabaseManager().GetQueryReactor());
                YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("volada", "¡Corre, nueva mega oferta fue colocada!", ""));
                Session.SendWhisper("Nova mega oferta iniciada!");
            }

            if (Params[1] == "off" || Params[1] == "OFF")
            {
                // Comando editaveu abaixo mais cuidado pra não faze merda
                using (var dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.RunQuery("UPDATE targeted_offers SET active = 'false' WHERE active = 'true'");
                    dbClient.RunQuery("UPDATE users SET targeted_buy = '0'");
                }
                YezzEnvironment.GetGame().GetTargetedOffersManager().Initialize(YezzEnvironment.GetDatabaseManager().GetQueryReactor());
                YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("ADM", "¡Qué pena, la mega oferta fue retirada!", ""));
                Session.SendWhisper("Mega oferta retirada!");
            }

            if (Params[1] != "on" || Params[1] != "off")
            {
                //Session.SendMessage(new RoomNotificationComposer("erro", "message", "Ops, usted debe teclear así: ':megaoferta on o :megaoferta off'!"));
                Session.SendMessage(RoomNotificationComposer.SendBubble("advice", "Ops, usted debe teclear así: ':megaoferta on o :megaoferta off'!", ""));

            }
        }
    }
}
