using System.Linq;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Rooms.Nux;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class MassGiveCommand : IChatCommand
    {
        public string PermissionRequired => "user_13";
        public string Parameters => "[MONEDA] [MONTO]";
        public string Description => "Dar créditos, duckets, diamantes a todos en el Hotel.";

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Debes introducir el tipo de moneda: <b>creditos</b>, <b>duckets</b>, <b>diamantes</b>, <b>honor</b>.", 34);
                return;
            }

            string UpdateVal = Params[1];
            switch (UpdateVal.ToLower())
            {
                case "credits":
                case "creditos":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_give_coins"))
                        {
                            Session.SendWhisper("No tienes los permisos necesarios para usar este comando.", 34);
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[2], out Amount))
                            {
                                foreach (GameClient Target in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList())
                                {
                                    if (Target == null || Target.GetHabbo() == null || Target.GetHabbo().Username == Session.GetHabbo().Username)
                                        continue;

                                    Target.GetHabbo().Credits = Target.GetHabbo().Credits += Amount;
                                    Target.SendMessage(new CreditBalanceComposer(Target.GetHabbo().Credits));
                                    Target.SendMessage(RoomNotificationComposer.SendBubble("cred", "" + Session.GetHabbo().Username + " te acaba de enviar " + Amount + " créditos.", ""));

                                }

                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Sólo puedes introducir cantidades numerales.", 34);
                                break;
                            }
                        }
                    }

                case "duckets":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_give_pixels"))
                        {
                            Session.SendWhisper("No tienes los permisos necesarios para usar este comando.", 34);
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[2], out Amount))
                            {
                                if (Amount > 300)
                                {
                                    Session.SendWhisper("No pueden enviar más de 300 Duckets, esto será notificado al CEO y tomará medidas.");
                                    return;
                                }
                                else
                                {
                                    foreach (GameClient Target in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList())
                                {
                                    if (Target == null || Target.GetHabbo() == null || Target.GetHabbo().Username == Session.GetHabbo().Username)
                                        continue;

                                    Target.GetHabbo().Duckets += Amount;
                                    Target.SendMessage(new HabboActivityPointNotificationComposer(Target.GetHabbo().Duckets, Amount));
                                    Target.SendMessage(RoomNotificationComposer.SendBubble("duckets", "" + Session.GetHabbo().Username + " te acaba de enviar " + Amount + " duckets.", ""));
                                }
                                }
                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Sólo puedes introducir cantidades numerales.", 34);
                                break;
                            }
                        }
                    }

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
                            {
                                if (Amount > 50)
                                {
                                    Session.SendWhisper("No pueden enviar más de 50 diamantes, esto será notificado al CEO y tomará medidas.");
                                    return;
                                }
                                foreach (GameClient Target in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList())
                                {
                                    if (Target == null || Target.GetHabbo() == null || Target.GetHabbo().Username == Session.GetHabbo().Username)
                                        continue;

                                    Target.GetHabbo().Diamonds += Amount;
                                    Target.SendMessage(new HabboActivityPointNotificationComposer(Target.GetHabbo().Diamonds, Amount, 5));
                                }

                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Sólo puedes introducir cantidades numerales.", 34);
                                break;
                            }
                        }
                    }

                case "reward":
                case "premio":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_massgive_reward"))
                        {
                            Session.SendWhisper("Oops, No tiene el permiso necesario para usar este comando!");
                            break;
                        }

                        else
                        {
                            foreach (GameClient Target in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList())
                            {
                                if (Target == null || Target.GetHabbo() == null)
                                    continue;

                                Target.SendMessage(new NuxItemListComposer());
                            }
                        }

                    }
                    break;

                case "pixeles":
                case "honor":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_give_gotw"))
                        {
                            Session.SendWhisper("No tienes los permisos necesarios para usar este comando.", 34);
                            break;
                        }

                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[2], out Amount))
                            {
                                if (Amount > 50)
                                {
                                    Session.SendWhisper("No pueden enviar más de 50 Puntos, esto será notificado al CEO y tomará medidas.");
                                    return;
                                }

                                foreach (GameClient Target in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList())
                                {
                                    if (Target == null || Target.GetHabbo() == null || Target.GetHabbo().Username == Session.GetHabbo().Username)
                                        continue;

                                    Target.GetHabbo().GOTWPoints = Target.GetHabbo().GOTWPoints + Amount;
                                    Target.SendMessage(new HabboActivityPointNotificationComposer(Target.GetHabbo().GOTWPoints, Amount, 103));
                                    Target.SendMessage(RoomNotificationComposer.SendBubble("pumpkinz", "" + Session.GetHabbo().Username + " te acaba de enviar " + Amount + "  " + YezzEnvironment.GetDBConfig().DBData["seasonal.currency.name"] + ".\nHaz click para ver los premios disponibles.", "catalog/open/habbiween")); /*(RoomNotificationComposer.SendBubble("honor", "" + Session.GetHabbo().Username + " te acaba de enviar " + Amount + " puntos de honor.", ""));*/
                                }


                                break;
                            }
                            else
                            {
                                Session.SendWhisper("No tienes los permisos necesarios para usar este comando.", 34);
                                break;
                            }
                        }
                    }
                default:
                    Session.SendWhisper("¡'" + UpdateVal + "' no es una moneda válida!", 34);
                    break;
            }
        }
    }
}
