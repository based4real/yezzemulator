﻿using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Events
{
    internal class HelpCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get
            {
                return "user_normal";
            }
        }
        public string Parameters
        {
            get { return "%message%"; }
        }
        public string Description
        {
            get
            {
                return "Envía una petición de ayuda, describiendo brevemente tu problema.";
            }
        }
        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            long nowTime = YezzEnvironment.CurrentTimeMillis();
            long timeBetween = nowTime - Session.GetHabbo()._lastTimeUsedHelpCommand;
            if (timeBetween < 60000)
            {
                Session.SendMessage(RoomNotificationComposer.SendBubble("abuse", "Espera al menos 1 minuto para volver a usar el sistema de soporte.", ""));
                return;
            }

            Session.GetHabbo()._lastTimeUsedHelpCommand = nowTime;

            //YezzEnvironment.GetGame().GetAchievementManager().ProgressAchievement(Session, "ACH_GuideEnrollmentLifetime", 1);
            Session.SendMessage(new MassEventComposer("help/tour"));
            Session.SendMessage(RoomNotificationComposer.SendBubble("ambassador", "Tu petición de ayuda ha sido enviada correctamente, por favor espera.", ""));
        }
    }
}



