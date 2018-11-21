using System;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using System.Threading;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class
        CutCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_vip"; }
        }
        public string Parameters
        {
            get { return "[USUARIO]"; }
        }
        public string Description
        {
            get { return "Disparale a un usuario"; }
        }
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
                if (Params.Length == 1)
                {
                    Session.SendWhisper("¡Debe ingresar un nombre de usuario!", 34);
                    return;
                }

                GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
                if (TargetClient == null)
                {
                    Session.SendWhisper("Ese usuario no se puede encontrar, tal vez no está en línea o no esta en la sala.", 34);
                    return;
                }

                RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(TargetClient.GetHabbo().Id);
                if (User == null)
                {
                    Session.SendWhisper("Ese usuario no se puede encontrar, tal vez no está en línea o no esta en la sala.", 34);
                    return;
                }
                RoomUser Self = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
                if (User == Self)
                {
                    Session.SendWhisper("¡No puedes dispararte!");
                    return;
                }
            if (TargetClient.GetHabbo().Username == "Forbi" || TargetClient.GetHabbo().Username == "Forb")
            {
                Session.SendWhisper("¡El es tu patón!", 34);
                return;
            }
            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
                if (ThisUser == null)
                    return;

                if (Math.Abs(User.X - ThisUser.X) < 2 && Math.Abs(User.Y - ThisUser.Y) < 2)
                {
                    Room.SendMessage(new ShoutComposer(ThisUser.VirtualId, "*Dispararle en la cabeza a " + TargetClient.GetHabbo().Username + "*", 0, ThisUser.LastBubble));
                    Room.SendMessage(new ChatComposer(User.VirtualId, "*Muriendo*", 0, User.LastBubble));

                    User.GetClient().SendWhisper("Se muere en 3 segundos!");
                    ThisUser.ApplyEffect(539);
                    User.ApplyEffect(93);
                    TargetClient.SendMessage(new FloodControlComposer(3));
                    if (User != null)
                        User.Frozen = true;
                    System.Threading.Thread thrd = new System.Threading.Thread(delegate ()
                    {
                        Thread.Sleep(4000);
                        if (User != null)
                            User.Frozen = false;
                        User.ApplyEffect(23);
                        Thread.Sleep(2000);
                        ThisUser.ApplyEffect(539);
                        User.ApplyEffect(23);
                        if (User == null)
                            return;

                        if (User.Statusses.ContainsKey("lie") || User.isLying || User.RidingHorse || User.IsWalking)
                            return;

                        if (!User.Statusses.ContainsKey("lay"))
                        {
                            if ((User.RotBody % 2) == 0)
                            {
                                if (User == null)
                                    return;

                                try
                                {
                                    User.Statusses.Add("lay", "1.0");
                                    User.Z -= 0.35;
                                    User.isLying = true;
                                    User.UpdateNeeded = true;
                                }
                                catch { }
                            }
                            else
                            {
                                User.RotBody--;
                                User.Statusses.Add("lay", "1.0");
                                User.Z -= 0.35;
                                User.isLying = true;
                                User.UpdateNeeded = true;
                            }
                        }
                        else if (User.isLying == true)
                        {
                            User.Z += 0.35;
                            User.Statusses.Remove("lay");
                            User.Statusses.Remove("1.0");
                            User.isLying = false;
                            User.UpdateNeeded = true;
                        }
                        Room.SendMessage(new ChatComposer(User.VirtualId, "*He muerto*", 0, User.LastBubble));

                    });
                    thrd.Start();
                }
                else
                {
                    Session.SendWhisper("Ese usuario está demasiado lejos, trata de acercarte.", 34);
                    return;
                }
            }
        }
    }