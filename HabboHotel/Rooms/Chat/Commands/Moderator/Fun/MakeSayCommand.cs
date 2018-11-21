using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class MakeSayCommand : IChatCommand
    {
        public string PermissionRequired => "user_11";
        public string Parameters => "[USUARIO] [MENSAJE]";
        public string Description => "Que otro usuario diga algo.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            if (Params.Length == 1)
                Session.SendWhisper("Escribe correctamente el nombre del usuario");
            else
            {
                string Message = CommandManager.MergeParams(Params, 2);
                RoomUser TargetUser = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Params[1]);
                if (TargetUser != null)
                {
                    if (TargetUser.GetClient() != null && TargetUser.GetClient().GetHabbo() != null)
                        if (TargetUser.GetClient().GetHabbo().Username == "Forbi" || TargetUser.GetClient().GetHabbo().Username == "Forb")
                            Room.SendMessage(new ChatComposer(TargetUser.VirtualId, Message, 0, TargetUser.LastBubble));
                        else
                            Session.SendWhisper("El usuario no puede decir eso.", 34);
                }
                else
                    Session.SendWhisper("El usuario no se encuentra en la sala.", 34);
            }
        }
    }
}
