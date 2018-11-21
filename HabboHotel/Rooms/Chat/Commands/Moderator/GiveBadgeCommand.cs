using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class GiveBadgeCommand : IChatCommand
    {
        public string PermissionRequired => "user_12";
        public string Parameters => "[USUARIO] [IDPLACA]";
        public string Description => "Dar placa a un usuario.";

        public void Execute(GameClient Session, Room Room, string[] Params)
        {

            if (Params.Length != 3)
            {
                Session.SendWhisper("Por favor, introduzca un nombre de usuario y el código del identificador que le gustaría dar!");
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient != null)
            {
                if (!TargetClient.GetHabbo().GetBadgeComponent().HasBadge(Params[2]))
                {
                    TargetClient.GetHabbo().GetBadgeComponent().GiveBadge(Params[2], true, TargetClient);
                    if (TargetClient.GetHabbo().Id != Session.GetHabbo().Id)
                        TargetClient.SendMessage(RoomNotificationComposer.SendBubble("badge/" + Params[2], "Acabas de recibir una placa!", "/inventory/open/badge"));

                    else
                        Session.SendWhisper("Ha enviado correctamente la placa  " + Params[2] + "!", 34);

                }
                else
                    Session.SendWhisper("Vaya, ese usuario ya tiene esta placa (" + Params[2] + ") !");
                return;
            }
            else
            {
                Session.SendWhisper("Vaya, no hemos podido encontrar al usuario!");
                return;
            }
        }
    }
}
