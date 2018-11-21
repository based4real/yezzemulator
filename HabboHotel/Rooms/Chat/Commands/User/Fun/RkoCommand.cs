using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class
        RkoCommand : IChatCommand
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
            get { return "Rko a otro usuario."; }
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
                    Session.SendWhisper("Usted no puede hacer el Rko a usted mismo!", 34);
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
                    Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "*RKO*", 0, User.LastBubble));
                    User.Statusses.Add("lay", "1.0");
                    User.Z -= 0.35;
                    User.isLying = true;
                    User.UpdateNeeded = true;
                    User.ApplyEffect(53);
                    Room.SendMessage(new ChatComposer(User.VirtualId, "*Obtiene RKO por " + Session.GetHabbo().Username + "*", 0, User.LastBubble));

                    User.GetClient().SendWhisper("Si no hay movimiento dentro de 5 segundos, automáticamente se levantará.", 34);

                    System.Threading.Thread thrd = new System.Threading.Thread(delegate ()
                    {
                        Thread.Sleep(5000);
                        User.ApplyEffect(0);
                        if (User.isLying)
                        {
                            User.Statusses.Remove("lay");
                            User.Z += 0.35;
                            User.isLying = true;
                            User.UpdateNeeded = true;
                            Room.SendMessage(new ChatComposer(User.VirtualId, "*Se levanta*", 0, User.LastBubble));
                        }

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