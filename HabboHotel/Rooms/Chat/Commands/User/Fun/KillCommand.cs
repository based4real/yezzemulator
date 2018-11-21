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
    class KillCommand : IChatCommand
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
            get { return "Mata al usuario x.x"; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Escribe el nombre de tu victima.", 34);
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient == null)
            {
                Session.SendWhisper("¡Oops! Probablemente el usuario no se encuentre en linea.", 34);
                return;
            }

            RoomUser TargetUser = Room.GetRoomUserManager().GetRoomUserByHabbo(TargetClient.GetHabbo().Id);

            if (TargetUser == null)
            {
                Session.SendWhisper("¡Oops! Probablemente no se encuentre en la sala", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("¡No te suicides! :(", 34);
                return;
            }

            if (TargetClient.GetHabbo().GetPermissions().HasRight("mod_tool"))
            {
                Session.SendWhisper("No puedes asesinar a este usuario.", 34);
                return;
            }

            if (TargetUser.isLying || TargetUser.isSitting)
            {
                Session.SendWhisper("No puedes asesinarlo así...", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == "Forbi" || TargetClient.GetHabbo().Username == "Forb")
            {
                Session.SendWhisper("¡No puedes matar a ese usuario!", 34);
                return;
            }

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            if (!((Math.Abs(TargetUser.X - ThisUser.X) > 1) || (Math.Abs(TargetUser.Y - ThisUser.Y) > 1)))
            {
                Room.SendMessage(new ChatComposer(ThisUser.VirtualId, " *Ha asesinado a " + Params[1] + "*", 0, ThisUser.LastBubble));
                Room.SendMessage(new ChatComposer(TargetUser.VirtualId, "Oh no, he muerto x.x", 0, TargetUser.LastBubble));
                TargetUser.RotBody--;//
                TargetUser.Statusses.Add("lay", "1.0 null");
                TargetUser.Z -= 0.35;
                TargetUser.isLying = true;
                TargetUser.UpdateNeeded = true;
            }
            else
            {
                Session.SendWhisper("¡Oops! " + Params[1] + " esta muy alejado.", 34);
            }
        }
    }
}