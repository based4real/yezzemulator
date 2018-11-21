using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class ChatHTMLSizeCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_vip"; }
        }
        public string Parameters
        {
            get { return "Numero del 1 al 20."; }
        }
        public string Description
        {
            get { return "Cambia el tamaño de tu nombre"; }
        }
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
                if (Params.Length == 1)
                {
                    Session.SendWhisper("Oops, debes escribir un numero de 1-20!", 34);
                    return;
                }
                string chatColour = Params[1];

                int chatsize;
                bool isNumeric = int.TryParse(chatColour, out chatsize);
                if (isNumeric)
                {
                    if (Session.GetHabbo().chatHTMLColour == null || Session.GetHabbo().chatHTMLColour == String.Empty)
                    {
                        Session.GetHabbo().chatHTMLColour = "000000";
                    }
                    switch (chatsize)
                    {
                        case 12:
                            Session.GetHabbo().chatHTMLSize = 12;
                            Session.SendWhisper("Tu tamaño de nombre ha vuelto a la normalidad", 34);
                            break;
                        default:
                            bool isValid = true;
                            if (chatsize < 1)
                            {
                                isValid = false;
                            }

                            if (chatsize > 20 && Session.GetHabbo().Rank < 6)
                            {
                                isValid = false;
                            }
                            if (isValid)
                            {
                                Session.SendWhisper("El tamaño ha sido cambiado a " + chatsize, 34);
                                Session.GetHabbo().chatHTMLSize = chatsize;
                            }
                            else
                            {
                                Session.SendWhisper("Tamaño invalido, debe ser numero de 1-20.", 34);
                            }

                            break;
                    }
                }
                else
                {
                    Session.SendWhisper("Tamaño invalido, debe ser numero de 1-20.", 34);
                }
                return;
            }
        }
    }
