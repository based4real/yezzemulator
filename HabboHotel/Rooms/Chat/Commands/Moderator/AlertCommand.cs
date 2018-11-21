using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class AlertCommand : IChatCommand
    {
        public string PermissionRequired => "user_10";
        public string Parameters => "[USUARIO] [MENSAJE]";
        public string Description => "Enviar alerta a un usuario.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {

            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor, introduzca el nombre de usuario del usuario que desea alertar.", 34);
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("Se produjo un error mientras que la búsqueda de usuario, tal vez no están en línea.", 34);
                return;
            }

            if (TargetClient.GetHabbo() == null)
            {
                Session.SendWhisper("Se produjo un error mientras que la búsqueda de usuario, tal vez no están en línea.", 34);
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Consigue una vida.");
                return;
            }

            string Message = CommandManager.MergeParams(Params, 2);

            TargetClient.SendNotification(Session.GetHabbo().Username + " te ha alertado con el siguiente mensaje:\n\n" + Message);
            Session.SendWhisper("Alerta enviada con éxito para " + TargetClient.GetHabbo().Username, 34);

        }
    }
}
