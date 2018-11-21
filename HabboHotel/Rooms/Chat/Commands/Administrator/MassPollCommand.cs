using Yezz.Communication.Packets.Outgoing;
using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Communication.Packets.Outgoing.Rooms.Polls;
using Yezz.HabboHotel.Rooms.Polls;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class MassPollCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_13"; }
        }

        public string Parameters
        {
            get { return "[ID]"; }
        }

        public string Description
        {
            get { return "Envia una encuesta a todo el hotel"; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Por favor introduzca la ID de la poll que desee enviar.", 34);
                return;
            }

            RoomPoll poll = null;
            if (YezzEnvironment.GetGame().GetPollManager().TryGetPollForHotel(int.Parse(Params[1]), out poll))
            {
                if (poll.Type == RoomPollType.Poll)
                {
                    YezzEnvironment.GetGame().GetClientManager().SendMessage(new PollOfferComposer(poll));
                }
            }
            return;
        }
    }
}
