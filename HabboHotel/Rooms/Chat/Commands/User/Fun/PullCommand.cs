using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Pathfinding;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class PullCommand : IChatCommand
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
            get { return "Atraer a un usuario hacia tí."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Introduce el Nombre del usuario el cual quieres atraer", 34);
                return;
            }

            //if (!Room.PullEnabled && !Session.GetHabbo().GetPermissions().HasRight("room_override_custom_config"))
            //{
            //    Session.SendWhisper("Oops, el dueño de la sala no permite que hales a otros en su sala", 34);
            //    return;
            //}

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("Ocurrio un error, no se consigue al usuario, tal vez no esta online.", 34);
                return;
            }

            RoomUser TargetUser = Room.GetRoomUserManager().GetRoomUserByHabbo(TargetClient.GetHabbo().Id);
            if (TargetUser == null)
            {
                Session.SendWhisper("Ocurrio un error, escribe correctamente el nombre, el usuario NO se encuentra online o en la sala.", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Vamos! No te puedes jalar a ti mismo..", 34);
                return;
            }

            if (TargetUser.TeleportEnabled)
            {
                Session.SendWhisper("Oops, you cannot push a user whilst they have their teleport mode enabled.", 34);
                return;
            }

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            if (ThisUser.SetX - 1 == Room.GetGameMap().Model.DoorX)
            {
                Session.SendWhisper("Por favor no lo tires de la sala :(!", 34);
                return;
            }


            string PushDirection = "down";
            if (TargetClient.GetHabbo().CurrentRoomId == Session.GetHabbo().CurrentRoomId && (Math.Abs(ThisUser.X - TargetUser.X) < 3 && Math.Abs(ThisUser.Y - TargetUser.Y) < 3))
            {
                Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "*He atraído a " + Params[1] + "*", 0, ThisUser.LastBubble));

                if (ThisUser.RotBody == 0)
                    PushDirection = "up";
                if (ThisUser.RotBody == 2)
                    PushDirection = "right";
                if (ThisUser.RotBody == 4)
                    PushDirection = "down";
                if (ThisUser.RotBody == 6)
                    PushDirection = "left";

                if (PushDirection == "up")
                    TargetUser.MoveTo(ThisUser.X, ThisUser.Y - 1);

                if (PushDirection == "right")
                    TargetUser.MoveTo(ThisUser.X + 1, ThisUser.Y);

                if (PushDirection == "down")
                    TargetUser.MoveTo(ThisUser.X, ThisUser.Y + 1);

                if (PushDirection == "left")
                    TargetUser.MoveTo(ThisUser.X - 1, ThisUser.Y);
                return;
            }
            else
            {
                Session.SendWhisper("Este usuario no esta lo suficientemente cerca, acercate!!", 34);
                return;
            }
        }
    }
}
