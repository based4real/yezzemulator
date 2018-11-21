using Yezz.Communication.Packets.Outgoing.Rooms.Engine;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using System;
using System.Text;
using Yezz.Communication.Packets.Outgoing.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class PetCommand : IChatCommand
    {
        public string PermissionRequired => "user_normal";
        public string Parameters => "";
        public string Description => "Transformarte en una mascota.";

        public void Execute(GameClients.GameClient Session, Room Room, string[] Params)
        {


            RoomUser RoomUser = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (RoomUser == null)
                return;

            if (!Room.PetMorphsAllowed)
            {
                Session.SendWhisper("El dueño de la sala no permite que te conviertas en una mascota.");
                if (Session.GetHabbo().PetId > 0)
                {
                    Session.SendWhisper("Vaya usted, utodavía tiene una metamorfosis, un-morphing.");
                    Session.GetHabbo().PetId = 0;

                    Room.SendMessage(new UserRemoveComposer(RoomUser.VirtualId));
                    Room.SendMessage(new UsersComposer(RoomUser));
                }
                return;
            }

            if (Params.Length == 1)
            {
                StringBuilder List = new StringBuilder("");
                List.AppendLine("                              - LISTA DE MASCOTAS -");
                List.AppendLine("Colocando estos parámetros usando el comando :pet podrás transformarte en:");
                List.AppendLine("  l[»]l 1) perro - Transfórmate en un perro.");
                List.AppendLine("  l[»]l 2) gato - Transfórmate en un gato.");
                List.AppendLine("  l[»]l 3) terrier - Transfórmate en un fox terrier.");
                List.AppendLine("  l[»]l 4) cocodrilo - Transfórmate en un cocodrilo.");
                List.AppendLine("  l[»]l 5) oso - Transfórmate en un oso.");
                List.AppendLine("  l[»]l 6) cerdo - Transfórmate en un cerdo.");
                List.AppendLine("  l[»]l 7) león - Transfórmate en un león.");
                List.AppendLine("  l[»]l 8) rinoceronte - Transfórmate en un rinoceronte.");
                List.AppendLine("  l[»]l 9) araña - Transfórmate en una araña.");
                List.AppendLine("  l[»]l 10) tortuga - Transfórmate en una tortuga.");
                List.AppendLine("  l[»]l 11) pollo - Transfórmate en un pollito.");
                List.AppendLine("  l[»]l 12) rana - Transfórmate en una rana.");
                List.AppendLine("  l[»]l 13) mono - Transfórmate en un mono.");
                List.AppendLine("  l[»]l 14) caballo - Transfórmate en un caballo.");
                List.AppendLine("  l[»]l 15) conejo - Transfórmate en un conejo.");
                List.AppendLine("  l[»]l 16) pajaro - Transfórmate en una pajaro.");
                List.AppendLine("  l[»]l 17) demonio - Transfórmate en un demonio.");
                List.AppendLine("  l[»]l 18) gnomo - Transfórmate en un gnomo.");

                List.AppendLine("  l[»]l 19) piedra - Transfórmate en una piedra.");
                List.AppendLine("  l[»]l 20) bebeoso - Transfórmate en un bebeoso.");
                List.AppendLine("  l[»]l 21) bebeguapo - Transfórmate en un bebeguapo.");
                List.AppendLine("  l[»]l 22) bebefeo - Transfórmate en un bebefeo.");
                List.AppendLine("  l[»]l 23) mario - Transfórmate en mario.");
                List.AppendLine("  l[»]l 24) pikachu - Transfórmate en pikachu.");
                List.AppendLine("  l[»]l 25) lobo - Transfórmate en un lobo.");
                List.AppendLine("  l[»]l 26) elefante - Transfórmate en un elefante.");
                List.AppendLine("  l[»]l 27) pinguino - Transfórmate en un pinguino.");
                List.AppendLine("  l[»]l 28) vaca - Transfórmate en una vaca.");
                List.AppendLine("  l[»]l 29) velociraptor - Transfórmate en un velociraptor.");
                List.AppendLine("  l[»]l 30) pterosaur - Transfórmate en un pterosaur.");
                List.AppendLine("  l[»]l 31) haloompa - Transfórmate en un haloompa.");
                List.AppendLine("  l[»]l 32) cerdito - Transfórmate en un cerdito.");
                List.AppendLine("  l[»]l 33) perrito - Transfórmate en un perrito.");
                List.AppendLine("  l[»]l 34) gatito - Transfórmate en un gatito.");
                List.AppendLine("  l[»]l 35) bebeterrier - Transfórmate en un bebeterrier.");
                List.AppendLine("  l[»]l 36) bebeoso - Transfórmate en un bebeoso.");

                Session.SendMessage(new MOTDNotificationComposer(List.ToString()));
                return;
            }

            if (Params[1].ToString().ToLower() == "list")
            {
                StringBuilder List = new StringBuilder("");
                List.AppendLine("                              - LISTA DE MASCOTAS -");
                List.AppendLine("Colocando estos parámetros usando el comando :pet podrás transformarte en:");
                List.AppendLine("  l[»]l 1) perro - Transfórmate en un perro.");
                List.AppendLine("  l[»]l 2) gato - Transfórmate en un gato.");
                List.AppendLine("  l[»]l 3) terrier - Transfórmate en un fox terrier.");
                List.AppendLine("  l[»]l 4) cocodrilo - Transfórmate en un cocodrilo.");
                List.AppendLine("  l[»]l 5) oso - Transfórmate en un oso.");
                List.AppendLine("  l[»]l 6) cerdo - Transfórmate en un cerdo.");
                List.AppendLine("  l[»]l 7) león - Transfórmate en un león.");
                List.AppendLine("  l[»]l 8) rinoceronte - Transfórmate en un rinoceronte.");
                List.AppendLine("  l[»]l 9) araña - Transfórmate en una araña.");
                List.AppendLine("  l[»]l 10) tortuga - Transfórmate en una tortuga.");
                List.AppendLine("  l[»]l 11) pollo - Transfórmate en un pollito.");
                List.AppendLine("  l[»]l 12) rana - Transfórmate en una rana.");
                List.AppendLine("  l[»]l 13) mono - Transfórmate en un mono.");
                List.AppendLine("  l[»]l 14) caballo - Transfórmate en un caballo.");
                List.AppendLine("  l[»]l 15) conejo - Transfórmate en un conejo.");
                List.AppendLine("  l[»]l 16) pajaro - Transfórmate en una pajaro.");
                List.AppendLine("  l[»]l 17) demonio - Transfórmate en un demonio.");
                List.AppendLine("  l[»]l 18) gnomo - Transfórmate en un gnomo.");

                List.AppendLine("  l[»]l 19) piedra - Transfórmate en una piedra.");
                List.AppendLine("  l[»]l 20) bebeoso - Transfórmate en un bebeoso.");
                List.AppendLine("  l[»]l 21) bebeguapo - Transfórmate en un bebeguapo.");
                List.AppendLine("  l[»]l 22) bebefeo - Transfórmate en un bebefeo.");
                List.AppendLine("  l[»]l 23) mario - Transfórmate en mario.");
                List.AppendLine("  l[»]l 24) pikachu - Transfórmate en pikachu.");
                List.AppendLine("  l[»]l 25) lobo - Transfórmate en un lobo.");
                List.AppendLine("  l[»]l 26) elefante - Transfórmate en un elefante.");
                List.AppendLine("  l[»]l 27) pinguino - Transfórmate en un pinguino.");
                List.AppendLine("  l[»]l 28) vaca - Transfórmate en una vaca.");
                List.AppendLine("  l[»]l 29) velociraptor - Transfórmate en un velociraptor.");
                List.AppendLine("  l[»]l 30) pterosaur - Transfórmate en un pterosaur.");
                List.AppendLine("  l[»]l 31) haloompa - Transfórmate en un haloompa.");
                List.AppendLine("  l[»]l 32) cerdito - Transfórmate en un cerdito.");
                List.AppendLine("  l[»]l 33) perrito - Transfórmate en un perrito.");
                List.AppendLine("  l[»]l 34) gatito - Transfórmate en un gatito.");
                List.AppendLine("  l[»]l 35) bebeterrier - Transfórmate en un bebeterrier.");
                List.AppendLine("  l[»]l 36) bebeoso - Transfórmate en un bebeoso.");

                Session.SendMessage(new MOTDNotificationComposer(List.ToString()));
                return;
            }

            int TargetPetId = GetPetIdByString(Params[1].ToString());
            if (TargetPetId == 0)
            {
                Session.SendWhisper("Vaya, no hay mascota con ese nombre!");
                return;
            }

            //Change the users Pet Id.
            Session.GetHabbo().PetId = (TargetPetId == -1 ? 0 : TargetPetId);

            //Quickly remove the old user instance.
            Room.SendMessage(new UserRemoveComposer(RoomUser.VirtualId));

            //Add the new one, they won't even notice a thing!!11 8-)
            Room.SendMessage(new UsersComposer(RoomUser));

            //Tell them a quick message.
            if (Session.GetHabbo().PetId > 0)
                Session.SendWhisper("Usa ':pet habbo' para regresarte a tu keko!");
        }

        private int GetPetIdByString(string Pet)
        {
            switch (Pet.ToLower())
            {
                default:
                    return 0;
                case "habbo":
                    return -1;
                case "perro":
                    return 60;//This should be 0.
                case "gato":
                case "1":
                    return 1;
                case "terrier":
                case "2":
                    return 2;
                case "croc":
                case "croco":
                case "cocodrilo":
                case "3":
                    return 3;
                case "oso":
                case "4":
                    return 4;
                case "liz":
                case "cerdo":
                case "kill":
                case "5":
                    return 5;
                case "leon":
                case "rawr":
                case "6":
                    return 6;
                case "rhino":
                case "rinoceronte":
                case "7":
                    return 7;
                case "spider":
                case "arana":
                case "araña":
                case "8":
                    return 8;
                case "tortuga":
                case "9":
                    return 9;
                case "chick":
                case "chicken":
                case "pollo":
                case "10":
                    return 10;
                case "frog":
                case "rana":
                case "11":
                    return 11;
                case "drag":
                case "dragon":
                case "12":
                    return 12;
                case "monkey":
                case "mono":
                case "14":
                    return 14;
                case "horse":
                case "caballo":
                case "15":
                    return 15;
                case "bunny":
                case "conejo":
                case "17":
                    return 17;
                case "pigeon":
                case "pajaro":
                case "21":
                    return 21;
                case "demon":
                case "demonio":
                case "23":
                    return 23;
                case "babybear":
                case "bebeoso":
                case "24":
                    return 24;
                case "babyterrier":
                case "bebeterrier":
                case "25":
                    return 25;
                case "gnome":
                case "gnomo":
                case "26":
                    return 26;
                case "kitten":
                case "gatito":
                case "28":
                    return 28;
                case "puppy":
                case "perrito":
                case "29":
                    return 29;
                case "piglet":
                case "cerdito":
                case "30":
                    return 30;
                case "haloompa":
                case "31":
                    return 31;
                case "rock":
                case "piedra":
                case "32":
                    return 32;
                case "pterosaur":
                case "33":
                    return 33;
                case "velociraptor":
                case "34":
                    return 34;
                case "vaca":
                case "35":
                    return 35;
                case "pinguino":
                case "36":
                    return 36;
                case "elefante":
                case "37":
                    return 37;
                case "bebeguapo":
                case "38":
                    return 38;
                case "bebefeo":
                case "39":
                    return 39;
                case "mario":
                case "40":
                    return 40;
                case "pikachu":
                case "41":
                    return 41;
                case "lobo":
                case "42":
                    return 42;
            }
        }
    }
}