using Yezz.Communication.Interfaces;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.HabboHotel.GameClients;
using System;
using System.Threading;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    internal class WeedCommand : IChatCommand
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
                return "[ACCEPT]";
            }
        }

        public string Description
        {
            get
            {
                return "Fumar un blunt, Blunts costo (1000c) C/u.";
            }
        }
        // Coded By Hamada.
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendNotification("¿Estás seguro de que quieres comprar un Blunt? Te costará 1000c\n\n" +
                 "Para confirmar, escriba \":smokeweed accept \". \n\nUna vez que hagas esto, no hay vuelta atrás!\n\n(Si no desea comprar el Blunt, por favor ignore este mensaje!)\n\n");
                return;
            }
            RoomUser roomUserByHabbo = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            if (Params.Length == 2 && Params[1].ToString() == "accept")
            {
                Session.GetHabbo().Credits = Session.GetHabbo().Credits -= 1000;
                Session.SendMessage(new CreditBalanceComposer(Session.GetHabbo().Credits));
                roomUserByHabbo.GetClient().SendWhisper("¡Usted acaba de comprar un Blunt por 1000c!");
                Thread.Sleep(1000);
                Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "*Saca un blunt, y se pone más ligero*", 0, 6), false);
                Thread.Sleep(2000);
                Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "*Enciende el blunt, y le da un fuerte jalón*", 0, 6), false);
                Thread.Sleep(2000);
                roomUserByHabbo.ApplyEffect(53);
                Thread.Sleep(2000);
                switch (new Random().Next(1, 4))
                {
                    case 1:
                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "Relajad@, en un viaje!", 0, 6), false);
                        break;
                    case 2:
                        roomUserByHabbo.ApplyEffect(70);
                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "¿Pero qué mier...? Siento que mi rostro esta gordo. Maldición, alguien haga algo", 0, 6), false);
                        break;
                    default:
                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "Jejeje, todo es divertido con colores. Parece un lollipop:D", 0, 6), false);
                        break;
                }
                Thread.Sleep(2000);
                Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "JAJAJAJAJAJAJAJAJAJAJA ¡QUE BUENO!", 0, 6), false);
                Thread.Sleep(2000);
                roomUserByHabbo.ApplyEffect(53);
                Thread.Sleep(2000);
                Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "*Fuma el resto del blunt y lo tira al piso*", 0, 6), false);
            }

        }
    }
}