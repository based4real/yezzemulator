using System;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using System.Threading;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class
        SlapassCommand : IChatCommand
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
            get { return "Golpee el culo de otro usuario."; }
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
                    Session.SendWhisper("Ese usuario no se puede encontrar, tal vez está fuera de línea o no en la sala.", 34);
                    return;
                }

            if (TargetClient.GetHabbo().Username == "Forbi" || TargetClient.GetHabbo().Username == "Forb")
            {
                Session.SendWhisper("¡El es tu patón!", 34);
                return;
            }

            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(TargetClient.GetHabbo().Id);
                if (User == null)
                {
                    Session.SendWhisper("El usuario no se puede encontrar, tal vez no está en línea o no esta en la sala.", 34);
                    return;
                }
                RoomUser Self = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
                if (User == Self)
                {
                    Session.SendWhisper("No puedes golpearte el culo... !", 34);
                    return;
                }
                RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
                if (ThisUser == null)
                    return;

                if (Math.Abs(User.X - ThisUser.X) < 2 && Math.Abs(User.Y - ThisUser.Y) < 2)
                {
                    Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "*Golpear el culo de " + TargetClient.GetHabbo().Username + "*", 0, User.LastBubble));
                    Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "*¡Maldición! Que asno*", 0, User.LastBubble));
                    Room.SendMessage(new ShoutComposer(User.VirtualId, "¡Ouch, eso duele " + Session.GetHabbo().Username + "!", 0, User.LastBubble));
                    ThisUser.ApplyEffect(9);
                    System.Threading.Thread thrd = new System.Threading.Thread(delegate ()
                    {
                        Thread.Sleep(4000);
                        ThisUser.ApplyEffect(0);
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