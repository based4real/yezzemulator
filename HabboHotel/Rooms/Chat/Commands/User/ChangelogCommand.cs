using System;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class ChangelogCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_normal"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Últimas actualizaciones de Yezz."; }
        }

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            var _cache = new Random().Next(0, 300);
            Session.SendMessage(new MassEventComposer("habbopages/changelogs.txt?" + _cache));
        }
    }
}