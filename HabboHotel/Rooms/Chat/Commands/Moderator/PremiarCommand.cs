using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;
using Yezz.Database.Interfaces;
using System.Data;
using Yezz.Communication.Packets.Outgoing.Users;
using Yezz.HabboHotel.Quests;
using Yezz.Core;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class PremiarCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_12"; }
        }

        public string Parameters
        {
            get { return "[USUARIO]"; }
        }

        public string Description
        {
            get { return "Hace todas las funciones para premiar a un ganador de eventos."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {

            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor, introduzca el usuario que desea premiar!", 34);
                return;
            }

            GameClient Target = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (Target == null)
            {
                Session.SendWhisper("¡Opa, no fue posible encontrar ese usuario!", 34);
                return;
            }

            RoomUser TargetUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Target.GetHabbo().Id);
            if (TargetUser == null)
            {
                Session.SendWhisper("Usuario no encontrado! Tal vez no esté en línea o no esta sala.", 34);
                return;
            }

            if (Target.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("¡Usted no puede premiarse!", 34);
                return;
            }

            // Comando editaveu abaixo mais cuidado pra não faze merda

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
            {
                return;
            }
            else
            {
                Target.GetHabbo().Diamonds += Convert.ToInt32(YezzEnvironment.GetConfig().data["Diamantespremiar"]);
                Target.SendMessage(new HabboActivityPointNotificationComposer(Target.GetHabbo().Diamonds, 1, 5));

                Session.SendMessage(RoomNotificationComposer.SendBubble("moedas", "Ganaste " + Convert.ToInt32(YezzEnvironment.GetConfig().data["Diamantespremiar"]) + " Diamante(s)! ¡Enhorabuena " + Target.GetHabbo().Username + "!", ""));

                if (Target.GetHabbo().Rank >= 0)
                {
                    DataRow dFurni = null;
                    using (var dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        //BUscame WMTOTEM
                        dbClient.SetQuery("SELECT public_name FROM furniture WHERE id = '42636366'");
                        dFurni = dbClient.getRow();
                    }
                    Target.GetHabbo().GetInventoryComponent().AddNewItem(0, 42636366, Convert.ToString(dFurni["public_name"]), 1, true, false, 0, 0);
                    Target.GetHabbo().GetInventoryComponent().UpdateItems(false);

                }

                if (Session.GetHabbo().Rank >= 0)
                {
                    DataRow nivel = null;
                    using (var dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.SetQuery("SELECT premio FROM users WHERE id = '" + Target.GetHabbo().Id + "'");
                        nivel = dbClient.getRow();
                        dbClient.RunQuery("UPDATE users SET premio = premio + '1' WHERE id = '" + Target.GetHabbo().Id + "'");
                        dbClient.RunQuery("UPDATE users SET puntos_eventos = puntos_eventos + '1' WHERE id = '" + Target.GetHabbo().Id + "'");
                        dbClient.RunQuery();
                    }

                    if (Convert.ToString(nivel["premio"]) != YezzEnvironment.GetConfig().data["NiveltotalGames"])
                    {
                        string emblegama = "NV" + Convert.ToString(nivel["premio"]);

                        if (!Target.GetHabbo().GetBadgeComponent().HasBadge(emblegama))
                        {
                            Target.GetHabbo().GetBadgeComponent().GiveBadge(emblegama, true, Target);
                            if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                Target.SendMessage(RoomNotificationComposer.SendBubble("badge/" + emblegama, "Usted acaba de recibir una placa de juego nivel: " + emblegama + " !", ""));
                            YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(Target, "ACH_Evento", 1);
                            string figure = Target.GetHabbo().Look;
                            YezzEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("fig/" + figure, TargetUser.GetUsername() + " ha ganado un evento en el hotel. ¡Enhorabuena!", "Nivel del usuario: NIVEL " + Convert.ToString(nivel["premio"]) + "!"));
                        }
                        else
                            Session.SendWhisper("Ops, se produjo un error en el sistema de dar insignias automáticas! Error en el emblema: (" + emblegama + ") !", 34);
                        Session.SendWhisper("¡Comando (premiado) realizado con éxito!", 34);
                    }
                }
            }
        }

        private void SendMessage(RoomNotificationComposer roomNotificationComposer)
        {
            throw new NotImplementedException();
        }
    }
}
