using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Database.Interfaces;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class ColourCommand : IChatCommand
    {

        public string PermissionRequired => "user_normal";
        public string Parameters => "";
        public string Description => "off/red/green/blue/cyan/purple";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendMessage(new RoomNotificationComposer("Lista de colores:",
                     "<font color='#FF8000'><b>LISTA DE COLORES:</b>\n" +
                     "<font size=\"12\" color=\"#1C1C1C\">El comando :color te permitirá fijar un color que tu desees en tu bocadillo de chat, para poder seleccionar el color deberás especificarlo después de hacer el comando, como por ejemplo:\r\r" +
                     "<font size =\"11\" color=\"#FE2E2E\"><b>:color red</b> » Bienvenidos al infierno</font>\r\n" +
                     "<font size =\"11\" color=\"#8904B1\"><b>:color purple</b> » Pon tu toque de glamour</font>\r\n" +
                     "<font size =\"11\" color=\"#2ECCFA\"><b>:color cyan</b> » Tenemos un hueco para el cielo</font>\r\n" +
                     "<font size =\"11\" color=\"#0174DF\"><b>:color blue</b> » Tan bello como el mar..</font>\r\n" +
                     "<font size =\"11\" color=\"#31B404\"><b>:color green</b> » Unete al movimiento ecológico</font>\r\n" +
                     "", "", ""));
                return;
            }
            string chatColour = Params[1];
            string Colour = chatColour.ToUpper();
            switch (chatColour)
            {
                case "none":
                case "black":
                case "off":
                    Session.GetHabbo().chatColour = "";
                    Session.SendWhisper("Tu Color de Chat Ha Sido Desactivado", 34);
                    break;
                case "blue":
                case "red":
                case "green":
                case "cyan":
                case "purple":
                case "yellow":
                case "orange":
                    Session.GetHabbo().chatColour = chatColour;
                    Session.SendWhisper("@" + chatColour + "@Tu Color de chat ha sido activado a: " + chatColour + "", 34);
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.runFastQuery("UPDATE `users` SET `bubble_color` = '" + chatColour + "' WHERE `id` = '" + Session.GetHabbo().Id + "' LIMIT 1");
                    }
                    break;
                default:
                    Session.SendWhisper("El Color de chat: " + Colour + " No Existe!", 34);
                    break;
            }
            return;
        }
    }
}