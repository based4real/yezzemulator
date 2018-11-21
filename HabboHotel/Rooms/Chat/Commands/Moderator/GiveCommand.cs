using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Rooms.Nux;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class GiveCommand : IChatCommand
    {
        public string PermissionRequired => "user_12";
        public string Parameters => "[USUARIO] [MONEDA] [MONTO]";
        public string Description => "Dar créditos, duckets, diamantes a un usuario.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor introduce ! (coins, duckets, diamonds, honor)", 34);
                return;
            }

            GameClient Target = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (Target == null)
            {
                Session.SendWhisper("Oops, No se ha conseguido este usuario!", 34);
                return;
            }

            string UpdateVal = Params[2];
            switch (UpdateVal.ToLower())
            {
                case "coins":
                case "credits":
                case "creditos":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_give_coins"))
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[3], out Amount))
                            {
                                Target.GetHabbo().Credits = Target.GetHabbo().Credits += Amount;
                                Target.SendMessage(new CreditBalanceComposer(Target.GetHabbo().Credits));

                                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                    Target.SendMessage(RoomNotificationComposer.SendBubble("cred", "" + Session.GetHabbo().Username + " te acaba de enviar " + Amount.ToString() + " créditos.", ""));
                                Session.SendWhisper("Le enviaste " + Amount + " Credito(s) a " + Target.GetHabbo().Username + "!", 34);
                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Oops, las cantidades solo en numeros..");
                                break;
                            }
                        }
                    }

                case "pixels":
                case "duckets":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_give_pixels"))
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para enviar duckets!");
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[3], out Amount))
                            {
                                if (Amount > 300)
                                {
                                    Session.SendWhisper("No pueden enviar más de 300 Duckets, esto será notificado al CEO y tomará medidas.");
                                    return;
                                }
                                else
                                {
                                    Target.GetHabbo().Duckets += Amount;
                                Target.SendMessage(new HabboActivityPointNotificationComposer(Target.GetHabbo().Duckets, Amount));

                                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                    Target.SendMessage(RoomNotificationComposer.SendBubble("duckets", "" + Session.GetHabbo().Username + " te acaba de enviar " + Amount.ToString() + " duckets.", ""));
                                Session.SendWhisper("Le enviaste " + Amount + " Ducket(s) a " + Target.GetHabbo().Username + "!", 34);
                                break;
                            }
                            }
                            else
                            {
                                Session.SendWhisper("Oops, las cantidades solo en numeros..");
                                break;
                            }
                        }
                    }

                case "diamonds":
                case "diamantes":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_give_diamonds"))
                        {
                            Session.SendWhisper("Oops, No tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[3], out Amount))
                            {
                                if (Session.GetHabbo().Rank >= 16)
                                {
                                    Target.GetHabbo().Diamonds += Amount;
                                    Target.SendMessage(new HabboActivityPointNotificationComposer(Target.GetHabbo().Diamonds, Amount, 5));

                                    if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                        Target.SendNotification(Session.GetHabbo().Username + " te ha enviado " + Amount.ToString() + " Diamante(s)!");
                                    Session.SendWhisper("Le enviaste " + Amount + " Diamante(s) a " + Target.GetHabbo().Username + "!");
                                    break;
                                }
                                else

                                if (Amount > 50)
                                {
                                    Session.SendWhisper("No pueden enviar más de 50 diamantes, esto será notificado al CEO y tomará medidas.");
                                    return;
                                }
                                Target.GetHabbo().Diamonds += Amount;
                                Target.SendMessage(new HabboActivityPointNotificationComposer(Target.GetHabbo().Diamonds, Amount, 5));

                                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                    Target.SendMessage(RoomNotificationComposer.SendBubble("diamonds", "" + Session.GetHabbo().Username + " te acaba de enviar " + Amount.ToString() + " Diamante(s).", ""));
                                Session.SendWhisper("Le enviaste " + Amount + " Diamante(s) a " + Target.GetHabbo().Username + "!");
                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Oops, las cantidades solo en numeros..!");
                                break;
                            }
                        }
                    }

                case "premio":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_massgive_reward"))
                        {
                            Session.SendWhisper("Oops, No tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        else
                        {

                            if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                Target.SendMessage(new NuxItemListComposer());


                            break;
                        }

                    }

                //case "gotw":
                case "pixeles":
                case "honor":
                case "copos":
                    {
                        if (!Session.GetHabbo().GetPermissions().HasCommand("command_give_gotw"))
                        {
                            Session.SendWhisper("Oops, no tiene el permiso necesario para usar este comando!");
                            break;
                        }

                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[3], out Amount))
                            {
                                if (Session.GetHabbo().Rank < 9 && Amount > 10)
                                {
                                    Session.SendWhisper("No pueden enviar más de 50 puntos, esto será notificado a los CEO y se tomarán las medidas oportunas.");
                                    return;
                                }

                                Target.GetHabbo().GOTWPoints = Target.GetHabbo().GOTWPoints + Amount;
                                Target.GetHabbo().UserPoints = Target.GetHabbo().UserPoints + 1;
                                Target.SendMessage(new HabboActivityPointNotificationComposer(Target.GetHabbo().GOTWPoints, Amount, 103));

                                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                    Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", "" + Session.GetHabbo().Username + " te acaba de enviar " + Amount + "  " + YezzEnvironment.GetDBConfig().DBData["seasonal.currency.name"] + ".\nHaz click para ver los premios disponibles.", "catalog/open/habbiween"));
                                Session.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", "Acabas de enviar " + Amount + "  " + YezzEnvironment.GetDBConfig().DBData["seasonal.currency.name"] + " a " + Target.GetHabbo().Username + "\nRecuerda que hemos depositado tu confianza en tí y que estos comandos los vemos en directo.", "catalog/open/habbiween"));
                                YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(Target, "ACH_AnimationRanking", 1);
                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Sólo puedes introducir parámetros numerales, de 1 a 50.");
                                break;
                            }
                        }
                    }
                default:
                    Session.SendWhisper("'" + UpdateVal + "' no es una moneda válida.");
                    break;
            }
        }
    }
}