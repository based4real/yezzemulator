using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Database.Interfaces;
using System;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class GiveRanksCommand : IChatCommand
    {
        public string PermissionRequired => "user_16";
        public string Parameters => "[USUARIO] [TIPO] [RANGO]";
        public string Description => "Escribe :rank para ver la explicación.";

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendMessage(new MassEventComposer("habbopages/chat/giverankinfo.txt"));
                return;
            }

            GameClient Target = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (Target == null)
            {
                Session.SendWhisper("Oops, no se ha conseguido este usuario.");
                return;
            }


            string RankType = Params[2];
            switch (RankType.ToLower())
            {
                case "Administrador":
                case "administrador":
                case "adm":
                    {
                        int NewRank = 15;
                        if (Session.GetHabbo().Rank < 12)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 16)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Encargado de la parte administrativa.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '15' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                        }
                        Target.Disconnect();
                        break;
                    }

                case "encargado":
                case "concursos":
                    {
                        int NewRank = 14;
                        if (Session.GetHabbo().Rank < 12)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 15)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Encargado del Hotel.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '15' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");

                        }
                        Target.Disconnect();
                        break;
                    }
                case "coadm":
                case "coadministrador":
                    {
                        int NewRank = 13;
                        if (Session.GetHabbo().Rank < 12)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 15)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Encargado de los concursos del Hotel.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '15' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");

                        }
                        Target.Disconnect();
                        break;
                    }
                case "gm":
                case "gamemaster":
                    {
                        int NewRank = 12;
                        if (Session.GetHabbo().Rank < 12)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 15)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Encargado de la diversión.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '15' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");

                        }
                        Target.Disconnect();
                        break;
                    }

                case "eds":
                case "seguridad":
                    {
                        int NewRank = 11;
                        if (Session.GetHabbo().Rank < 12)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 13)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Encargado de la seguridad.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '10' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");

                        }
                        Target.Disconnect();
                        break;
                    }

                case "mod":
                case "moderador":
                    {
                        int NewRank = 10;
                        if (Session.GetHabbo().Rank < 12)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 13)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Encargado de los casinos.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '10' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");

                        }
                        Target.Disconnect();
                        break;
                    }

                case "builder":
                case "baw":
                    {
                        int NewRank = 9;
                        if (Session.GetHabbo().Rank < 11)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 11)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Constructor oficial del Hotel.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '10' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");


                        }
                        Target.Disconnect();
                        break;
                    }

                case "dj":
                case "radio":
                    {
                        int NewRank = 8;
                        if (Session.GetHabbo().Rank < 11)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 11)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Estoy para servirle a los usuarios.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '5' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");

                        }
                        Target.Disconnect();
                        break;
                    }

                case "emb":
                case "embajador":
                    {
                        int NewRank = 7;
                        if (Session.GetHabbo().Rank < 11)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 11)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Encargado de la Publicidad.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '5' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");

                        }
                        Target.Disconnect();
                        break;
                    }

                case "helper":
                case "help":
                    {
                        int NewRank = 6;
                        if (Session.GetHabbo().Rank < 8)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 8)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Miembro del HabboVisa.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '5' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");

                        }
                        Target.Disconnect();
                        break;
                    }


                case "lince":
                case "lnc":
                    {
                        int NewRank = 5;
                        if (Session.GetHabbo().Rank < 8)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Session.GetHabbo().Rank < 8)
                        {
                            Session.SendWhisper("Oops, usted no tiene los permisos necesarios para usar este comando!");
                            break;
                        }
                        if (Target.GetHabbo().Rank == NewRank)
                        {
                            Session.SendWhisper("Oops, El usuario ya tiene este rango!");
                            break;
                        }
                        if (Target.GetHabbo().Rank >= Session.GetHabbo().Rank)
                        {
                            Session.SendWhisper("Oops, El usuario tiene un rango mayor que usted y no lo puede modificar!");
                            break;
                        }
                        //Target.SendMessage(RoomNotificationComposer.SendBubble("eventoxx", Session.GetHabbo().Username + " acaba de darte el rango de Fundador. Reinicia para aplicar los cambios respectivos.\n\nRecuerda que hemos depositado nuestra confianza en tí y que todo esfuerzo tiene su recompensa.", ""));
                        Session.SendWhisper("Rango entregado satisfactoriamente a " + Target.GetHabbo().Username + ".");
                        Target.GetHabbo().Rank = NewRank;
                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("UPDATE `users` SET `rank` = '" + NewRank + "', cms_role = 'Encargado de subir los ons.' WHERE `id` = '" + Target.GetHabbo().Id + "' LIMIT 1");
                            dbClient.runFastQuery("UPDATE `users` SET `respetos` = '5' WHERE `id` = '" + Target.GetHabbo().Id + "'");
                            dbClient.runFastQuery("UPDATE `users` SET `tag` = '®' WHERE `id` = '" + Target.GetHabbo().Id + "'");

                        }
                        Target.Disconnect();
                        break;
                    }

                default:
                    Session.SendWhisper(RankType + "' no es un rango disponible para otorgar.");
                    break;
            }


        }
    }
}