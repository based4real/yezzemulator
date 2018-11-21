using System;
using System.Linq;
using Yezz.Database.Interfaces;
using System.Data;
using Yezz.HabboHotel.GameClients;

using Yezz.HabboHotel.Users.Messenger;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class ChatAlertCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_vip"; }
        }
        public string Parameters
        {
            get { return "[USUARIO] [MENSAJE]"; }
        }
        public string Description
        {
            get { return "Envia mensajes a un usuario."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
                if (Params.Length == 1)
                {
                    Session.SendWhisper("Escriba el nombre de usuario al que desea enviar un mensaje.", 34);
                    return;
                }

                GameClient TargetClient = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
                if (TargetClient == null)
                {
                    Session.SendWhisper("El usuario no está en línea!", 34);
                    return;
                }

                if (TargetClient.GetHabbo() == null)
                {
                    Session.SendWhisper("El usuario no está en línea!", 34);
                    return;
                }

                if (TargetClient.GetHabbo().Rank >= 3)
                {
                    Session.SendWhisper("¡No puedes enviar mensajes a los Staffs!", 34);
                    return;
                }

                if (TargetClient.GetHabbo().AllowFriendRequests)
                {
                    Session.SendWhisper("¡El usuario ha desactivado los mensajes!", 34);
                    return;
                }

                // Kolla om personerna är vänner!

                if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
                {
                    Session.SendWhisper("No puedes enviarte un mensaje, a ti mismo", 34);
                    return;
                }

                string Message = CommandManager.MergeParams(Params, 2);

                TargetClient.SendWhisper("Recibiste un mensaje de " + Session.GetHabbo().Username, 34);
                TargetClient.SendWhisper(Message, 34);
                Session.SendWhisper("Mensaje enviado a " + TargetClient.GetHabbo().Username + "!", 34);
            }

        }
    }