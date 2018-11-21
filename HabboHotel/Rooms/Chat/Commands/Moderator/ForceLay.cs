using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class ForceLay : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_6"; }
        }

        public string Parameters
        {
            get { return "[USUARIO]"; }
        }

        public string Description
        {
            get { return "Forzar al usuario a acostarse."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Vaya, se te olvidó elegir un usuario de destino!", 34);
                return;
            }

            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Params[1]);
            if (User == null)
                return;

            if (User.Statusses.ContainsKey("lie") || User.isLying || User.RidingHorse || User.IsWalking)
                return;

            if (!User.Statusses.ContainsKey("lay"))
            {
                if ((User.RotBody % 2) == 0)
                {
                    if (User == null)
                        return;

                    try
                    {
                        User.Statusses.Add("lay", "1.0");
                        User.Z -= 0.35;
                        User.isLying = true;
                        User.UpdateNeeded = true;
                    }
                    catch { }
                }
                else
                {
                    User.RotBody--;
                    User.Statusses.Add("lay", "1.0");
                    User.Z -= 0.35;
                    User.isLying = true;
                    User.UpdateNeeded = true;
                }
            }
            else if (User.isLying == true)
            {
                User.Z += 0.35;
                User.Statusses.Remove("lay");
                User.Statusses.Remove("1.0");
                User.isLying = false;
                User.UpdateNeeded = true;
            }
        }
    }
}
