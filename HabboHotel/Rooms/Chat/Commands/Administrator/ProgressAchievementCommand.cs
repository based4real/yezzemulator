using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class ProgressAchievementCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_16"; }
        }

        public string Parameters
        {
            get { return "<usuario> <achievement> <puntos>"; }
        }

        public string Description
        {
            get { return "Progresar la recompensa de un usuario."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (Params.Length != 4)
            {
                Session.SendWhisper("Introduce el nombre del usuario a quien deseas enviar una placa!");
                return;
            }

            GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient != null)
            {
                if (YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(TargetClient, "ACH_" + Params[2], int.Parse(Params[3])))
                {
                    Session.SendMessage(RoomNotificationComposer.SendBubble("definitions", "Has progresado el logro " + Params[2] + " a " + TargetClient.GetHabbo().Username + " " + Params[3] + " puntos.", ""));
                }
                else { Session.SendWhisper("Introducido algún valor mal, comprúebalo: ACH = " + Params[2] + " PROGRESO = " + Params[3] + "."); }
            }
        }
    }
}
