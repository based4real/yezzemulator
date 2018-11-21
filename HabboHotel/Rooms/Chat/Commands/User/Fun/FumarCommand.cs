using Yezz.Communication.Interfaces;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.HabboHotel.GameClients;
using System;
using System.Threading;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    internal class FumarCommand : IChatCommand
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
                return "Fúmate un Blunt, Blunts cuestan (1000c)..";
            }
        }
        // Coded By Hamada.
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendNotification("¿Está seguro de que quiere comprar un Blunt? Le va a costar a usted 1000créditos\n\n" +
                 "Si aceptas, escribe \":fumar accept\". \n Al hacer esto, no hay vuelta atrás!\n\n(Si no quiere comprar el Blunt, ignora este mensaje!)\n\n");
                return;
            }
            RoomUser roomUserByHabbo = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (roomUserByHabbo == null)
                return;
            if (Params.Length == 2 && Params[1].ToString() == "accept")
            {
                Session.GetHabbo().Credits = Session.GetHabbo().Credits -= 1000;
                Session.SendMessage(new CreditBalanceComposer(Session.GetHabbo().Credits));
                roomUserByHabbo.GetClient().SendWhisper("Has comprado correctamente un Blunt por 1000 créditos!");
                Thread.Sleep(1000);
                Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "*Saco la Marihuana y la mezclo*", 0, 6), false);
                Thread.Sleep(2000);
                Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "*Lo rulo, y me lo enciendo*", 0, 6), false);
                Thread.Sleep(2000);
                roomUserByHabbo.ApplyEffect(53);
                Thread.Sleep(2000);
                switch (new Random().Next(1, 4))
                {
                    case 1:
                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "Hey, ¿por qué me siento cómo si volara? Por que yo volé de el y c fue a la puta, lol", 0, 6), false);
                        break;
                    case 2:
                        roomUserByHabbo.ApplyEffect(70);
                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "Yo WTF! Siento que mi cara es gruesa, soy un puto Panda, Damm soy blanco Wow Que alguien haga algo!", 0, 6), false);
                        break;
                    default:
                        Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "Hehehe voy muy drogado necesito cagar, me gusta es un caramelo :D", 0, 6), false);
                        break;
                }
                Thread.Sleep(2000);
                Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "HAHAAHHAHAHAHAAHAHAHHAHAHAHA LOL", 0, 6), false);
                Thread.Sleep(2000);
                roomUserByHabbo.ApplyEffect(0);
                Thread.Sleep(2000);
                Room.SendMessage((IServerPacket)new ChatComposer(roomUserByHabbo.VirtualId, "*Me fumo el resto y me da un amarillo*", 0, 6), false);
            }

        }
    }
}