using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.HabboHotel.GameClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class BurnCommand : IChatCommand
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
            get { return "Quemar a otro usuario."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Introduce el nombre del usuario que deseas quemar. { :quemar NOMBRE }", 34);
                return;
            }

            //if (!Room.QuemarEnabled && !Session.GetHabbo().GetPermissions().HasRight("room_override_custom_config"))
            //{
            //    Session.SendWhisper("@red@ Oops, el dueño de la sala no permite que quemes a otros en su sala.", 34);
            //    return;
            //}

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("Ocurrió un problema, al parecer el usuario no se encuentra online o usted no escribio bien el nombre", 34);
                return;
            }

            RoomUser TargetUser = Room.GetRoomUserManager().GetRoomUserByHabbo(TargetClient.GetHabbo().Id);
            if (TargetUser == null)
            {
                Session.SendWhisper("Ocurrió un error, escribe correctamente el nombre, el usuario NO se encuentra online o en la sala.", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == "Forbi" || TargetClient.GetHabbo().Username == "Forb")
            {
                Session.SendWhisper("¡El es tu patón!", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("No está mal que intentes quemarte a ti mismo... pero puede parecer extraño y pensarán que estás loco...", 34);
                return;
            }

            if (TargetUser.TeleportEnabled)
            {
                Session.SendWhisper("Oops, No puedes quemar a alguien si usas teleport.", 34);
                return;
            }

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            if (!((Math.Abs(TargetUser.X - ThisUser.X) >= 2) || (Math.Abs(TargetUser.Y - ThisUser.Y) >= 2)))
            {
                Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "*¡Empiezo a quemar a " + Params[1] + "!", 0, ThisUser.LastBubble));
                ThisUser.ApplyEffect(5);
                Room.SendMessage(new ChatComposer(TargetUser.VirtualId, "¡Ayuda! ¡Me están quemando!", 0, ThisUser.LastBubble));
                TargetUser.ApplyEffect(25);
            }
            else
            {
                Session.SendWhisper("@green@ ¡Oops, " + Params[1] + " no está lo suficientemente cerca!", 34);
            }
        }
    }
}
