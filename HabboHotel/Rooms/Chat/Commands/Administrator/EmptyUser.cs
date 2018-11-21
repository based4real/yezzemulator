using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class EmptyUser : IChatCommand
    {
        public string PermissionRequired => "user_14";
        public string Parameters => "[USUARIO]";
        public string Description => "Limpiar Inventario a un Usuario";

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Escribe el nombre del usuario que deseas limpiar el inventario.");
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("¡Oops! Probablemente el usuario no se encuentre en linea.");
                return;
            }

            if (TargetClient.GetHabbo().Rank >= Session.GetHabbo().Rank)
            {
                Session.SendWhisper("No puedes limpiar el inventario a este usuario");
                return;
            }

            TargetClient.GetHabbo().GetInventoryComponent().ClearItems();
        }
    }
}