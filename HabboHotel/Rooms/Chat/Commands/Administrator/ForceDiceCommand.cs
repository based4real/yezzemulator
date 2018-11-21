﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class ForceDiceCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_16"; }
        }

        public string Parameters
        {
            get { return "%number%"; }
        }

        public string Description
        {
            get { return "Allows you to carry a hand item"; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Debes colocar una cifra para que salga en el dado de 1 a 6.");
                return;
            }

            GameClient Target = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (Target == null)
            {
                Session.SendWhisper("¡Oops, no se ha conseguido este usuario!");
                return;
            }

            int Number;
            if (!int.TryParse(Params[2], out Number))
            {
                Session.SendWhisper("Porfavor introduce un número válido.");
                return;
            }

            if(Number > 6 || Number < 1)
            {
                Session.SendWhisper("La cifra debe estar entre 1 y 6.");
                return;
            }

            Target.GetHabbo().RigDice = true;
            Target.GetHabbo().DiceNumber = Number;
            Session.SendWhisper("Acabas de activar el número " + Number + " para que salga en los dados de " + Target.GetHabbo().Username + ".");
        }
    }
}

