using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Nux;
using Yezz.Communication.Packets.Outgoing.Rooms.Nux;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class GiveSpecialReward : IChatCommand
    {
        public string PermissionRequired => "user_13";
        public string Parameters => "[USUARIO]";
        public string Description => "";

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            if (Params.Length == 0)
            {
                Session.SendWhisper("Por favor introduce un nombre de usuario para premiar.", 34);
                return;
            }

            GameClient Target = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (Target == null)
            {
                Session.SendWhisper("Oops, No se ha conseguido este usuario!", 34);
                return;
            }

            Target.SendMessage(new NuxItemListComposer());
            Session.SendWhisper("Has activado correctamente el premio especial para " + Target.GetHabbo().Username, 34);
        }
    }
}