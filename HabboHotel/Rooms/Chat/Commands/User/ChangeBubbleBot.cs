using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.Bots;
using Yezz.HabboHotel.GameClients;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class BubbleBotCommand : IChatCommand
    {
        public string PermissionRequired => "user_vip";
        public string Parameters => "[BOTNAME] [BUBBLEID]";
        public string Description => "Asígnale una burbuja de chat a tu bot.";

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (User == null)
                return;

            string BotName = CommandManager.MergeParams(Params, 1);
            string Bubble = CommandManager.MergeParams(Params, 2);
            int BubbleID = 0;

            long nowTime = YezzEnvironment.CurrentTimeMillis();
            long timeBetween = nowTime - Session.GetHabbo()._lastTimeUsedHelpCommand;
            if (timeBetween < 60000 && Session.GetHabbo().Rank == 1)
            {
                Session.SendMessage(RoomNotificationComposer.SendBubble("abuse", "Espera al menos 1 minuto para volver a cambiar la burbuja de tu Bot.\n\nCompra la suscripción VIP de "+YezzEnvironment.HotelName+" haciendo click aquí para evitar esta espera.", "catalog/open/clubVIP"));
                return;
            }

            Session.GetHabbo()._lastTimeUsedHelpCommand = nowTime;

            if (Params.Length == 1)
            {
                Session.SendWhisper("No has introducido un nombre de bot válido.", 34);
                return;
            }

            RoomUser Bot = Room.GetRoomUserManager().GetBotByName(Params[1]);
            if (Bot == null)
            {
                Session.SendWhisper("No hay ningún bot llamado "+ Params[1] +" en la sala.", 34);
                return;
            }

            if (Bot.BotData.ownerID != Session.GetHabbo().Id)
                {
                Session.SendWhisper("Estás cambiándole la burbuja a un bot que no es tuyo, crack, máquina, figura.", 34);
                return;
                }

            if (Bubble == "1" || Bubble == "23" || Bubble == "34" || Bubble == "37")
            {
                Session.SendWhisper("Estás colocando una burbuja prohibida.");
                return;
            }

                if (Params.Length == 2)
                {
                    Session.SendWhisper("Uy, se te olvidó introducir una ID de la burbuja.", 34);
                    return;
                }
                
                if (int.TryParse(Bubble, out BubbleID))
                {
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `bots` SET `chat_bubble` =  '" + BubbleID + "' WHERE `name` =  '" + Bot.BotData.Name + "' AND  `room_id` =  '" + Session.GetHabbo().CurrentRoomId + "'");
                        Bot.Chat("Me acabas de colocar la burbuja " + BubbleID + ".", true, BubbleID);
                        Bot.BotData.ChatBubble = BubbleID;
                }
                }

            return;
        }
    }
}