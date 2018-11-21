using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Database.Interfaces;
using Yezz.Utilities;
using Yezz.HabboHotel.Users;
using Yezz.HabboHotel.GameClients;



namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class MuteCommand : IChatCommand
    {
        public string PermissionRequired => "user_10";
        public string Parameters => "[USUARIO] [TIEMPO]";
        public string Description => "Mutear al usuario por un tiempo.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Introduce el nombre del usuario a mutear y el tiempo expresado en Segundos (Maximo 600).", 34);
                return;
            }

            Habbo Habbo = YezzEnvironment.GetHabboByUsername(Params[1]);
            if (Habbo == null)
            {
                Session.SendWhisper("Ocurrio un error mientras se buscaba al usuario en la base de datos.", 34);
                return;
            }

            if (Habbo.Username == "Forbi" || Habbo.Username == "Forb" || Habbo.Username == "Antoniocrevi")
            {
                Session.SendWhisper("¡No puedes mutear a ese usuario!", 34);
                return;
            }

            double Time;
            if (double.TryParse(Params[2], out Time))
            {
                if (Time > 600 && !Session.GetHabbo().GetPermissions().HasRight("mod_mute_limit_override"))
                    Time = 600;

                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.RunQuery("UPDATE `users` SET `time_muted` = '" + Time + "' WHERE `id` = '" + Habbo.Id + "' LIMIT 1");
                }

                if (Habbo.GetClient() != null)
                {
                    Habbo.TimeMuted = Time;
                    Habbo.GetClient().SendNotification("Usted ha sido muteado " + Time + " segundos!");
                }

                Session.SendWhisper("Muteaste a  " + Habbo.Username + " por " + Time + " segundos.", 34);
            }
            else
                Session.SendWhisper("Por favor introduce numeros enteros.", 34);
        }
    }
}