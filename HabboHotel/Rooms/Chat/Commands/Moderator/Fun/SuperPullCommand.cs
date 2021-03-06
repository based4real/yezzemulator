﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class SuperPullCommand : IChatCommand
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
            get { return "Hala a alguien sin liminte alguno"; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Introduce el nombre del usuario que deseas hacer el super pull.", 34);
                return;
            }

            //if (!Room.SPullEnabled && !Room.CheckRights(Session, true) && !Session.GetHabbo().GetPermissions().HasRight("room_override_custom_config"))
            //{
            //    Session.SendWhisper("Oops, al parecer el dueño de la sala ha prohibido hacer los super pull en su sala.");
            //    return;
            //}

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("Hay un Error, no se encuentra al usuario online o no se encuentra en la sala.", 34);
                return;
            }

            RoomUser TargetUser = Room.GetRoomUserManager().GetRoomUserByHabbo(TargetClient.GetHabbo().Id);
            if (TargetUser == null)
            {
                Session.SendWhisper("Hay un Error, no se encuentra al usuario online o no se encuentra en la sala..", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Vamos.. Seguramente usted no se querra empujar a si mismo!", 34);
                return;
            }

            if (TargetUser.TeleportEnabled)
            {
                Session.SendWhisper("Vaya, no puedes empujar a un usuario mientras tiene el modo de teleport activado.", 34);
                return;
            }

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            if (ThisUser.SetX - 1 == Room.GetGameMap().Model.DoorX)
            {
                Session.SendWhisper("Por favor, no tire a ese usuario fuera de la Habitacion :c ", 34);
                return;
            }

            if (ThisUser.RotBody % 2 != 0)
                ThisUser.RotBody--;
            if (ThisUser.RotBody == 0)
                TargetUser.MoveTo(ThisUser.X, ThisUser.Y - 1);
            else if (ThisUser.RotBody == 2)
                TargetUser.MoveTo(ThisUser.X + 1, ThisUser.Y);
            else if (ThisUser.RotBody == 4)
                TargetUser.MoveTo(ThisUser.X, ThisUser.Y + 1);
            else if (ThisUser.RotBody == 6)
                TargetUser.MoveTo(ThisUser.X - 1, ThisUser.Y);

            Room.SendMessage(new ChatComposer(ThisUser.VirtualId, "*Super tiron a " + Params[1] + "*", 0, ThisUser.LastBubble));
            return;
        }
    }
}