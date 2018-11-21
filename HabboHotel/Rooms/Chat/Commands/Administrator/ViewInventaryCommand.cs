using Yezz.HabboHotel.GameClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class ViewInventaryCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_10"; }
        }

        public string Parameters
        {
            get { return "[USUARIO]"; }
        }

        public string Description
        {
            get { return "Permite ver el inventario de un usuario"; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Room == null)
                return;

            if (Params.Length == 2)
            {
                string Username = Params[1];

                GameClient Client = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Username);
                if (Client != null)
                {
                    Session.SendWhisper("El usuario está online. Espera a que este se desconecte para poder ver su inventario.");
                    return;
                }

                int UserId = YezzEnvironment.GetGame().GetClientManager().GetUserIdByUsername(Username);
                if (UserId == 0)
                {
                    Session.SendWhisper("El nombre de usuario no existe.");
                    return;
                }

                Session.GetHabbo().GetInventoryComponent().LoadUserInventory(UserId);

                Session.SendWhisper("El inventario ha sido cambiado por el de " + Username);
            }
            else
            {
                Session.GetHabbo().GetInventoryComponent().LoadUserInventory(0);

                Session.SendWhisper("Tu inventario ha vuelto a la normalidad.");
            }

            Session.SendWhisper("La sala se ha guardado correctamente a la lista.");
        }
    }
}