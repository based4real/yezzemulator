using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Rooms.Nux;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class RoomGiveCommand : IChatCommand
    {
        public string PermissionRequired => "user_13";
        public string Parameters => "[MONEDA] [MONTO]";
        public string Description => "Dar créditos, duckets, diamantes a la sala.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor introduce ! (coins, duckets, diamonds, pixeles)");
                return;
            }

            string UpdateVal = Params[1];
            switch (UpdateVal.ToLower())
            {
                case "coins":
                case "credits":
                case "creditos":
                    {
                        if (Params.Length == 1)
                        {
                            Session.SendWhisper("Introduce el numero de creditos");
                            return;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[2], out Amount))
                                foreach (RoomUser User in Room.GetRoomUserManager().GetUserList().ToList())
                                {
                                    User.GetClient().GetHabbo().Credits += Amount;
                                    User.GetClient().SendMessage(new CreditBalanceComposer(User.GetClient().GetHabbo().Credits));
                                    User.GetClient().SendMessage(new RoomCustomizedAlertComposer(Session.GetHabbo().Username + " te acaba de regalar " + Amount + " Créditos."));
                                }
                        }
                        Session.SendWhisper("Enviaste correctamente en la sala " + Params[2] + " diamantes!");
                    }
                    break;

                case "diamonds":
                case "diamantes":

                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_give_diamonds"))
                        {
                            Session.SendWhisper("No tienes los permisos necesarios para usar este comando.", 34);
                            break;
                        }
                        else
                        {
                            int Amount;
                        if (int.TryParse(Params[2], out Amount))
                            if (Amount > 50)
                            {
                                Session.SendWhisper("No pueden enviar más de 50 Pixeles, esto será notificado al CEO y tomará medidas.");
                                return;
                            }
                            else
                            {
                                foreach (RoomUser User in Room.GetRoomUserManager().GetUserList().ToList())
                                {
                                    User.GetClient().GetHabbo().Diamonds += Amount;
                                    User.GetClient().SendMessage(new HabboActivityPointNotificationComposer(User.GetClient().GetHabbo().Diamonds, Amount, 5));
                                    User.GetClient().SendMessage(new RoomCustomizedAlertComposer(Session.GetHabbo().Username + " te acaba de regalar " + Amount + " Diamantes."));
                                }
                                Session.SendWhisper("Enviaste correctamente en la sala " + Params[2] + " diamantes!");

                            }
                    }
                    }
                    break;

                case "reward":
                case "regalo":
                case "premio":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_roomgive_reward"))
                        {
                            Session.SendWhisper("Oops, No tiene el permiso necesario para usar este comando!");
                            break;
                        }

                        else
                        {
                            foreach (RoomUser User in Room.GetRoomUserManager().GetUserList().ToList())
                            {
                                User.GetClient().SendMessage(new NuxItemListComposer());
                            }
                        }
                    }
                    break;

                case "duckets":
                    {
                        if (Params.Length == 1)
                        {
                            Session.SendWhisper("Introduce el numero de duckets");
                            return;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[2], out Amount))
                                if (Amount > 300)
                                {
                                    Session.SendWhisper("No pueden enviar más de 300 Duckets, esto será notificado al CEO y tomará medidas.");
                                    return;
                                }
                                else
                                {
                                    foreach (RoomUser User in Room.GetRoomUserManager().GetUserList().ToList())
                                {
                                    User.GetClient().GetHabbo().Duckets += Amount;
                                    User.GetClient().SendMessage(new HabboActivityPointNotificationComposer(User.GetClient().GetHabbo().Duckets, Amount));
                                    User.GetClient().SendMessage(new RoomCustomizedAlertComposer(Session.GetHabbo().Username + " te acaba de regalar " + Amount + " Duckets."));
                                }
                        }
                        }
                        Session.SendWhisper("Enviaste correctamente en la sala " + Params[2] + " Duckets!");
                    }
                    break;
            }
        }
    }
}
