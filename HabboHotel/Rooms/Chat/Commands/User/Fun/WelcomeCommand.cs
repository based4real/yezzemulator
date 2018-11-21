using System;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using System.Threading;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class
        WelcomeCommand : IChatCommand
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
            get { return "Dale la bienvenida a un usuario."; }
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
                    Session.SendWhisper("Ese usuario no se puede encontrar, tal vez no está en línea o no está en la sala.", 34);
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
                    Session.SendWhisper("No puedes darte la bienvenida!", 34);
                    return;
                }
                RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
                if (ThisUser == null)
                    return;

                if (Math.Abs(User.X - ThisUser.X) < 2000 && Math.Abs(User.Y - ThisUser.Y) < 2000)
                {
                    Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "¡Hola " + TargetClient.GetHabbo().Username + ", bienvenid@ a la comunidad "+YezzEnvironment.HotelName+"!", 0, 4));
                    Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "Aquí podrás divertirte, chatear y hacer nuevos amigos. Espero que la pases bien.", 0, 4));
                    ThisUser.ApplyEffect(0);
                    System.Threading.Thread thrd = new System.Threading.Thread(delegate ()
                    {
                        Thread.Sleep(4000);
                        ThisUser.ApplyEffect(0);
                    });
                    thrd.Start();
                }
            }
        }
    }