using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.HabboHotel.GameClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class GolpeCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_normal"; }
        }

        public string Parameters
        {
            get { return "[USUARIO]"; }
        }

        public string Description
        {
            get { return "Golpear a alguien si la sala lo permite."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("@green@ Introduce el nombre del usuario que deseas golpear. { :golpe NOMBRE }", 34);
                return;
            }

            //if (!Room.GolpeEnabled && !Session.GetHabbo().GetPermissions().HasRight("room_override_custom_config"))
            //{
            //    Session.SendWhisper("@red@ Oops, el dueño de la sala no permite que des golpes a otros en su sala.");
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
                Session.SendWhisper("@red@ Ocurrió un error, escribe correctamente el nombre, el usuario NO se encuentra online o en la sala.", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("@red@ ¿Estás loco o qué te pasa? ¡Maldito masoquista!", 34);
                return;
            }

            if (TargetUser.TeleportEnabled)
            {
                Session.SendWhisper("Oops, No puedes golpear a alguien si usas teleport.", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == "Forbi" || TargetClient.GetHabbo().Username == "Forb")
            {
                Session.SendWhisper("¡No puedes golpear a ese usuario!", 34);
                return;
            }

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            if (!((Math.Abs(TargetUser.X - ThisUser.X) >= 2) || (Math.Abs(TargetUser.Y - ThisUser.Y) >= 2)))
            {
                Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "*" + Params[1] + " ha recibido un golpe en la cara*", 0, ThisUser.LastBubble));
                Room.SendMessage(new ChatComposer(TargetUser.VirtualId, "¡Menuda ostia men!", 0, ThisUser.LastBubble));

                if (TargetUser.RotBody == 4)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.isLying = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 0)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.isLying = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 6)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.isLying = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 2)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.isLying = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 3)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.isLying = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 1)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.isLying = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 7)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.isLying = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 5)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.isLying = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

            }
            else
            {
                Session.SendWhisper("¡Oops, " + Params[1] + " no está lo suficientemente cerca!", 34);
            }
        }
    }
}
