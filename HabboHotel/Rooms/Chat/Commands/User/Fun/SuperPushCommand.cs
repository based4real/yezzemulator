using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class SuperPushCommand : IChatCommand
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
            get { return "Empuja a otro usuario 3 cuadrados de ti."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor ingrese el nombre de usuario que desea empujar.", 34);
                return;
            }

            //if (!Room.SPushEnabled && !Room.CheckRights(Session, true) && !Session.GetHabbo().GetPermissions().HasRight("room_override_custom_config"))
            //{
            //    Session.SendWhisper("Oops, it appears that the room owner has disabled the ability to use the push command in here.", 34);
            //    return;
            //}

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("Se ha producido un error al encontrar a ese usuario, tal vez no esta en línea.", 34);
                return;
            }

            RoomUser TargetUser = Room.GetRoomUserManager().GetRoomUserByHabbo(TargetClient.GetHabbo().Id);
            if (TargetUser == null)
            {
                Session.SendWhisper("Se ha producido un error al encontrar a ese usuario, tal vez no esta en línea o en esta sala.", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Vamos, seguramente no quieres empujarte!", 34);
                return;
            }

            if (TargetUser.TeleportEnabled)
            {
                Session.SendWhisper("Vaya, no puedes empujar a un usuario mientras tiene el modo teleport activado.", 34);
                return;
            }

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            if (!((Math.Abs(TargetUser.X - ThisUser.X) >= 2) || (Math.Abs(TargetUser.Y - ThisUser.Y) >= 2)))
            {
                if (TargetUser.SetX - 1 == Room.GetGameMap().Model.DoorX || TargetUser.SetY - 1 == Room.GetGameMap().Model.DoorY)
                {
                    Session.SendWhisper("Por favor, no empuje a ese usuario fuera de la sala :(!", 34);
                    return;
                }

                if (TargetUser.SetX - 2 == Room.GetGameMap().Model.DoorX || TargetUser.SetY - 2 == Room.GetGameMap().Model.DoorY)
                {
                    Session.SendWhisper("Por favor, no empuje a ese usuario fuera de la sala :(!", 34);
                    return;
                }

                if (TargetUser.SetX - 3 == Room.GetGameMap().Model.DoorX || TargetUser.SetY - 3 == Room.GetGameMap().Model.DoorY)
                {
                    Session.SendWhisper("Por favor, no empuje a ese usuario fuera de la sala :(!", 34);
                    return;
                }

                if (TargetUser.RotBody == 4)
                {
                    TargetUser.MoveTo(TargetUser.X, TargetUser.Y + 3);
                }

                if (ThisUser.RotBody == 0)
                {
                    TargetUser.MoveTo(TargetUser.X, TargetUser.Y - 3);
                }

                if (ThisUser.RotBody == 6)
                {
                    TargetUser.MoveTo(TargetUser.X - 3, TargetUser.Y);
                }

                if (ThisUser.RotBody == 2)
                {
                    TargetUser.MoveTo(TargetUser.X + 3, TargetUser.Y);
                }

                if (ThisUser.RotBody == 3)
                {
                    TargetUser.MoveTo(TargetUser.X + 3, TargetUser.Y);
                    TargetUser.MoveTo(TargetUser.X, TargetUser.Y + 3);
                }

                if (ThisUser.RotBody == 1)
                {
                    TargetUser.MoveTo(TargetUser.X + 3, TargetUser.Y);
                    TargetUser.MoveTo(TargetUser.X, TargetUser.Y - 3);
                }

                if (ThisUser.RotBody == 7)
                {
                    TargetUser.MoveTo(TargetUser.X - 3, TargetUser.Y);
                    TargetUser.MoveTo(TargetUser.X, TargetUser.Y - 3);
                }

                if (ThisUser.RotBody == 5)
                {
                    TargetUser.MoveTo(TargetUser.X - 3, TargetUser.Y);
                    TargetUser.MoveTo(TargetUser.X, TargetUser.Y + 3);
                }

                Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "*Super empujada a " + Params[1] + "*", 0, ThisUser.LastBubble));
            }
            else
            {
                Session.SendWhisper("Oops, " + Params[1] + " no está lo suficientemente cerca!", 34);
            }
        }
    }
}
