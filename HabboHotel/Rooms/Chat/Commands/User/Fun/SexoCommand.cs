using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Interfaces;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.HabboHotel.GameClients;
using System;
using System.Threading;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class
       SexoCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get
            {
                return "user_normal";
            }
        }

        public string Parameters
        {
            get
            {
                return "[USUARIO]";
            }
        }

        public string Description
        {
            get
            {
                return "Tenga relaciones sexuales con un usuario.";
            }
        }
        // Coded By Hamada.
        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            long nowTime = YezzEnvironment.CurrentTimeMillis();
            long timeBetween = nowTime - Session.GetHabbo()._lastTimeUsedHelpCommand;
            if (timeBetween < 300000)
            {
                Session.SendMessage(RoomNotificationComposer.SendBubble("advice", "Espera al menos 1 minuto para volver a usar el comando.", ""));
                return;
            }

            Session.GetHabbo()._lastTimeUsedHelpCommand = nowTime;

            RoomUser roomUserByHabbo1 = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo1 == null)
                return;

            if (Params.Length == 0)
            {
                Session.SendWhisper("Debe introducir el nombre de usuario de la persona con la que desea tener relaciones sexuales.", 34);
            }
            else
            {
                RoomUser roomUserByHabbo2 = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Params[1]);
                GameClient clientByUsername = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

                if (clientByUsername.GetHabbo().Username == "Forbi" || clientByUsername.GetHabbo().Username == "Forb")
                {
                    Session.SendWhisper("¡No puedes tener relaciones con ese usuario!", 34);
                    return;
                }

                if (clientByUsername.GetHabbo().Username == Session.GetHabbo().Username)
                {

                    RoomUser Self = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
                    if (roomUserByHabbo1 == Self)
                    {
                        Session.SendWhisper("¡No puedes tener sexo contigo mismo!", 34);
                        return;
                    }
                }
                else if (clientByUsername.GetHabbo().CurrentRoomId == Session.GetHabbo().CurrentRoomId && (Math.Abs(checked(roomUserByHabbo1.X - roomUserByHabbo2.X)) < 2 && Math.Abs(checked(roomUserByHabbo1.Y - roomUserByHabbo2.Y)) < 2))
                {
                    if ((Session.GetHabbo().sexWith == null || Session.GetHabbo().sexWith == "") && (clientByUsername.GetHabbo().Username != Session.GetHabbo().sexWith && Session.GetHabbo().Username != clientByUsername.GetHabbo().sexWith))
                    {
                        Session.GetHabbo().sexWith = clientByUsername.GetHabbo().Username;
                        clientByUsername.SendNotification(Session.GetHabbo().Username + " ha pedido tener relaciones sexuales contigo, para tener relaciones con " + Session.GetHabbo().Username + " escriba \":sexo " + Session.GetHabbo().Username + "\"");
                        Session.SendNotification("Le has enviado tu solicitud de sexo a " + clientByUsername.GetHabbo().Username + ". Si responde, usted podrá tener relaciones sexuales.");
                    }
                    else if (roomUserByHabbo2 != null)
                    {
                        if (clientByUsername.GetHabbo().sexWith == Session.GetHabbo().Username)
                        {
                            if (roomUserByHabbo2.GetClient() != null && roomUserByHabbo2.GetClient().GetHabbo() != null)
                            {
                                if (clientByUsername.GetHabbo().CurrentRoomId == Session.GetHabbo().CurrentRoomId && (Math.Abs(checked(roomUserByHabbo1.X - roomUserByHabbo2.X)) < 2 && Math.Abs(checked(roomUserByHabbo1.Y - roomUserByHabbo2.Y)) < 2))
                                {
                                    clientByUsername.GetHabbo().sexWith = (string)null;
                                    Session.GetHabbo().sexWith = (string)null;
                                    if (Session.GetHabbo().Gender == "m")
                                    {
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Agarra a " + Params[1] + " le da la vuelta, y comienzan a tener sexo*", 0, 16), false);
                                        Thread.Sleep(2000);
                                        roomUserByHabbo1.ApplyEffect(9);
                                        roomUserByHabbo2.ApplyEffect(9);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Mirada picara a " + Session.GetHabbo().Username + "*", 0, 16), false);
                                        Thread.Sleep(2000);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Tocando lentamente a " + Params[1] + ", he inserta su pene*", 0, 16), false);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*¿Te gusta " + Params[1] + "?, dandole rápido*", 0, 16), false);
                                        Room.SendMessage((IServerPacket)new ShoutComposer(roomUserByHabbo2.VirtualId, "*Sigue " + Session.GetHabbo().Username + ", me encanta *", 0, 16), false);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "ahhh ahhh, que bien se siente, ahhhhhhh*", 0, 16), false);
                                        Thread.Sleep(2000);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Le da lento, estoy apunto de acabar*", 0, 16), false);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Toca su entrepierna, para hacer el orgasmo mejor para " + Session.GetHabbo().Username + "*", 0, 16), false);
                                        Thread.Sleep(2000);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Saca su pene, y le acaba a " + Params[1] + "*", 0, 16), false);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Mirada picara* Espero que se repita!", 0, 16), false);
                                        Thread.Sleep(2000);
                                        roomUserByHabbo1.ApplyEffect(9);
                                        roomUserByHabbo2.ApplyEffect(9);
                                    }
                                    else
                                    {
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Toca las entrepiernas de " + Session.GetHabbo().Username + "*", 0, 16), false);
                                        Thread.Sleep(1000);
                                        roomUserByHabbo1.ApplyEffect(9);
                                        roomUserByHabbo2.ApplyEffect(9);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Mirada picara a " + Params[1] + "*", 0, 16), false);
                                        Thread.Sleep(2000);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Golpea suavemente el coño de " + Session.GetHabbo().Username + "*", 0, 16), false);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Mmmmmm* Esto se siente bien.. |.|", 0, 16), false);
                                        Thread.Sleep(2000);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Frota las entrepiertas de " + Session.GetHabbo().Username + "* ", 0, 16), false);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*mm, mm, mm, mm, mmmmmm!*", 0, 16), false);
                                        Thread.Sleep(2000);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Retrocede y le acaba en el cuerpo a " + Session.GetHabbo().Username + "*", 0, 16), false);
                                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Morder el labio* ¡Eso fue increíble!", 0, 16), false);
                                        Thread.Sleep(1000);
                                        roomUserByHabbo1.ApplyEffect(0);
                                        roomUserByHabbo2.ApplyEffect(0);
                                    }

                                }
                                else
                                    Session.SendWhisper("Ese usuario está demasiado lejos para tener sexo contigo!", 34);
                            }
                            else
                                Session.SendWhisper("Se ha producido un error al encontrar a el usuario.", 34);
                        }
                        else
                            Session.SendWhisper("Este usuario no ha aceptado tu solicitud de sexo.", 34);
                    }
                    else
                        Session.SendWhisper("Este usuario no se pudo encontrar en la sala.", 34);
                }
                else
                    Session.SendWhisper("Ese usuario está demasiado lejos para tener sexo contigo!", 34);
            }
        }
    }
}
